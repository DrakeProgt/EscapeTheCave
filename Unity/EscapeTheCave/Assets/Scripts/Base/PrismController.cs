using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismController : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
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
                if (child.name == "PrismPlatform")
                {
                    child.gameObject.GetComponent<PrismPlatformController>().Rotate();
                }
                else
                {
                    child.gameObject.GetComponent<StarLightController>().Rotate();
                }
            }
        }
    }
}
