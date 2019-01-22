using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricFogAndMist;

public class FogSpread : MonoBehaviour
{

	public GameObject GroundFog;
	public float targetFogHeight = 1.5f;
	public float targetFogFinalHeight = 4f;

//	private Vector3 startPosition;
	private Tweeny fogRise;
	private Tweeny fogGrowth;
	private Tweeny fogDensityGrowth;
	private Tweeny fogSpread;
	private Tweeny fogFinalGrowth;
	private Tweeny fogSlowDown;
	private Tweeny fogFinalRise;
	private Tweeny fogDensityFall;


	private bool tweenStart = false;

	// Use this for initialization
	void Start () {
		fogGrowth = new Tweeny(new Vector3(50,15,50), new Vector3(200,15,200), 10000, "linear");
		fogRise = new Tweeny(GroundFog.GetComponent<VolumetricFog>().height, targetFogHeight, 4000, "linear");
		fogDensityGrowth = new Tweeny(0.8f, 1.1f, 2000, "default");

		fogSpread = new Tweeny(GroundFog.GetComponent<VolumetricFog>().fogAreaFallOff, 3.0f, 20000, "linear");
		fogSlowDown = new Tweeny(GroundFog.GetComponent<VolumetricFog>().speed, 0.005f, 10000, "default");
		fogFinalRise = new Tweeny(targetFogHeight, targetFogFinalHeight, 10000, "default");
		fogDensityFall = new Tweeny(1.1f, 0.4f, 10000, "default");
		fogFinalGrowth = new Tweeny(new Vector3(200,15,200), new Vector3(2000,15,2000), 10000, "linear");

//		StartCoroutine(DelayedStart(3));
	}

	IEnumerator DelayedStart(float waitTime)
	{
		// suspend execution for waitTime seconds
		yield return new WaitForSeconds(waitTime);
		tweenStart = true;
		
	}

	// Update is called once per frame
	void Update () {
		if (!GameManager.isWordPuzzleSolved)
		{
			return;
		}
		
		if (!fogGrowth.finished)
		{
			GroundFog.GetComponent<Transform>().localScale = fogGrowth.nextVector();
			GroundFog.GetComponent<VolumetricFog>().height = fogRise.nextValue();
			GroundFog.GetComponent<VolumetricFog>().density = fogDensityGrowth.nextValue();
		}

		if (fogGrowth.finished && !fogSpread.finished)
		{
			GroundFog.GetComponent<VolumetricFog>().fogAreaFallOff = fogSpread.nextValue();
			GroundFog.GetComponent<VolumetricFog>().speed = fogSlowDown.nextValue();
			GroundFog.GetComponent<VolumetricFog>().height = fogFinalRise.nextValue();
			GroundFog.GetComponent<VolumetricFog>().density = fogDensityFall.nextValue();
			GroundFog.GetComponent<Transform>().localScale = fogFinalGrowth.nextVector();
		}
		
	}
}
