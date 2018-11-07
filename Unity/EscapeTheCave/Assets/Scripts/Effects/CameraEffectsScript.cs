using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.PostProcessing;
using Assets.Scripts;

public class CameraEffectsScript : MonoBehaviour
{
    private BlurOptimized blur;
    private MotionBlur motionBlur;
    private VignetteModel vignette;
    private bool bPulsation;

    // Use this for initialization
    private void Start()
    {
        bPulsation = false;
        //use Camera.main or gameObject
        motionBlur = gameObject.AddComponent<MotionBlur>();
        blur = gameObject.AddComponent<BlurOptimized>();

        //TODO: create in script with 'AddComponent' and set profile in script?
        vignette = gameObject.GetComponent<PostProcessingBehaviour>().profile.vignette;
        
        InitEffects();
    }

    private void InitEffects()
    {
        blur.downsample = 2;
        blur.blurSize = 0;
        blur.blurIterations = 2;
        blur.blurShader = Shader.Find("Hidden/FastBlur");

        motionBlur.blurAmount = 0;
        motionBlur.extraBlur = false;
        motionBlur.shader = Shader.Find("Hidden/MotionBlur");

        ChangeBlur(false);

        SetVignetteIntensitySmooth(0);
        ChangeVignette(false);
    }

    // Update is called once per frame
    private void Update()
    {
        #region for testing...
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangeBlur(false);
            ChangeVignette(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeBlur(true);
            ChangeVignette(true);
            StartCoroutine(SetBlurIntensitySmooth(1));
            StartCoroutine(SetVignetteIntensitySmooth(0.05f));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeBlur(true);
            ChangeVignette(true);
            StartCoroutine(SetBlurIntensitySmooth(2));
            StartCoroutine(SetVignetteIntensitySmooth(0.1f));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeBlur(true);
            ChangeVignette(true);
            StartCoroutine(SetBlurIntensitySmooth(3));
            StartCoroutine(SetVignetteIntensitySmooth(0.15f));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeBlur(true);
            ChangeVignette(true);
            StartCoroutine(SetBlurIntensitySmooth(4));
            StartCoroutine(SetVignetteIntensitySmooth(0.2f));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeBlur(true);
            ChangeVignette(true);
            StartCoroutine(SetBlurIntensitySmooth(5));
            StartCoroutine(SetVignetteIntensitySmooth(0.25f));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeBlur(true);
            ChangeVignette(true);
            StartCoroutine(SetBlurIntensitySmooth(6));
            StartCoroutine(SetVignetteIntensitySmooth(0.3f));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeBlur(true);
            ChangeVignette(true);
            StartCoroutine(SetBlurIntensitySmooth(7));
            StartCoroutine(SetVignetteIntensitySmooth(0.35f));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeBlur(true);
            ChangeVignette(true);
            ChangeVignettePulsation(true);
            StartCoroutine(SetBlurIntensitySmooth(8));
            StartCoroutine(SetVignetteIntensitySmoothPulsation(0.4f, 0.5f));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangeBlur(true);
            ChangeVignette(true);
            StartCoroutine(SetBlurIntensitySmooth(9));
            StartCoroutine(SetVignetteIntensitySmooth(0.45f));
        }
        #endregion
    }

    /// <summary>
    /// Activate or deactivate blur and motion blur effect
    /// </summary>
    private void ChangeBlur(bool bActivate)
    {
        blur.enabled = bActivate;
        motionBlur.enabled = bActivate;
    }

    private IEnumerator SetBlurIntensitySmooth(float intensity)
    {
        bool increment = blur.blurSize < intensity;
        float step = 0.05f;
        float delta = Mathf.Abs(blur.blurSize - intensity);
        float steps = delta / step;
        while ((increment && blur.blurSize < intensity) || (!increment && blur.blurSize > intensity))
        {
            //TODO: make time depends of delta? 
            //TODO: make it unpossible to run a new co-routine with StartCoroutine if another is already running!
            yield return new WaitForSecondsRealtime(0.8f / steps);
            if (increment)
            {
                blur.blurSize += step;
                motionBlur.blurAmount += step / 10;
            }
            else
            {
                blur.blurSize -= step;
                motionBlur.blurAmount -= step / 10;
            }
        }

        blur.blurSize = intensity;
        motionBlur.blurAmount = intensity / 10;
    }

    /// <summary>
    /// Activate or deactivate vignette effect
    /// </summary>
    private void ChangeVignette(bool bActivate)
    {
        vignette.enabled = bActivate;
    }

    /// <summary>
    /// Activate or deactivate pulsation vignette effect
    /// </summary>
    private void ChangeVignettePulsation(bool bActivate)
    {
        bPulsation = bActivate;
    }

    private IEnumerator SetVignetteIntensitySmooth(float intensity)
    {
        VignetteModel.Settings vignetteSettings = vignette.settings;

        bool increment = vignetteSettings.intensity < intensity;
        float step = 0.01f;
        float delta = Mathf.Abs(vignetteSettings.intensity - intensity);
        float steps = delta / step;
        while ((increment && vignetteSettings.intensity < intensity) || (!increment && vignetteSettings.intensity > intensity))
        {
            //TODO: make time depends of delta? 
            //TODO: make it unpossible to run a new co-routine with StartCoroutine if another is already running! 
            yield return new WaitForSecondsRealtime(0.5f / steps);
            if (increment)
            {
                vignetteSettings.intensity += step;
                //overwrite the original with copy
                vignette.settings = vignetteSettings;
            }
            else
            {
                vignetteSettings.intensity -= step;
                vignette.settings = vignetteSettings;
            }
        }

        vignetteSettings.intensity = intensity;
        vignette.settings = vignetteSettings;
    }

    private void SetVignetteIntensity(float intensity)
    {
        VignetteModel.Settings vignetteSettings = vignette.settings;
        vignetteSettings.intensity = intensity;
        vignette.settings = vignetteSettings;
    }

    private IEnumerator SetVignetteIntensitySmoothPulsation(float maxIntensity, float frequency)
    {
        //pulsation
        while (bPulsation)
        {
            //use random factor for more realistic result
            //use half of waiting time because of the two-time wait
            SetVignetteIntensity(maxIntensity);
            yield return new WaitForSecondsRealtime(Random.Range(frequency / 2 * 0.75f, frequency / 2));
            SetVignetteIntensity(maxIntensity*0.85f);
            yield return new WaitForSecondsRealtime(Random.Range(frequency / 2 * 0.75f, frequency / 2));
        }
    }
}