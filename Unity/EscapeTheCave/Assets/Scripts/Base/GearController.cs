using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    [SerializeField] GameObject prismPlatform;
    float moveSpeed;
    public int index;
    bool isSoundPlaying;

    // Use this for initialization
    void Start()
    {
        moveSpeed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pressedInteractKey
            && GameManager.focused == gameObject
            && GameManager.focused.tag == "rotateable"
            && !transform.parent.gameObject.GetComponent<BaseController>().gearsRotating[index])
        {
            Debug.Log("Start rotating...");
            prismPlatform.GetComponent<PrismController>().Rotate();
        }
        
        if (transform.parent.gameObject.GetComponent<BaseController>().gearsRotating[index])
        {
            Debug.Log("Rotate gear...");
            if(!isSoundPlaying)
            {
                SoundSystem.PlayGearSound(gameObject, 0, .2f);
                isSoundPlaying = true;
            }
            transform.Rotate(Vector3.up * Time.deltaTime * 300);
        } else
        {
            isSoundPlaying = false;
        }
    }
}
