using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismController : MonoBehaviour, ControllerInterface {

    public bool isEnabled { set; get; }
    [SerializeField] GameObject crystal, lantern;

    // Use this for initialization
    void Start ()
	{
        isEnabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void ActivateLights() 
	{
        if (lantern.GetComponent<ControllerInterface>().isEnabled && crystal.GetComponent<ControllerInterface>().isEnabled)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void Enable()
    {
        isEnabled = true;
        ActivateLights();
    }

    public void Rotate()
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.activeSelf)
            {
                string name = child.name.Replace("Left", "").Replace("Middle", "").Replace("Right", "");
                if (name == "PrismPlatform")
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

    public ItemType GetItemType()
    {
        return ItemType.Prism;
    }
}
