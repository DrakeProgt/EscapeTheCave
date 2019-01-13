using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour, ControllerInterface {

    public bool isEnabled { set; get; }
    [SerializeField] GameObject lantern;
    [SerializeField] GameObject[] prisms;

    // Use this for initialization
    void Start () {
        isEnabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Enable()
    {
        isEnabled = true;
        ActivateLights();
    }

	public void ActivateLights()
	{
        if (lantern.GetComponent<ControllerInterface>().isEnabled)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

            foreach (GameObject prism in prisms)
            {
                if (prism.GetComponent<ControllerInterface>().isEnabled)
                {
                    prism.GetComponent<PrismChildController>().ActivateLights();
                }
            }
        }
    }

    public ItemType GetItemType()
    {
        return ItemType.Crystal;
    }
}
