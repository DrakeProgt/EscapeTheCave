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
            transform.Translate(transform.right * 1 * Time.deltaTime);
        }
	}
}
