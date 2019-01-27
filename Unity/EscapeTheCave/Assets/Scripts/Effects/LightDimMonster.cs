using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDimMonster : MonoBehaviour
{

    private float distanceReact = 15.0f;
    private float distanceBlack = 8.0f;

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

    private bool active = true;
    
    private float activateDistance = 50; // disable Lights, which are too far away from player
    
    // Use this for initialization
    void Start()
    {
        light = GetComponentInChildren<Light>();
        if (null != light && light.isActiveAndEnabled)
        {
            originalIntensity = light.intensity;
            originalRange = light.range;
            originalColor = CalculateRandomColor(light.color, deltaOriginalColor);
            deltaColor = CalculateRandomColor(originalColor, deltaSecondColor);
            switchDuration = Random.Range(7.0f, 30.0f);
            intensityDuration = Random.Range(5.0f, 15.0f);
        }
        else
        {
            active = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;
        float distanceToPlayer = Vector3.Distance(transform.position, GameManager.Player.transform.position);
        if (distanceToPlayer > activateDistance)
        {
            light.enabled = false;
            return;
        }
        else
        {
            light.enabled = true;
        }

        float monsterDim = monsterDimCalculation(GameManager.monsterPosition);
        
        foreach (var monsterZone in GameManager.monsterZones)
        {
            if (monsterZone.active) monsterDim *= monsterDimCalculation(monsterZone.Monster.transform.position);
        }
        
        float timeDim = 1.0f - (Mathf.PingPong(Time.time, intensityDuration) / intensityDuration) * intensityDim;

        light.intensity = originalIntensity * monsterDim * timeDim;
        light.range = originalRange * monsterDim * timeDim;
        
        float t = Mathf.PingPong(Time.time, switchDuration) / switchDuration;
        light.color = Color.Lerp(originalColor, deltaColor, t);
    }

    private float monsterDimCalculation(Vector3 position)
    {
        float distance = Vector3.Distance(transform.position, position);
        if (distance < distanceReact)
        {
            if (distance < distanceBlack)
            {
                return 0.0f;
            }
            else
            {
                return (distance - distanceBlack) / (distanceReact - distanceBlack);
            }
//            Debug.Log("monsterDim: " + monsterDim + " | " + distance);
        }

        return 1;
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
