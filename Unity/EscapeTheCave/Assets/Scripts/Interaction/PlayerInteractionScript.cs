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
    private void FixedUpdate()
    {
        RaycastHit hit;

        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); //or Input.mousePosition?
        if (Physics.Raycast(ray, out hit, 5))
        {
            //TODO: set width and height of textBox depending on message
            SetTextBoxDim(250, 20);

            switch (hit.collider.gameObject.tag)
            {
                case "pickable":
                    message = "Pick up " + hit.collider.gameObject.name + " with Button E";
                    DoPickingUpObject(hit.collider.gameObject);
                    break;
                case "rotateable":
                    message = "Rotate " + hit.collider.gameObject.name + " with Button E";
                    break;
                case "CrystalFrame":
                    message = "Set crystal into base with Button E";
                    DoPlacingObject((GameObject)Resources.Load("Crystal"), hit.collider.gameObject);
                    break;
                case "Untagged":
                    return;
                default:
                    break;
            }

            hovered = true;
        }
        else
            hovered = false;
    }

    private void DoPickingUpObject(GameObject pickedObject)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            pickedObject.GetComponent<BoxCollider>().enabled = false;

            //move the gameObject slowly
            MoveSample animation = pickedObject.GetComponent<MoveSample>();
            animation.MoveAnimation(gameObject.GetComponent<Transform>().position);
            StartCoroutine(animation.DestroyObject(2));

            //TODO: add item to inventory
        }
    }

    private void DoPlacingObject(GameObject objToBePlaced, GameObject parent)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            objToBePlaced.GetComponent<BoxCollider>().enabled = false;
            GameObject placedObject = GameObject.Instantiate(objToBePlaced, gameObject.GetComponent<Transform>().position, new Quaternion());

            //move the gameObject slowly
            MoveSample animation = placedObject.GetComponent<MoveSample>();
            animation.MoveAnimation(parent.GetComponent<Transform>().position);
            StartCoroutine(animation.EnableCollider(2));

            parent.tag = "Untagged";
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