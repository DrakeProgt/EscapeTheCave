using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] float movementSpeed, movementTime;
    [SerializeField] Vector3 startPosition, targetPosition;
    [SerializeField] GameObject crystalChild, lanternChild;
    [SerializeField] GameObject[] prismChildren;

    public float correctRotations;

    float t;
    bool isMovedUp = true;

    // Use this for initialization
    void Start()
    {
        targetPosition = new Vector3(0, 0.397f, 0);
        movementTime = 1;
        movementSpeed = 1;
        correctRotations = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMovedUp)
        {
            // Move up
            t += Time.deltaTime / movementTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, t);

            if (transform.position == targetPosition)
            {
                isMovedUp = true;
            }
        }
        else
        {
            if (GameManager.pressedInteractKey && GameManager.focused && System.Array.IndexOf(new string[] { "PrismPlatform", "CrystalPlatform", "LanternPlatform" }, GameManager.focused.name) > -1)
            {
                GameManager.focused.transform.GetChild(0).gameObject.SetActive(true);
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
        }

        if(correctRotations == 3)
        {
            GameManager.isLightPuzzleSolved = true;
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

    public bool GetIsMovedUp()
    {
        return isMovedUp;
    }
}
