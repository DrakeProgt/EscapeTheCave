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
                    message = "Pick up " + hit.collider.gameObject.name;
                    DoPickingUpObject(hit.collider.gameObject);
                    break;
                case "rotateable":
                    message = "Rotate " + hit.collider.gameObject.name;
                    break;
                case "CrystalFrame":
                    message = "Set crystal into base";
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

    private void DoPlacingObject(GameObject objToBePlaced, GameObject parent)
    {
        if (Input.GetButtonDown("Interact"))
        {
            objToBePlaced.GetComponent<BoxCollider>().enabled = false;
            //put the item over the target
            Vector3 position = parent.GetComponent<Transform>().position;
            position.y += 1;
            GameObject placedObject = GameObject.Instantiate(objToBePlaced, position, new Quaternion());

            //move the gameObject slowly
            MoveSample animation = placedObject.GetComponent<MoveSample>();
            animation.MoveAnimation(parent.GetComponent<Transform>().position);
            StartCoroutine(animation.EnableCollider(2));

            Destroy(parent);
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