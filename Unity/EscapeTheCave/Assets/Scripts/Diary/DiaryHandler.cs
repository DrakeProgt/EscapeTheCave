using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryHandler : MonoBehaviour {

Animator m_Animator;
private bool opened;

    // Use this for initialization
    void Start () {

		m_Animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.O)){
			opened = m_Animator.GetBool("DiaryOpened");
			if(opened){
				opened = false;
			}else{
				opened = true;
			}
			m_Animator.SetBool("DiaryOpened",opened);
		}


	}
}
