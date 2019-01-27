using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterZone : MonoBehaviour
{

	[HideInInspector] public GameObject Monster;
	[HideInInspector] public GameObject TriggerArea;
	[HideInInspector] public DeadZone deadZone;
	[HideInInspector] public GameObject BlackFog;
	[HideInInspector] public SelfMovement selfMovementScript;
	public GameObject[] Points;
	[HideInInspector] public int currentPoint = 0;
	[HideInInspector] public int currentSequence = 0;
	[HideInInspector] public int currentSequencePosition = 0;
	[HideInInspector] public int[][] sequences;
	[HideInInspector] public bool isMovementFinished = false;
	[HideInInspector] public bool isReturningToBase = false;
	[HideInInspector] public bool isActive = false;
	[HideInInspector] public bool isStartWithCameraSequence = true;
	[HideInInspector] public bool isCameraSequenceRunning = false;
	[HideInInspector] public float CameraSequenceStartTime;
	[HideInInspector] public float CameraSequenceDuration = 5;
	[HideInInspector] private Quaternion originRotation;
	
	private float deadAnimationDuration = 6;
	private float deadAnimationCameraEffectsProgress = 0;
	private bool isPlayerCaught = false;
	private float currentSequenceDuration = 0;
	
	public float speed = 3.0f;
	public float speedReturning = 5.0f;
	

	protected abstract void Init();
	protected abstract int NextSequence();
	protected abstract int returnSequence();
	
	void Start ()
	{
		Monster = transform.Find("Monster").gameObject;
		deadZone = Monster.transform.Find("DeadZone").gameObject.GetComponent<DeadZone>();
		BlackFog = Monster.transform.Find("BlackFog").gameObject;
		originRotation = Monster.transform.rotation;
		selfMovementScript = Monster.GetComponent<SelfMovement>();
		TriggerArea = transform.Find("TriggerArea").gameObject;
		StartCoroutine(DelayedStart(1));

		Init();
		isActive = false;
		Reset();
	}

	public void Reset()
	{
		deadAnimationCameraEffectsProgress = 0;
		isPlayerCaught = false;
		deadZone.isTriggered = false;
		BlackFog.SetActive(isActive);
		currentSequence = 0;
		currentSequencePosition = 0;
		isReturningToBase = false;
		Monster.transform.position = Points[sequences[0][0]].transform.position;
		Monster.transform.rotation = originRotation;
		selfMovementScript.speed = speed;
		selfMovementScript.active = false;
		currentPoint = 0;
		isCameraSequenceRunning = false;
		Monster.SetActive(isActive);
		if (isActive) deadZone.ignorePlayerCollision();
	}

	IEnumerator DelayedStart(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Physics.IgnoreCollision(GameManager.Player.GetComponent<Collider>(), TriggerArea.GetComponent<Collider>());
	}

	private void deactivate()
	{
		if (isActive)
		{
			isReturningToBase = true;
			selfMovementScript.speed = speedReturning;
		}
	}
	
	void Update () {
		
		if (isActive)
		{
			if (isMovementFinished)
			{
				if (!isReturningToBase)
				{
					currentSequencePosition++;
					
					// if sequence end reached, start next sequence
					if (currentSequencePosition >= sequences[currentSequence].Length)
					{
						currentSequence = NextSequence();
						currentSequencePosition = 0;
					}
//					Debug.Log("currentSequencePosition: " + currentSequencePosition + " | currentSequence: " + currentSequence);
			
					currentPoint = sequences[currentSequence][currentSequencePosition];
				}
				else
				{
					currentPoint = returnSequence();
					// reached base
					if (-1 == currentPoint)
					{
						isActive = false;
						Reset();
						return;
					}
				}
			
				selfMovementScript.Goto(Points[currentPoint].transform.position);
				isMovementFinished = false;
			}

			isMovementFinished = !selfMovementScript.active;
		}
		
		if (!isPlayerCaught && (!isActive || (isActive && isReturningToBase)))
		{
			isActive = TriggerArea.GetComponent<BoxCollider>().bounds.Contains(GameManager.Player.transform.position);

			if (isActive)
			{
				Reset();
				if (isStartWithCameraSequence) startCameraSequence(CameraSequenceDuration);
				SoundSystem.PlaySound("Audio/Cave/Monster/Monster-Growl (5)", .5f, 1, 10, 0, Monster);
				// reset all other monsters zones
				foreach (var monsterZone in GameManager.monsterZones)
				{
					if (this != monsterZone) monsterZone.deactivate();
				}
			}
		}

		// start die animation
		if (deadZone.isTriggered && !isPlayerCaught)
		{
			isPlayerCaught = true;
			string file = "Audio/Cave/Monster/Monster-Scream (" + Random.Range(1, 5) +")";
			SoundSystem.PlaySound(file, 0, 1, 10, 0, Monster);
			selfMovementScript.Goto(GameManager.Player.transform.position);
			selfMovementScript.speed = 1;
			startCameraSequence(deadAnimationDuration);
		}

		if (isPlayerCaught)
		{
			deadAnimationCameraEffectsProgress += Time.deltaTime * 0.5f;
			
			GameManager.cameraEffects.ChangeBlur(true);
			GameManager.cameraEffects.ChangeVignette(true);
			GameManager.cameraEffects.SetBlurIntensity(deadAnimationCameraEffectsProgress);
			GameManager.cameraEffects.SetVignetteIntensity(deadAnimationCameraEffectsProgress);
		}
		
		// finish die animation
		if (deadZone.isTriggered && !isCameraSequenceRunning)
		{
			GameManager.Die();
		}
		
		if (isCameraSequenceRunning && currentSequenceDuration < (Time.time - CameraSequenceStartTime )) stopCameraSequence();
	}

	void startCameraSequence(float duration)
	{
		GameManager.Player.GetComponent<Cinema>().LookAtTarget = Monster;
		GameManager.Player.GetComponent<Cinema>().start = true;
		CameraSequenceStartTime = Time.time;
		isCameraSequenceRunning = true;
		currentSequenceDuration = duration;
	}
	
	void stopCameraSequence()
	{
		GameManager.Player.GetComponent<Cinema>().deactivcate();
		isCameraSequenceRunning = false;
	}
}
