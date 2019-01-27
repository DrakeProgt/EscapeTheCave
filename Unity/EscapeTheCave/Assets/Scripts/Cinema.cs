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
	public GameObject target;
	public GameObject LookAtTarget;

	// Use this for initialization
	void Start () {
		FirstPersonCamera = GameObject.Find("FirstPersonCharacter");
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
			}

			if (moveTo(target.transform.position, 2f))
			{
				start = false;
				started = false;
				transform.gameObject.GetComponent<FirstPersonController>().disableCinematicMode();
				blackBars.SetActive(false);
			}
			else
			{
				LookAt(LookAtTarget.transform.position);
			}
			
		}
		
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
}
