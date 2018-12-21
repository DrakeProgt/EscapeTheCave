using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSignLine : MonoBehaviour {

    private GameObject startLight;
    private GameObject targetLight;
    private GameObject target;
    private GameObject tube;
    private bool startLightPresent = false;
    
    private Tweeny lineGrowth;
    private Tweeny lightGrowth;
    public bool buildReady = false;
    
    void Start () {
        // associate objects and make them invisible in the start, better than disabled for performance
        if (GetComponent<Transform>().Find("LightRaySystem/PointLightStart") != null)
        {
            startLight = GetComponent<Transform>().Find("LightRaySystem/PointLightStart").gameObject;
            startLightPresent = true;
            startLight.GetComponent<Light>().range = 0;
        }
        
        tube = GetComponent<Transform>().Find("LightRaySystem/tube002").gameObject;
        tube.SetActive(false);
        
        targetLight = GetComponent<Transform>().Find("LightRaySystem/PointLight").gameObject;
        targetLight.GetComponent<Light>().range = 0;
        
        target = GetComponent<Transform>().Find("target").gameObject;
        
        lightGrowth = new Tweeny(0.0f, 0.15f, 700, "default");
        lineGrowth = 
            new Tweeny(
                GetComponent<Transform>().position, 
                target.GetComponent<Transform>().position,
                1000.0f,
                "default"
            );

        target.GetComponent<Transform>().position = GetComponent<Transform>().position;
    }
	
    public bool buildUp()
    {
        if (!lightGrowth.finished)
        {
            if (startLightPresent) startLight.GetComponent<Light>().range = lightGrowth.nextValue();
            targetLight.GetComponent<Light>().range = lightGrowth.nextValue();
        }
        
        if (!lineGrowth.finished)
        {
            target.GetComponent<Transform>().position = lineGrowth.nextVector();
            tube.SetActive(true);
            return false;
        }

        buildReady = true;
        return true;
    }

}
