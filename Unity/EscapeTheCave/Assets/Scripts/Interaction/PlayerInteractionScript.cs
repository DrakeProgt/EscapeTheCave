using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    [SerializeField] GameObject crystal, lantern, prismLeft, prismRight, prismMiddle;

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
    private void FixedUpdate()
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
                    message = "Pick up " + hit.collider.gameObject.name;
                    PickUpObject(hit.collider.gameObject);
                    break;
                case "pressable":
                    message = "Press with Button E";
                    break;
                case "rotateable":
                    message = "Rotate " + hit.collider.gameObject.name;
                    break;
                case "CrystalPlatform":
                    message = "Set crystal into base";
                    PlaceObject(crystal);
                    break;
                case "LanternPlatform": 
                    message = "Set Lantern Into Base";
                    PlaceObject(lantern);
                    break;
                case "PrismLeftPlatform":
                    message = "Set Prism Into Base";
                    PlaceObject(prismLeft);
                    break;
                case "PrismRightPlatform":
                    message = "Set Prism Into Base";
                    PlaceObject(prismRight);
                    break;
                case "PrismMiddlePlatform":
                    message = "Set Prism Into Base";
                    PlaceObject(prismMiddle);
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

    private void PlaceObject(GameObject objToBePlaced)
    {
        if (Input.GetButtonDown("Interact"))
        {
            objToBePlaced.SetActive(true);
            if(objToBePlaced.CompareTag("Lantern") || objToBePlaced.CompareTag("Crystal"))
            {
                objToBePlaced.transform.parent.parent.GetComponent<BaseController>().ActivateLights();
            }
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