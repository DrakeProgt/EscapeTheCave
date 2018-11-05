using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffectsScript : MonoBehaviour
{
    private Light light;
    private float minWaitTime;
    private float maxWaitTime;
    private bool isFlickeingEnabled;

    // Use this for initialization
    private void Start()
    {
        light = gameObject.GetComponent<Light>();
        SetMaxWaitTime(0.5f);
        isFlickeingEnabled = false;
    }
	
	// Update is called once per frame
	private void Update()
    {
        //TODO: test
        if (Input.GetKeyDown(KeyCode.L))
        {
            isFlickeingEnabled = !isFlickeingEnabled;
            ChangeLightFlickering(isFlickeingEnabled);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(SetLightIntensitySmooth(0.2f));
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(SetLightIntensitySmooth(2f));
        }
    }

    private void ChangeLightFlickering(bool bActivate)
    {
        if (bActivate)
        {
            isFlickeingEnabled = true;
            StartCoroutine(RandomFlickering());
        }
        else
        {
            isFlickeingEnabled = false;
            StopCoroutine(RandomFlickering());
            light.enabled = true;
        }
    }

    private IEnumerator RandomFlickering()
    {
        while(isFlickeingEnabled)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            light.enabled = !light.enabled;
        }
        light.enabled = true;
    }

    private void SetMaxWaitTime(float time)
    {
        maxWaitTime = time * 1.3f;
        minWaitTime = time * 0.7f;

        //or:
        //maxWaitTime = time;
        //minWaitTime = time / 3;
    }

    private IEnumerator SetLightIntensitySmooth(float intensity)
    {
        bool increment = light.intensity < intensity;
        float step = 0.05f;
        float delta = Mathf.Abs(light.intensity - intensity);
        float steps = delta / step;
        while((increment && light.intensity < intensity) || (!increment && light.intensity > intensity))
        {
            //TODO: make time depends of delta? 
            //TODO: make it unpossible to run a new co-routine with StartCoroutine if another is already running!
            yield return new WaitForSecondsRealtime(1 / steps);
            if (increment)
                light.intensity += step;
            else
                light.intensity -= step;
        }
        light.intensity = intensity;
    }
}
