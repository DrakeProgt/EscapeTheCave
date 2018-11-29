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
            && GameManager.focused.tag == "rotateable"
            && GameManager.focused == gameObject)
        {
            isRotating = true;
            prismPlatform.GetComponent<PrismPlatformController>().Rotate();
        }

        /**
        if (isRotating)
        {
            transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, targetRotation, moveSpeed * Time.deltaTime);
            if (transform.rotation == targetRotation)
            {
                isRotating = false;
                if (transform.parent.rotation == Quaternion.Euler(correctRotation))
                {
                    Debug.Log("CORRECT ROTATION");
                }
            }
        }
        else
        {
            targetRotation = transform.parent.rotation * Quaternion.Euler(0, 360 / 8, 0);
        }
    **/

    }
}
