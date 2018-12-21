using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDimMonster : MonoBehaviour
{

    [SerializeField] float distanceReact = 10.0f;
    //distanceReact = intensity
    //distance = new intensity

    private Light[] lights;
    private float[] originalIntensity;

    // Use this for initialization
    void Start()
    {
        lights = GetComponentsInChildren<Light>();
        originalIntensity = new float[lights.Length];

        for (int i = 0; i < originalIntensity.Length; i++)
        {
            originalIntensity[i] = lights[i].intensity;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 monsterPosition = GameManager.monsterPosition;

        for (int i = 0; i < lights.Length; i++)
        {
            float distance = Vector3.Distance(lights[i].transform.position, monsterPosition);
            if (distance < distanceReact)
            {
                lights[i].intensity = (distance * originalIntensity[i]) / distanceReact;
            }
        }
    }
}
