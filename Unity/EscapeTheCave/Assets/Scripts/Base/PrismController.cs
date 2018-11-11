using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismController : MonoBehaviour {

    [SerializeField] float rotationSpeed;
    [SerializeField] bool isInteracted;

	// Use this for initialization
	void Start () {
        rotationSpeed = 90f;
	}
	
	// Update is called once per frame
	void Update () {
            transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
            isInteracted = false;
	}
}
