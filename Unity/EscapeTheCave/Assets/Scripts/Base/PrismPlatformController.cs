using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismPlatformController : MonoBehaviour
{

    bool isRotating;
    Quaternion targetRotation;
    float moveSpeed;
    int prismIndex;
    [SerializeField] int rotateIndex, correctRotation;


    // Use this for initialization
    void Start()
    {
        moveSpeed = 50;
        rotateIndex = 0;

        prismIndex = transform.parent.GetComponent<PrismController>().prismIndex;

        // for Dev Start 
        if (rotateIndex == correctRotation)
        {
            transform.parent.parent.gameObject.GetComponent<BaseController>().gearsSolved[prismIndex] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isRotating = false;
                transform.parent.parent.gameObject.GetComponent<BaseController>().gearsRotating[prismIndex] = false;
                //Debug.Log("is " + rotateIndex + " and should be " + correctRotation);
            }
        }
        else
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, 0, 360 / 8);
        }

        if (gameObject.activeSelf && rotateIndex == correctRotation)
        {
            Debug.Log("Correct Rotation!!!");
            transform.parent.parent.gameObject.GetComponent<BaseController>().gearsSolved[prismIndex] = true;
        }
        else
        {
            transform.parent.parent.gameObject.GetComponent<BaseController>().gearsSolved[prismIndex] = false;
        }
    }

    float EqualVecs(Vector3 lhs, Vector3 rhs)
    {
        return Mathf.Abs((lhs.x - rhs.x) + (lhs.y - rhs.y) + (lhs.z - lhs.z));
    }

    public void Rotate()
    {
        Debug.Log("Rotating PrismPlatform...");
        if (++rotateIndex >= 8)
        {
            rotateIndex = 0;
        }
        isRotating = true;
        transform.parent.parent.gameObject.GetComponent<BaseController>().gearsRotating[prismIndex] = true;

    }
}
