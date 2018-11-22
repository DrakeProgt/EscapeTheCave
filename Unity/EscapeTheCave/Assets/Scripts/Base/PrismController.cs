using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismController : MonoBehaviour {

    [SerializeField] BaseController parentBase;
    [SerializeField] bool isInteracted;
    [SerializeField] float moveSpeed;
    [SerializeField] Quaternion myRotation, targetRotation;
    [SerializeField] Vector3 correctRotation;

	// Use this for initialization
	void Start ()
	{
        isInteracted = false;
        moveSpeed = 50;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(parentBase.GetIsMovedUp() && isInteracted)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
            if(transform.rotation == targetRotation)
            {
                isInteracted = false;
                if(transform.rotation == Quaternion.Euler(correctRotation))
                {
                    Debug.Log("CORRECT ROTATION");
                }
            }
        }
        else
        {
            targetRotation = transform.rotation * Quaternion.Euler(360 / 8, 0, 0);
        }
	}

	public void ActivateLights() 
	{
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
