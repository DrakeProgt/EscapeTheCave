using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.isLightPuzzleSolved)
        {
            transform.Translate(transform.right * 1 * Time.deltaTime);
        }
	}
}
