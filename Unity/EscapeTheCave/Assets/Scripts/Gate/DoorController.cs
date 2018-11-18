using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    [SerializeField] bool isInteracted;

	// Use this for initialization
	void Start () {
        isInteracted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isInteracted)
        {
            transform.RotateAround(transform.position - new Vector3(3, 0, 0), transform.up, -45 * Time.deltaTime);
        }
	}
}
