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
        SetWaitTime(0.5f);
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
            StartCoroutine(RandomLightFlickering());
        }
        else
        {
            isFlickeingEnabled = false;
            StopCoroutine(RandomLightFlickering());
        }
    }

    private IEnumerator RandomLightFlickering()
    {
        float intensity = GetLightIntensity();

        //TODO: set intensity smooth?
        while (isFlickeingEnabled)
        {
            //use random factor for more realistic result
            //use half of waiting time because of the two-time wait
            SetLightIntensity(intensity * 0.6f);
            yield return new WaitForSecondsRealtime(Random.Range(minWaitTime / 2, maxWaitTime / 2));
            SetLightIntensity(intensity);
            yield return new WaitForSecondsRealtime(Random.Range(minWaitTime / 2, maxWaitTime / 2));
        }

        SetLightIntensity(intensity);
    }

    private void SetWaitTime(float time)
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

    private void SetLightIntensity(float intensity)
    {        
        light.intensity = intensity;
    }

    private float GetLightIntensity()
    {
        return light.intensity;
    }
}
