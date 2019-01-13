using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLightController : MonoBehaviour
{

    [SerializeField] bool isRotating;
    int rotateIndex;
    [SerializeField] GameObject targetRotations;
    Vector3[] rotations;
    [SerializeField] int[] validRotations;


    // Use this for initialization
    void Start()
    {
        rotations = new Vector3[8];
        for (int i = 0; i < validRotations.Length; i++)
        {
            rotations[i] = targetRotations.transform.GetChild(validRotations[i] - 1).position;
        }
        rotateIndex = 0;
        isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            Vector3 direction = (rotations[rotateIndex] - transform.position).normalized;
            Quaternion rotateValue = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, rotateValue, 4 * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, rotateValue) < .01f)
            {
                Debug.Log("ROTATING IS FALSE");
                isRotating = false;
            }
        }
    }

    public void ActivateHaloLights()
    {
        Debug.Log("ACTIVATING HALO LIGHTS!!!");
        if (targetRotations.transform.GetChild(validRotations[rotateIndex] - 1).GetComponent<Light>() != null)
            targetRotations.transform.GetChild(validRotations[rotateIndex] - 1).GetComponent<Light>().enabled = true;
    }

    public void Rotate()
    {
        if (isRotating) return;

        if (targetRotations.transform.GetChild(validRotations[rotateIndex] - 1).GetComponent<Light>() != null)
            targetRotations.transform.GetChild(validRotations[rotateIndex] - 1).GetComponent<Light>().enabled = false;

        if (++rotateIndex >= rotations.Length)
        {
            rotateIndex = 0;
        }
        isRotating = true;
    }
}
