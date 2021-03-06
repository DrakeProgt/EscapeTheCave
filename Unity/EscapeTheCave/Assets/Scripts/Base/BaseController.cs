﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] float movementTime;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] GameObject[] prismChildren;

    public bool[] gearsSolved = { false, false, false };
    public bool[] gearsRotating = { false, false, false };

    float t;
    bool isMovedUp;

    // Use this for initialization
    void Start()
    {
        isMovedUp = true;
        t = 0;
        targetPosition = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
        movementTime = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (gearsSolved[0] && gearsSolved[1] && gearsSolved[2])
        {
            Debug.Log("LightPuzzle solved!");
            GameManager.isLightPuzzleSolved = true;
            foreach(GameObject prism in prismChildren)
            {
                foreach(Transform child in prism.transform)
                {
                    if(child.name.Contains("Platform")) continue;
                    child.gameObject.GetComponent<StarLightController>().ActivateHaloLights();
                }
            }
        }
    }
}
