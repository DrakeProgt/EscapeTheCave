using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterZone : MonoBehaviour
{

	[HideInInspector] public GameObject Monster;
	[HideInInspector] public GameObject TriggerArea;
	[HideInInspector] public GameObject BlackFog;
	[HideInInspector] public SelfMovement selfMovementScript;
	public GameObject[] Points;
	[HideInInspector] public int currentPoint = 0;
	[HideInInspector] public int currentSequence = 0;
	[HideInInspector] public int currentSequencePosition = 0;
	[HideInInspector] public int[][] sequences;
	[HideInInspector] public bool movementFinished = false;
	[HideInInspector] public bool returnToBase = false;
	[HideInInspector] public bool active = false;
	[HideInInspector] private Quaternion originRotation;
	public float speed = 3.0f;
	public float speedReturning = 5.0f;
	

	protected abstract void Init();
	protected abstract int NextSequence();
	protected abstract int returnSequence();
	
	void Start ()
	{
		Monster = transform.Find("Monster").gameObject;
		BlackFog = Monster.transform.Find("BlackFog").gameObject;
		originRotation = Monster.transform.rotation;
		selfMovementScript = Monster.GetComponent<SelfMovement>();
		TriggerArea = transform.Find("TriggerArea").gameObject;
		StartCoroutine(DelayedStart(1));

		Init();
		active = false;
		Reset();
	}

	public void Reset()
	{
		BlackFog.SetActive(active);
		currentSequence = 0;
		currentSequencePosition = 0;
		returnToBase = false;
		Monster.transform.position = Points[sequences[0][0]].transform.position;
		Monster.transform.rotation = originRotation;
		selfMovementScript.speed = speed;
		selfMovementScript.active = false;
		currentPoint = 0;
	}

	IEnumerator DelayedStart(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Physics.IgnoreCollision(GameManager.Player.GetComponent<Collider>(), TriggerArea.GetComponent<Collider>());
	}

	private void deactivate()
	{
		if (active)
		{
			returnToBase = true;
			selfMovementScript.speed = speedReturning;
		}
	}
	
	void Update () {
		
		if (active)
		{
			if (movementFinished)
			{
				if (!returnToBase)
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
						active = false;
						Reset();
						return;
					}
				}
			
				selfMovementScript.Goto(Points[currentPoint].transform.position);
				movementFinished = false;
			}

			movementFinished = !selfMovementScript.active;
		}
		
		if (!active || (active && returnToBase))
		{
			active = TriggerArea.GetComponent<BoxCollider>().bounds.Contains(GameManager.Player.transform.position);

			if (active)
			{
				Reset();
				// reset all other monsters zones
				foreach (var monsterZone in GameManager.monsterZones)
				{
					if (this != monsterZone) monsterZone.deactivate();
				}
			}
		}
		
	}
}
