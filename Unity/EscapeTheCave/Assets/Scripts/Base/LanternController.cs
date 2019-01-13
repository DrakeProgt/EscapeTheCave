using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour, ControllerInterface {

    public bool isEnabled { set; get; }
    [SerializeField] GameObject crystal;
    [SerializeField] GameObject[] prisms;

    // Use this for initialization
    void Start ()
	{
        isEnabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
        
    }

    public void Enable()
    {
        isEnabled = true;
        if(crystal.GetComponent<ControllerInterface>().isEnabled)
        {
            crystal.GetComponent<CrystalController>().ActivateLights();
        }
        foreach(GameObject prism in prisms)
        {
            if(prism.GetComponent<ControllerInterface>().isEnabled)
            {
                prism.GetComponent<PrismChildController>().ActivateLights();
            }
        }
    }

    public ItemType GetItemType()
    {
        return ItemType.Lantern;
    }
}
