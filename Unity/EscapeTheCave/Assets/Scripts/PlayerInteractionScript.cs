using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    private bool hovered;

    // Use this for initialization
    private void Start()
    {
        hovered = false;
    }
	
	// Update is called once per frame
	private void Update()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); //or Input.mousePosition?
        if (Physics.Raycast(ray, out hit, 5) && hit.collider.gameObject.tag == "pickable")
        {
            hovered = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Do something...
                Destroy(hit.collider.gameObject);
            }
        }
        else
            hovered = false;
    }

    private void OnGUI()
    {
        if (hovered)
        {
            GUI.Box(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 50, 150, 20), "Bitte 'E' drücken");
        }
        
        //Interaction point
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 5, 5), "");
    }
}
