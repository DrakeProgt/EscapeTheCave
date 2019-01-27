using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    [SerializeField] GameObject prismLeft, prismMiddle;

    [SerializeField] GameObject inventoryObject;
    Inventory inventory;

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
        inventory = inventoryObject.GetComponent<Inventory>();
        StartCoroutine(DelayedStart(1));
        GameManager.Player = gameObject;
        GameManager.cameraEffects = transform.Find("FirstPersonCharacter").GetComponent<CameraEffectsScript>();
        GameManager.LoadCurrentLevel();
    }

    IEnumerator DelayedStart(float waitTime)
    {
        // suspend execution for waitTime seconds
        yield return new WaitForSeconds(waitTime);
        if (!GameManager.isLightPuzzleSolved)
        {
            inventory.AddItemToInventory(prismLeft, prismLeft.GetComponent<ControllerInterface>().GetItemType());
            inventory.AddItemToInventory(prismMiddle, prismMiddle.GetComponent<ControllerInterface>().GetItemType());
        }
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
                    message = GameManager.hoverMessage;
                    break;
                case "placeable":
                    string itemName = hit.collider.gameObject.name.Replace("Platform", "");
                    itemName = itemName.Replace("Left", "").Replace("Right", "").Replace("Middle", "");
                    bool place = true;
                    foreach (Transform child in hit.collider.gameObject.transform)
                    {
                        if (child.name.Replace("Left", "").Replace("Right", "").Replace("Middle", "") == itemName)
                        {
                            place = false;
                        }
                    }

                    if (place)
                    {
                        message = "Place " + itemName;
                        PlaceObject(hit.collider.gameObject);
                    }
                    else
                    {
                        message = "";
                        GameManager.hoverMessage = null;
                        hovered = false;
                        GameManager.focused = null;
                    }

                    break;
                case "rotateable":
                    message = "Rotate " + hit.collider.gameObject.name;
                    break;
                default:
                    message = null;
                    break;
            }

            if (!String.IsNullOrEmpty(message))
            {
                //TODO: set width and height of textBox depending on message
                SetTextBoxDim(250, 20);
                hovered = true;
            }
        }
        else
        {
            GameManager.hoverMessage = null;
            hovered = false;
            GameManager.focused = null;
        }
            
    }

    private void PlaceObject(GameObject platform)
    {
        if (GameManager.pressedInteractKey && inventory.GetSelectedItem().name == platform.name.Replace("Platform", ""))
        {
            GameObject obj = inventory.RemoveAndGetSelectedItemFromInventory();
            Transform t = platform.transform.GetChild(0);
            Vector3 targetPosition = t.position;
            t.gameObject.SetActive(false);

            obj.SetActive(true);
            obj.transform.parent = platform.transform;
            if (!obj.name.Contains("Prism"))
            {
                obj.transform.localRotation = Quaternion.identity;
            }

            Vector3 startPosition = transform.position;

            StartCoroutine(MoveObjectAway(obj, startPosition, targetPosition, .6f));
        }
    }

    private void PickUpObject(GameObject pickedObject)
    {
        if (GameManager.pressedInteractKey)
        {
            pickedObject.GetComponent<BoxCollider>().enabled = false;

            inventory.AddItemToInventory(pickedObject, pickedObject.GetComponent<ControllerInterface>().GetItemType());

            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
            Vector3 startPosition = pickedObject.transform.position;

            StartCoroutine(MoveObjectTowards(pickedObject, startPosition, targetPosition, .6f));
        }
    }

    IEnumerator MoveObjectTowards(GameObject obj, Vector3 source, Vector3 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            obj.transform.position = Vector3.Slerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        obj.transform.position = target;
        obj.SetActive(false);
    }

    IEnumerator MoveObjectAway(GameObject obj, Vector3 source, Vector3 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            obj.transform.position = Vector3.Slerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        obj.transform.position = target;
        obj.GetComponent<ControllerInterface>().Enable();
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