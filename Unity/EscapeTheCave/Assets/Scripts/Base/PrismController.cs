using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismController : MonoBehaviour {

    Dictionary<int, Vector3> rotateValues;

	// Use this for initialization
	void Start ()
	{
        rotateValues = new Dictionary<int, Vector3>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void ActivateLights() 
	{
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void Rotate()
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.activeSelf)
            {
                child.gameObject.GetComponent<StarLightController>().Rotate();
            }
        }
    }
}
