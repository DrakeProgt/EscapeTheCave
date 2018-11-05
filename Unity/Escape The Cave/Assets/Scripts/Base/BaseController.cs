using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {

    public bool isInteracted = false;
    public float movementSpeed = 1f;

    Rigidbody rigidbody;
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float movementTime;
    float t;

	// Use this for initialization
	void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody>();

        startPosition = targetPosition = transform.position;
        targetPosition = new Vector3(0, 0.666f, 0);
        movementTime = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if(isInteracted)
        {
            t += Time.deltaTime / movementTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
        }
    }
}
