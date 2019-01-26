using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
    float timeElapsed, waitTimeOne, waitTimeTwo;

	// Use this for initialization
	void Start () {
        waitTimeOne = 0;
        waitTimeTwo = 0;
        timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.isLightPuzzleSolved)
        {
            timeElapsed += Time.deltaTime;
            if(waitTimeOne == 0)
            {
                waitTimeOne = timeElapsed;
            }

            if(timeElapsed > waitTimeOne + 2 && timeElapsed < waitTimeOne + 6)
            {
                transform.Translate(transform.forward * -1 * Time.deltaTime);
            }

            if(timeElapsed > waitTimeOne + 18 && timeElapsed < waitTimeOne + 22)
            {
                transform.Translate(transform.forward * Time.deltaTime);
            }
        }
	}

    IEnumerator MoveAside()
    {
        yield return new WaitForSeconds(2);
        transform.Translate(transform.forward * -1 * Time.deltaTime);
    }
}
