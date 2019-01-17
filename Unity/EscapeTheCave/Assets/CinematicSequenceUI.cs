using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicSequenceUI : MonoBehaviour {

    public bool isEnabled;
    float targetOne = 577, originOne = 677, targetTwo = 43.6f, originTwo = -56f;

    // Update is called once per frame
    void Update () {
		if(isEnabled)
        {
            transform.GetChild(0).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, targetTwo, 0);
            transform.GetChild(1).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, targetOne, 0);
        } else
        {
            transform.GetChild(0).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, originTwo, 0);
            transform.GetChild(1).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, originOne, 0);
        }
	}
}
