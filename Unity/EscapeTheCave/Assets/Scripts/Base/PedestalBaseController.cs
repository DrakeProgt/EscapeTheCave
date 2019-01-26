using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalBaseController : MonoBehaviour
{

    [SerializeField] Vector3 targetPosition;
    [SerializeField] GameObject[] pedestalGates;
    bool isPedestalMovedUp, isGateOpened, isGateAudioPlaying, isPedestalAudioPlaying;
    float timePassed;

    // Use this for initialization
    void Start()
    {
        isPedestalMovedUp = false;
        isGateOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isWordPuzzleSolved || GameManager.isLightPuzzleSolved)
        {
            return;
        }

        timePassed += Time.deltaTime;

        if (!isGateAudioPlaying && timePassed > 6)
        {
            SoundSystem.PlaySound("Audio/Cave/FX/Plateau-Szene", 0, .2f);
            isGateAudioPlaying = true;
        }

        if (timePassed < 8)
        {
            return;
        }

        if (!isGateOpened)
        {
            foreach (GameObject pedestalGate in pedestalGates)
            {
                pedestalGate.transform.Translate(Vector3.forward * -.5f * Time.deltaTime);
                if (pedestalGate.transform.position.z < 0)
                {
                    isGateOpened = true;
                }
            }
        }
        else if (!isPedestalMovedUp)
        {
            if(!isPedestalAudioPlaying)
            {
                SoundSystem.PlayPedestalSound();
                isPedestalAudioPlaying = true;
            }
            transform.Translate(Vector3.up * .5f * Time.deltaTime);

            if (transform.position.y >= -3.6f)
            {
                isPedestalMovedUp = true;
            }
        }
    }
}
