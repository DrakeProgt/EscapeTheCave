using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    private bool hovered;
    private string message;
    private float textBoxWidth;
    private float textBoxHeight;

    // Use this for initialization
    private void Start()
    {
        hovered = false;
        message = "";
    }
	
	// Update is called once per frame
	private void Update()
    {
        RaycastHit hit;

        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); //or Input.mousePosition?
        if (Physics.Raycast(ray, out hit, 5))
        {
            //TODO: set width and height of textBox depending on message
            SetTextBoxDim(250, 20);

            GameManager.UnpressAllKeys();

            if (hit.collider.gameObject.tag != "Untagged")
            {
                GameManager.focused = hit.collider.gameObject;
                if (Input.GetKeyDown(KeyCode.E)) GameManager.pressedInteractKey = true;
                
            } 

            switch (hit.collider.gameObject.tag)
            {
                case "pickable":
                    message = "Pick up " + hit.collider.gameObject.name + " with Button E";
                    doPickingUpObject(hit.collider.gameObject);
                    break;
                case "placeable":
                    message = "Place " + hit.collider.gameObject.name + " with Button E";
                    doPlacingObject(hit.collider.gameObject);
                    break;
                case "pressable":
                    message = "Press with Button E";
                    break;
                case "rotateable":
                    message = "Rotate " + hit.collider.gameObject.name + " with Button E";
                    break;
                case "Untagged":
                    return;
                default:
                    break;
            }

            hovered = true;
        }
        else
        {
            hovered = false;
            GameManager.focused = null;
            GameManager.UnpressAllKeys();
        }
            
    }

    private void doPickingUpObject(GameObject pickedObject)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Do something...
            Destroy(pickedObject);
        }
    }

    private void doPlacingObject(GameObject placedObject)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Do something...
            placedObject.GetComponent<MeshRenderer>().enabled = true;
            placedObject.tag = "Untagged";
            //TODO: doesn't work...
            hovered = false;
        }
    }

    private void SetTextBoxDim(float width, float height)
    {
        textBoxWidth = width;
        textBoxHeight = height;
    }

    private void OnGUI()
    {
        if (hovered)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, textBoxWidth, textBoxHeight), message);
        }
    }
}
