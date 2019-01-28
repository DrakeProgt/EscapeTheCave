using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSequence : MonoBehaviour
{

	private TriggerZone TargetArea;
	private GameObject Arrows;
	private bool arrowsShot = false;
	private GameObject LookAtTarget1;
	private GameObject LookAtTarget2;
	private GameObject LookAtTarget3;

	private Vector3 PlayerPosition;
	private bool started = false;
	private float runTime;
	private float fadeoutProgress;

	private bool[] stepsFinished = new bool [10];
	
	// Use this for initialization
	void Start ()
	{
		Arrows = transform.Find("PoisonArrows").gameObject;
		LookAtTarget1 = transform.Find("LookAtTarget1").gameObject;
		LookAtTarget2 = transform.Find("LookAtTarget2").gameObject;
		LookAtTarget3 = transform.Find("LookAtTarget3").gameObject;
		TargetArea = transform.Find("TriggerArea").gameObject.GetComponent<TriggerZone>();
		started = false;
		arrowsShot = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		// step 0
		if (TargetArea.isTriggered && !started)
		{
			started = true;
			runTime = 0;
			GameManager.Player.GetComponent<Cinema>().LookAtTarget = LookAtTarget1;
			GameManager.Player.GetComponent<Cinema>().MoveToTarget = LookAtTarget1;
			GameManager.Player.GetComponent<Cinema>().start = true;
		}

		if (!started) return;
		
		runTime += Time.deltaTime;
		
		// step 1
		if (runTime > 4 && !stepsFinished[1])
		{
			stepsFinished[1] = true;
			SoundSystem.PlaySound("Audio/EndTunnel/Stone1-Crack", 0, 1, 10, 0, GameManager.Player);
			GameManager.Player.GetComponent<Cinema>().setLookAtTarget(LookAtTarget2);
			GameManager.Player.GetComponent<Cinema>().MoveToTarget = null;
		}
		
		// step 2
		if (runTime > 6 && !stepsFinished[2])
		{
			stepsFinished[2] = true;
			GameManager.Player.GetComponent<Cinema>().setLookAtTarget(LookAtTarget3);
		}
		
		// step 3
		if (runTime > 8 && !stepsFinished[3])
		{
			stepsFinished[3] = true;
			SoundSystem.PlaySound("Audio/EndTunnel/shot", 0, 1, 10, 0, LookAtTarget2);
			arrowsShot = true;
			GameManager.Player.GetComponent<Cinema>().setLookAtTarget(LookAtTarget1);
		}

		if (arrowsShot)
		{
			Vector3 position = Arrows.transform.position;
			position.x += Time.deltaTime * 20;
			Arrows.transform.position = position;
		}
		
		// step 4
		if (runTime > 9 && !stepsFinished[4])
		{
			stepsFinished[4] = true;
			fadeoutProgress = 0;
			SoundSystem.PlayScream(2, 0.5f);
			PlayerPosition = GameManager.Player.transform.position;
			GameManager.Player.GetComponent<Collider>().enabled = false;
			
		}

		// step 5
		if (runTime > 10 && !stepsFinished[5])
		{
			stepsFinished[5] = true;
		}
		
		if (stepsFinished[5] && !stepsFinished[6])
		{
			PlayerPosition.y -= Time.deltaTime * 0.5f;
			GameManager.Player.transform.position = PlayerPosition;
		}

		// step 6
		if (runTime > 11 && !stepsFinished[6])
		{
			GameManager.Player.GetComponent<Cinema>().setLookAtTarget(LookAtTarget3);
			stepsFinished[6] = true;
		}
		
		if (stepsFinished[6])
		{
			fadeoutProgress += Time.deltaTime * 1.0f;
			GameManager.cameraEffects.ChangeBlur(true);
			GameManager.cameraEffects.ChangeVignette(true);
			GameManager.cameraEffects.SetBlurIntensity(fadeoutProgress);
			GameManager.cameraEffects.SetVignetteIntensity(fadeoutProgress);
		}
		
		
		if (runTime > 15)
		{
			SceneManager.LoadScene("Scenes/Outro");
		}
		
		
	}
	
}
