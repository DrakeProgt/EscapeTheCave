using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismPlatformController : MonoBehaviour {

    bool isRotating;
    Quaternion targetRotation;
    [SerializeField] Vector3 correctRotation;
    float moveSpeed;
    [SerializeField] GameObject prism;

	// Use this for initialization
	void Start () {
        moveSpeed = 50;
	}
	
	// Update is called once per frame
	void Update () {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
            if (transform.rotation == targetRotation)
            {
                isRotating = false;
                Debug.Log(transform.eulerAngles);
                if (EqualVecs(transform.eulerAngles, correctRotation) < 1)
                {
                    Debug.Log("CORRECT");
                    transform.parent.gameObject.GetComponent<BaseController>().correctRotations++;
                }
                else
                {
                    transform.parent.gameObject.GetComponent<BaseController>().correctRotations = 0;
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
            isRotating = true;
            if(prism.activeSelf)
            {
                prism.GetComponent<PrismController>().Rotate();
            }
        }
    }
}
