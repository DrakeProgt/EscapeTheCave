using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Cinema : MonoBehaviour
{

	public bool start = false;
	public bool started = false;

	private GameObject FirstPersonCamera;

	public GameObject blackBars;
	public GameObject MoveToTarget;
	public float movementSpeed = 2;
	public GameObject LookAtTarget;

	private float LookToProgress = 0;
	// Use this for initialization
	void Start () {
		FirstPersonCamera = GameObject.Find("FirstPersonCharacter");
		deactivcate();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (start)
		{
			if (!started)
			{
				transform.gameObject.GetComponent<FirstPersonController>().cinematicMode = true;
				blackBars.SetActive(true);
				started = true;
				LookToProgress = 0;
			}
			
			if (MoveToTarget != null)
			{
				moveTo(MoveToTarget.transform.position, movementSpeed);
			}
			else
			{
				stopMoving();
			}
			
			if (LookAtTarget != null)
			{
				LookTo(LookAtTarget.transform.position);
			}
			
		}
		
	}

	public void setLookAtTarget(GameObject target)
	{
		LookAtTarget = target;
		LookToProgress = 0;
	}
	
	public void deactivcate()
	{
		start = false;
		started = false;
		transform.gameObject.GetComponent<FirstPersonController>().disableCinematicMode();
		blackBars.SetActive(false);
	}

	public void LookTo(Vector3 position)
	{
		
		/*
		 * Quaternion targetRotation = Quaternion.LookRotation(position - transform.position);
		FirstPersonCamera.transform.rotation = Quaternion.Lerp(FirstPersonCamera.transform.rotation, targetRotation, LookToProgress);
		
//		Vector3 currentLookAt = (FirstPersonCamera.transform.rotation * Vector3.forward * (position - transform.position).magnitude) + transform.position;
//		LookAt((targetRotation * Vector3.forward * (position - transform.position).magnitude) + transform.position);
		
		LookToProgress += Time.deltaTime * 0.1f;
		if (LookToProgress > 0.8f) LookToProgress = 0.8f;
		 */
		// TODO Refactor to Quaternion
		Vector3 currentPosition = FirstPersonCamera.transform.position;
		Vector3 currentLookAt = 
			(FirstPersonCamera.transform.rotation * Vector3.forward * (position - currentPosition).magnitude) + currentPosition;
		LookToProgress += Time.deltaTime * 0.1f;
		if (LookToProgress > 0.8f) LookToProgress = 0.8f;
		LookAt(Vector3.Lerp(currentLookAt, position, LookToProgress));
	}
	
	private void LookAt(Vector3 position)
	{
		Vector3 lookHorizontalDirection = new Vector3(position.x, transform.position.y, position.z);
		transform.LookAt(lookHorizontalDirection);
		FirstPersonCamera.transform.LookAt(position);
		FirstPersonCamera.transform.localEulerAngles = new Vector3(FirstPersonCamera.transform.localEulerAngles.x,0,0);
	}

	private bool moveTo(Vector3 target, float speed)
	{
		Vector3 twoDimensionalTarget = target;
		Vector3 twoDimensionalPosition = transform.position;
		twoDimensionalTarget.y = 0;
		twoDimensionalPosition.y = 0;
		
		float distance = Vector3.Distance(twoDimensionalPosition, twoDimensionalTarget);
		if (distance < speed)
		{
			transform.gameObject.GetComponent<FirstPersonController>().cinematicVelocity =
				(twoDimensionalTarget - twoDimensionalPosition).normalized * distance;
			if (distance < 0.3f) return true;
			return false;
		}
		transform.gameObject.GetComponent<FirstPersonController>().cinematicVelocity =
			(twoDimensionalTarget - twoDimensionalPosition).normalized * speed;
		return false;
	}

	private void stopMoving()
	{
		transform.gameObject.GetComponent<FirstPersonController>().cinematicVelocity = Vector3.zero;
	}
}
