using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismPlatformController : MonoBehaviour {

    bool isRotating;
    Quaternion targetRotation;
    float moveSpeed;
    [SerializeField] int rotateIndex, correctRotation;


	// Use this for initialization
	void Start () {
        moveSpeed = 50;
        rotateIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
            if (transform.rotation == targetRotation)
            {
                isRotating = false;

                if (gameObject.activeSelf && rotateIndex == correctRotation)
                {
                    transform.parent.parent.gameObject.GetComponent<BaseController>().correctRotations++;
                }
            }
        }
        else
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, 0, 360 / 8);
        }
    }

    float EqualVecs(Vector3 lhs, Vector3 rhs)
    {
        return Mathf.Abs((lhs.x - rhs.x) + (lhs.y - rhs.y) + (lhs.z - lhs.z));
    }

    public void Rotate()
    {
        if(!isRotating)
        {
            if (++rotateIndex >= 8)
            {
                rotateIndex = 0;
            }
            isRotating = true;
        }
    }
}
