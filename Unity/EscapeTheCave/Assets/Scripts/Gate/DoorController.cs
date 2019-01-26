using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
    float timeElapsed, waitTimeOne, waitTimeTwo;
    bool isSoundPlayingFirstTime, isSoundPlayingSecondTime;

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

            if(timeElapsed > waitTimeOne + 5 && timeElapsed < waitTimeOne + 9)
            {
                if(!isSoundPlayingFirstTime)
                {
                    SoundSystem.PlayPedestalSound(gameObject);
                    isSoundPlayingFirstTime = true;
                }
                transform.Translate(transform.forward * -1 * Time.deltaTime);
            } else
            {
                isSoundPlayingFirstTime = false;
            }

            if(timeElapsed > waitTimeOne + 20 && timeElapsed < waitTimeOne + 24)
            {
                if (!isSoundPlayingSecondTime)
                {
                    SoundSystem.PlayPedestalSound(gameObject);
                    isSoundPlayingSecondTime = true;
                }
                transform.Translate(transform.forward * Time.deltaTime);
            }
            else
            {
                isSoundPlayingSecondTime = false;
            }
        }
	}
}
