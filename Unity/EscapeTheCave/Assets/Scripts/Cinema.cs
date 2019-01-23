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
				transform.LookAt(LookAtTarget.transform);
				FirstPersonCamera.transform.LookAt(LookAtTarget.transform);
			}
			
		}
		
	}

	private bool moveTo(Vector3 targetPosition, float speed)
	{
        Vector3 originPosition = transform.position;
        targetPosition.y = 0;
        originPosition.y = 0;
		
		float distance = Vector3.Distance(originPosition, targetPosition);
		if (distance < speed)
		{
			transform.gameObject.GetComponent<FirstPersonController>().cinematicVelocity = (targetPosition - originPosition).normalized * distance;
            if (distance < 1)
            {
                return true;
            }

			return false;
		}
		transform.gameObject.GetComponent<FirstPersonController>().cinematicVelocity = (targetPosition - originPosition).normalized * speed;
		return false;
	}
}
