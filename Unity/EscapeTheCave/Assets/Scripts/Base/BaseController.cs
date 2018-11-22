using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {


    [SerializeField] bool isInteracted;
    [SerializeField] float movementSpeed, movementTime;
    [SerializeField] Vector3 startPosition, targetPosition;
    [SerializeField] GameObject crystalChild, lanternChild;
    [SerializeField] GameObject[] prismChildren;

    float t;
    bool isMovedUp;

	// Use this for initialization
	void Start () {        
        targetPosition = new Vector3(0, 0.397f, 0);
        movementTime = 1;
        movementSpeed = 1;
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
        }
    }

    public void ActivateLights()
    {
        if (lanternChild.activeSelf && crystalChild.activeSelf)
        {
            lanternChild.GetComponent<LanternController>().ActivateLights();
            crystalChild.GetComponent<CrystalController>().ActivateLights();
            foreach (GameObject child in prismChildren)
            {
                child.GetComponent<PrismController>().ActivateLights();
            }
        }
    }

    void SetInteracted(bool isInteracted)
    {
        this.isInteracted = isInteracted;
    }

    public bool GetIsInteracted()
    {
        return isInteracted;
    }

    public bool GetIsMovedUp()
    {
        return isMovedUp;
    }
}
