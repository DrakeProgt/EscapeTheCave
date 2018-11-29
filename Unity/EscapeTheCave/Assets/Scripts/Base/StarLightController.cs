using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLightController : MonoBehaviour {

    [SerializeField] bool isRotating;
    int rotateIndex;
    Dictionary<int, Vector3> rotateValues;
    [SerializeField] Vector3[] temp;

	// Use this for initialization
	void Start () {
        rotateIndex = -1;
        isRotating = false;
        rotateValues = new Dictionary<int, Vector3>();

        for(int i = 0; i < temp.Length; i++)
        {
            rotateValues.Add(i, temp[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(isRotating)
        {
            Quaternion rotateValue = Quaternion.Euler(rotateValues[rotateIndex]);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateValue, 50 * Time.deltaTime);
            if (transform.rotation == rotateValue)
            {
                isRotating = false;
            }
        }
	}

    public void Rotate()
    {
        if (++rotateIndex >= rotateValues.Count)
        {
            rotateIndex = 0;
        }
        isRotating = true;
    }
}
