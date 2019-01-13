using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    [SerializeField] GameObject prismPlatform;
    bool isRotating;
    Quaternion targetRotation;
    float moveSpeed;

    // Use this for initialization
    void Start()
    {
        isRotating = false;
        moveSpeed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pressedInteractKey
            && GameManager.focused == gameObject
            && GameManager.focused.tag == "rotateable")
        {
            isRotating = true;
            prismPlatform.GetComponent<PrismController>().Rotate();
        }
        
        if (isRotating)
        {
            transform.GetChild(0).transform.Rotate(Vector3.up * 2);
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isRotating = false;
            }
        }
        else
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, 360 / 8, 0);
        }
    }
}
