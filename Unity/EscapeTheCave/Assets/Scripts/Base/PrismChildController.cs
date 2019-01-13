using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismChildController : MonoBehaviour, ControllerInterface {

    public bool isEnabled { set; get; }

    // Use this for initialization
    void Start () {
        isEnabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateLights()
    {
        transform.parent.parent.GetComponent<PrismController>().ActivateLights();
    }

    public void Enable()
    {
        isEnabled = true;
        transform.parent.parent.GetComponent<ControllerInterface>().Enable();
    }

    public ItemType GetItemType()
    {
        return ItemType.Prism;
    }
}
