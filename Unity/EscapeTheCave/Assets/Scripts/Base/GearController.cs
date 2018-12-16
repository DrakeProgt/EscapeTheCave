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

    }
}
