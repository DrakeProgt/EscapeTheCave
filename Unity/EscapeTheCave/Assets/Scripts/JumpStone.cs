using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStone : MonoBehaviour
{

	public GameObject parallelGroup;

	private GameObject area;
	private GameObject[] Cylinders = new GameObject[3];
	private Vector3[] positions = new Vector3[3];
	private Vector3[] rotations = new Vector3[3];
	private float startTime;
	private bool isActivated;
	private bool isRun;
	private float activateDistance = 2.0f;
	public static bool reset = false;
	
	private float waitTime = 2.0f;
	
	void Start ()
	{
		int i = 0;
		GameObject deadZone = transform.parent.Find("DeadZone").gameObject;
		
		foreach (Transform child in transform)
		{
			if ("Area" == child.gameObject.name)
			{
				area = child.gameObject;
			}
			else
			{
				Physics.IgnoreCollision(deadZone.GetComponent<Collider>(), child.gameObject.GetComponent<Collider>());
				Cylinders[i] = child.gameObject;
				positions[i] = child.localPosition;
				rotations[i] = child.localEulerAngles;
				i++;
			}
		}
		
		init();
	}

	public void init()
	{
		for (int i = 0; i < 3; i++)
		{
			Cylinders[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			Cylinders[i].transform.localPosition = positions[i];
			Cylinders[i].transform.localEulerAngles = rotations[i];
		}
		
		isActivated = false;
		isRun = false;
		JumpStone.reset = false;
	}

	public void fall()
	{
		foreach (GameObject cylinder in Cylinders)
		{
			cylinder.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		}	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.secondCaveReached && GameManager.Player != null)
		{
			float distance = Vector3.Distance(area.transform.position, GameManager.Player.transform.position);
//			distance = 1;
			if (!isActivated && distance < activateDistance)
			{
				startTime = Time.time;
				isActivated = true;
			}
	
			if (isActivated && !isRun && Time.time > (startTime + waitTime))
			{
				fall();
				isRun = true;
			}
		}

		if (JumpStone.reset)
		{
			StartCoroutine(DelayedReset(1));
		}
	}
	
	IEnumerator DelayedReset(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		init();
	}
}
