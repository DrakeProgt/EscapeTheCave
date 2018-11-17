using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour {

	[SerializeField] bool tempSwitch; //delete as soon as GameManager is implemented
	[SerializeField] bool isLanternPlaced;




	// Use this for initialization
	void Start () {
		isLanternPlaced = false;
	}
	
	// Update is called once per frame
	void Update () {
		if( tempSwitch == true && isLanternPlaced == true )
			ActivateLights();
		if( tempSwitch == false && isLanternPlaced == false )
			DeactivateLights();
	}

	void ActivateLights() //activate Lights if lantern is placed on base
	{
		foreach( Light light in GetComponentsInChildren<Light>() )
		{
			light.enabled = true;
		}
	}

	void DeactivateLights() //deactivate Lights if lantern is removed from base
	{
		foreach( Light light in GetComponentsInChildren<Light>() )
		{
			light.enabled = false;
		}
	}
}
