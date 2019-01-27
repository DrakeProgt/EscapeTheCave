using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just for the dead animation

public class JumpStoneGroup : MonoBehaviour
{
	
	private float deadAnimationDuration = 6;
	private float CameraSequenceStartTime;
	private bool isAnimationRunning;
	private float progress = 0;
	private Vector3 playerPosition;
	private TriggerZone triggerZone;
	// Use this for initialization
	void Start ()
	{
		triggerZone = transform.Find("DeadZone").gameObject.GetComponent<TriggerZone>();
		isAnimationRunning = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (triggerZone.isTriggered)
		{
			if (!isAnimationRunning)
			{
				isAnimationRunning = true;
				CameraSequenceStartTime = Time.time;
				progress = 0;
				playerPosition = GameManager.Player.transform.position + Vector3.up * 0.3f;
				GameManager.Player.GetComponent<Collider>().enabled = false;
				GameManager.Player.GetComponent<Cinema>().LookAtTarget = transform.Find("DeadZone").gameObject;
				GameManager.Player.GetComponent<Cinema>().start = true;
				playScream();
			}
		}

		if (isAnimationRunning)
		{
			progress += Time.deltaTime * 0.1f;
			
			GameManager.cameraEffects.ChangeBlur(true);
			GameManager.cameraEffects.ChangeVignette(true);
			GameManager.cameraEffects.SetBlurIntensity(progress);
			GameManager.cameraEffects.SetVignetteIntensity(progress);
			
			GameManager.Player.transform.position = playerPosition + Vector3.up * progress * 20;
			if (deadAnimationDuration < (Time.time - CameraSequenceStartTime))
			{
				GameManager.Player.GetComponent<Cinema>().deactivcate();
				triggerZone.isTriggered = false;
				isAnimationRunning = false;
				GameManager.Player.GetComponent<Collider>().enabled = true;
				GameManager.Die();
			}
		}
	}

	void playScream()
	{
		SoundSystem.PlaySound("Audio/Player/Scream/Scream_04", 0, 1, 10, 0, GameManager.Player);
	}
}
