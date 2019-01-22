using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDimMonster : MonoBehaviour
{

    private float distanceReact = 12.0f;
    private float distanceBlack = 5.0f;

    private Light light;
    private float originalIntensity;
    private Color originalColor;
    private float originalRange;
    private Color deltaColor;
    private float deltaOriginalColor = 0.1f;
    private float deltaSecondColor = 0.1f;
    private float switchDuration = 5;
    private float intensityDuration = 5;
    private float intensityDim = 0.35f;

    // Use this for initialization
    void Start()
    {
        light = GetComponentInChildren<Light>();
        originalIntensity = light.intensity;
        originalRange = light.range;
        originalColor = CalculateRandomColor(light.color, deltaOriginalColor);
        deltaColor = CalculateRandomColor(originalColor, deltaSecondColor);
        switchDuration = Random.Range(7.0f, 30.0f);
        intensityDuration = Random.Range(5.0f, 15.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, GameManager.monsterPosition);
        float monsterDim = 1.0f;
        float timeDim = 1.0f;
        if (distance < distanceReact)
        {
            if (distance < distanceBlack)
            {
                monsterDim = 0.0f;
            }
            else
            {
                monsterDim = (distance - distanceBlack) / (distanceReact - distanceBlack);
            }
//            Debug.Log("monsterDim: " + monsterDim + " | " + distance);
        }
        
        timeDim = 1.0f - (Mathf.PingPong(Time.time, intensityDuration) / intensityDuration) * intensityDim;

        light.intensity = originalIntensity * monsterDim * timeDim;
        light.range = originalRange * monsterDim * timeDim;
        
        float t = Mathf.PingPong(Time.time, switchDuration) / switchDuration;
        light.color = Color.Lerp(originalColor, deltaColor, t);
    }

    private Color CalculateRandomColor(Color originColor, float randomDelta)
    {
        Color newInstance = new Color();
//        Debug.Log("Old Color: " + originColor.r + " | " + originColor.g + " | " + originColor.b + " | ");
        newInstance.r = originColor.r + ((Random.Range(0.0f, 1.0f) * (2 * randomDelta) - randomDelta));
        newInstance.g = originColor.g + ((Random.Range(0.0f, 1.0f) * (2 * randomDelta) - randomDelta));
        newInstance.b = originColor.b + ((Random.Range(0.0f, 1.0f) * (2 * randomDelta) - randomDelta));
//        Debug.Log("New Color: " + newInstance.r + " | " + newInstance.g + " | " + newInstance.b + " | ");

        return newInstance;
    }
}
