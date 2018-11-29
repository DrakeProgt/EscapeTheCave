using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour {
    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ActivateLights()
	{
		foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
	}
}
