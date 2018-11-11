using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {

    public bool isInteracted;
    public float movementSpeed;

    Rigidbody rigidbody;
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float movementTime;
    float t;

	// Use this for initialization
	void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        
        targetPosition = new Vector3(0, -0.3f, 0);
        movementTime = 1;
        isInteracted = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(isInteracted)
        {
            t += Time.deltaTime / movementTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, t);
        }
    }
}
