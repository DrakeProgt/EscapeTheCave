using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorToggle : MonoBehaviour
{
	public bool showCursor = true;
	// Use this for initialization
	void Start () {
		Cursor.visible = showCursor;
		Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
	}


}
