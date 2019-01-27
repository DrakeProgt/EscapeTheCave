using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

	void Start () {
		StartCoroutine(DelayedStart(1));
	}
	
	IEnumerator DelayedStart(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Physics.IgnoreCollision(GameManager.Player.GetComponent<Collider>(), GetComponent<Collider>());
	}
	
	void Update () {
		if (GameManager.Player != null)
		{
			if (transform.gameObject.GetComponent<BoxCollider>().bounds.Contains(GameManager.Player.transform.position))
			{
				GameManager.Die();
			}
		}
	}
}
