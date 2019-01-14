using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalBaseController : MonoBehaviour {

    [SerializeField] Vector3 targetPosition;
    [SerializeField] GameObject[] pedestalGates;
    public bool isPedestalMovedUp, isGateOpened;

	// Use this for initialization
	void Start () {
        isPedestalMovedUp = false;
        isGateOpened = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.isWordPuzzleSolved)
        {
            return;
        }

        if(!isGateOpened)
        {
            foreach(GameObject pedestalGate in pedestalGates)
            {
                pedestalGate.transform.Translate(Vector3.forward * -.5f * Time.deltaTime);
                if(pedestalGate.transform.position.z < -3.5f)
                {
                    isGateOpened = true;
                }
            }
        } else if (!isPedestalMovedUp)
        {
            transform.Translate(Vector3.up * .5f * Time.deltaTime);

            if (transform.position.y >= -3.6f)
            {
                isPedestalMovedUp = true;
            }
        }
    }
}
