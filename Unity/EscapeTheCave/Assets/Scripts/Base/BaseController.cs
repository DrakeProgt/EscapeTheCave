using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] float movementTime;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] GameObject crystalChild, lanternChild;
    [SerializeField] GameObject[] prismChildren;
    [SerializeField] GameObject StarSign;

    public float correctRotations;

    float t;
    bool isMovedUp;

    // Use this for initialization
    void Start()
    {
        isMovedUp = true;
        t = 0;
        targetPosition = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
        movementTime = 20;
        correctRotations = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (correctRotations == 3) // TODO correctRotations == 11 for prod version
        {
            GameManager.isLightPuzzleSolved = true;
            StarSign.GetComponent<StarSignLineSystem>().buildUp();
        }
    }
}
