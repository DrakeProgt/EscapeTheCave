using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] float movementTime;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] GameObject crystalChild, lanternChild;
    [SerializeField] GameObject[] prismChildren;

    public float correctRotations;

    float t;
    bool isMovedUp;

    // Use this for initialization
    void Start()
    {
        isMovedUp = true;
        t = 0;
        targetPosition = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
        movementTime = 20;
        correctRotations = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isWordPuzzleSolved)
        {
            return;
        }

        if (GameManager.pressedInteractKey && GameManager.focused && System.Array.IndexOf(new string[] { "PrismPlatform", "CrystalPlatform", "LanternPlatform" }, GameManager.focused.name) > -1)
        {
            GameManager.focused.transform.GetChild(0).gameObject.SetActive(true);
            if (lanternChild.activeSelf)
            {
                lanternChild.GetComponent<LanternController>().ActivateLights();

                if (crystalChild.activeSelf)
                {
                    crystalChild.GetComponent<CrystalController>().ActivateLights();
                    foreach (GameObject child in prismChildren)
                    {
                        child.GetComponent<PrismController>().ActivateLights();
                    }
                }
            }
        }


        if (correctRotations == 3) // TODO correctRotations == 11 for prod version
        {
            GameManager.isLightPuzzleSolved = true;
        }
    }
}
