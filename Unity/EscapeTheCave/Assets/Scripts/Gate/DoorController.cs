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
            StartCoroutine(MoveAside());
        }
	}

    IEnumerator MoveAside()
    {
        yield return new WaitForSeconds(2);
        transform.Translate(transform.forward * -1 * Time.deltaTime);
    }
}
