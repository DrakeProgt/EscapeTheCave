using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

				public GameObject pivot;

				// Use this for initialization
				void Start () {
								Camera cam = Camera.main;
								for (int k=1;k<6;k++) {
												GameObject newCam = Instantiate<GameObject>(cam.gameObject);
												newCam.transform.SetParent(pivot.transform, false);
												newCam.transform.rotation = Quaternion.Euler(0, k * 60, 0);
												newCam.GetComponent<Camera>().rect = new Rect(0.16f * k, 0, 0.16f, 1f);
												Destroy(newCam.GetComponent<AudioListener>());
								}
		
				}
	
				// Update is called once per frame
				void Update () {
		
				}
}
