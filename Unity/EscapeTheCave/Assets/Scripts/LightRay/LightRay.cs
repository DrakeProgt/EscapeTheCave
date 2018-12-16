using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour {

    public GameObject CustomLightRaySystem;
    public GameObject CustomLightRay;
    public GameObject TargetPointLight;
    public float LightDistanceOffset;
    public bool ReactToCollider = true;

    public GameObject targetObject;
    public Vector3 TargetPosition;

    private Transform transform;
    private RaycastHit hit;
    private Vector3 endPosition; // if nothings interupts the light, this will be TargetPosition

    
    void Start () {
    }
	
	void Update () {
        // update origin position
        transform = CustomLightRaySystem.GetComponent<Transform>();

        if (targetObject != null)
        {
            TargetPosition = targetObject.GetComponent<Transform>().position;
        }

        // Rotate to target Position
        transform.LookAt(TargetPosition);

        //transform.localScale = new Vector3(1, 1, distance() / 2);

        //stretch Light Ray to target Position
        SetGlobalScale(new Vector3(1, 1, distance() / 2));

        // set point light to end position
        TargetPointLight.GetComponent<Transform>().position = endPosition;
    }

    float distance()
    {
        if(ReactToCollider && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            endPosition = hit.point + (transform.position - hit.point).normalized * LightDistanceOffset;
            return hit.distance;
        } else
        {
            endPosition = TargetPosition;
            return Vector3.Distance(TargetPosition, transform.position);
        }
    }

    private void SetGlobalScale(Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }
}
