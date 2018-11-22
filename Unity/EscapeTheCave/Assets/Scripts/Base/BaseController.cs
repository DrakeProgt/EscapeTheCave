using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {

    public bool isInteracted;

    [SerializeField] float movementSpeed;
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float movementTime;

    float t;
    bool isMovedUp, isLanternPlaced, isCrystalPlaced;
    GameObject crystalChild, lanternChild;
    List<GameObject> prismChildren;

	// Use this for initialization
	void Start () {        
        targetPosition = new Vector3(0, 0.397f, 0);
        movementTime = 1;
        prismChildren = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.name == "Crystal")
            {
                crystalChild = child.gameObject;
            }
            if(child.name == "Lantern")
            {
                lanternChild = child.gameObject;
            }
            if(child.CompareTag("prism"))
            {
                prismChildren.Add(child.gameObject);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(isInteracted)
        {
            if(!isMovedUp)
            {
                t += Time.deltaTime / movementTime;
                transform.position = Vector3.Lerp(transform.position, targetPosition, t);

                if (transform.position == targetPosition)
                {
                    isMovedUp = true;
                    isInteracted = false;
                }
            }
            else
            {
            
                if (!isCrystalPlaced)
                {
                    crystalChild.SetActive(true);
                    isCrystalPlaced = true;
                    isInteracted = false;
                }
                else
                {

                    if (!isLanternPlaced)
                    {
                        lanternChild.SetActive(true);
                        isLanternPlaced = true;
                        crystalChild.GetComponent<CrystalController>().ActivateLights();

                        foreach(GameObject child in prismChildren)
                        {
                            child.GetComponent<PrismController>().ActivateLights();
                        }
                    }
                }
            }
        }
    }
}
