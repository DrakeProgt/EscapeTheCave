using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSequence : MonoBehaviour
{

	private TriggerZone TargetArea;
	private GameObject Arrows;
	
	// Use this for initialization
	void Start ()
	{
		Arrows = transform.Find("PoisonArrows").gameObject;
		TargetArea = transform.Find("TriggerArea").gameObject.GetComponent<TriggerZone>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
