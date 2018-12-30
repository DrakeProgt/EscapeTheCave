using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    [SerializeField] GameObject inventory;

    private bool hovered;
    private string message;
    private float textBoxWidth;
    private float textBoxHeight;
    private int layerMask = 1 << 8; // Interaction Layer Only

    // Use this for initialization
    private void Start()
    {
        hovered = false;
        message = "";
    }

    // Update is called once per frame
    private void Update()
    {
        // pressed buttons
        GameManager.UnpressAllKeys();
        
        // TODO: this are just test keys, need to be corrected
        if (Input.GetKeyDown(KeyCode.E)) GameManager.pressedInteractKey = true;
        if (Input.GetKeyDown(KeyCode.O)) GameManager.pressedL1Key = true;
        if (Input.GetKeyDown(KeyCode.I)) GameManager.pressedR2Key = true;
        if (Input.GetKeyDown(KeyCode.U)) GameManager.pressedL2Key = true;

        // Raycast
        RaycastHit hit;

        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); //or Input.mousePosition?
        if (Physics.Raycast(ray, out hit, 5, layerMask))
        {
            //TODO: set width and height of textBox depending on message
            SetTextBoxDim(250, 20);

            if (hit.collider.gameObject.tag != "Untagged")
            {
                GameManager.focused = hit.collider.gameObject;
            } 

            switch (hit.collider.gameObject.tag)
            {
                case "pickable":
                    message = "Pick up " + hit.collider.gameObject.name;
                    PickUpObject(hit.collider.gameObject);
                    break;
                case "pressable":
                    message = "Press with Button E";
                    break;
                case "placeable": 
                    message = "Place " + hit.collider.gameObject.transform.GetChild(0).gameObject.name;
                    break;
                case "rotateable":
                    message = "Rotate " + hit.collider.gameObject.name;
                    break;
                default:
                    message = null;
                    break;
            }

            hovered = true;
        }
        else
        {
            hovered = false;
            GameManager.focused = null;
        }
            
    }

    private void PickUpObject(GameObject pickedObject)
    {
        if (Input.GetButtonDown("Interact"))
        {
            pickedObject.GetComponent<BoxCollider>().enabled = false;

            //move the gameObject slowly
            MoveSample animation = pickedObject.GetComponent<MoveSample>();
            animation.MoveAnimation(gameObject.GetComponent<Transform>().position);
            StartCoroutine(animation.DestroyObject(1.2f));

            //TODO: add item to inventory
        }
    }

    private void SetTextBoxDim(float width, float height)
    {
        textBoxWidth = width;
        textBoxHeight = height;
    }

    private void OnGUI()
    {
        if (GameManager.isGamePaused)
            return;

        if (hovered)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, textBoxWidth, textBoxHeight), message);
        }
    }
}