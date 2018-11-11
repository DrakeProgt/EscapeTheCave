using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour {

	private bool IsAssignedToBase;

	[SerializeField] Light spotLight;
	private float originalSpotLightAngle;

	[SerializeField] bool tempSwitch; //delete as soon as GameManager is implemented

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( tempSwitch && IsAssignedToBase == false)
			AssignToBase();
		if( !tempSwitch && IsAssignedToBase == true )
			RemoveFromBase();
	}

	//Assigns lantern to base object. Call method when lantern is placed on base object
	void AssignToBase()
	{
		IsAssignedToBase = true;
		if(spotLight != null ) //TODO Implement fade in/out
		{
			originalSpotLightAngle = spotLight.spotAngle;
			spotLight.spotAngle = 3f;
		}
	}

	//removes object from base object. Call method when lantern is took from base object
	void RemoveFromBase()
	{
		IsAssignedToBase = false;
		if( spotLight != null )
		{
			spotLight.spotAngle = originalSpotLightAngle;
		}
	}
}
