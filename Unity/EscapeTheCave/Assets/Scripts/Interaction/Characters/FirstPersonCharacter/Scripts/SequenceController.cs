using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceController: MonoBehaviour {
    
    [SerializeField] GameObject positionTarget, lookTarget;
	
	// Update is called once per frame
	void Update () {
		if(GameManager.isWordPuzzleSolved)
        {
            StartCoroutine(StartFirstSequence());
        }
	}

    IEnumerator StartFirstSequence()
    {
        gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        float startTime = Time.time;
        float overTime = 50000;
        while (Time.time < startTime + overTime)
        {
            transform.position = Vector3.Lerp(transform.position, positionTarget.transform.position, (Time.time - startTime) / overTime);
            transform.GetChild(0).rotation = Quaternion.identity;

            yield return null;
        }
    }
}
