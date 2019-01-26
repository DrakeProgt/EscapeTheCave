using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SequenceController : MonoBehaviour
{

    [SerializeField] GameObject sequenceUI;
    [SerializeField] GameObject[] rotationTargets, positionTargets;
    float duration, elapsed, stepTimePassed;
    Vector3 startPosition;

    bool started;
    bool isFirstSequenceFinished, isSecondSequenceFinished, testSequenceFinished;
    bool monsterSoundPlayed;

    bool[] sequencesDone;

    void Start()
    {
        stepTimePassed = 0;
        elapsed = 0;
        duration = 8.5f;
        sequencesDone = new bool[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isWordPuzzleSolved && !isFirstSequenceFinished)
        {
            if (!started)
            {
                StartSequence(3);
            }
            else if (!sequencesDone[0])
            {
                LookTo(rotationTargets[0], 1);
                MoveTo(positionTargets[0], elapsed / duration);
                elapsed += Time.deltaTime;
                PlayStepSound();

                if (elapsed > duration)
                {
                    ResetSequence(ref sequencesDone[0], positionTargets[0], rotationTargets[0], ref testSequenceFinished);
                }
            }
            else if (!sequencesDone[1])
            {
                LookTo(rotationTargets[1], 1);
                MoveTo(positionTargets[0], elapsed / duration);
                elapsed += Time.deltaTime;

                if (elapsed > duration)
                {
                    ResetSequence(ref sequencesDone[1], positionTargets[0], rotationTargets[1], ref isFirstSequenceFinished);
                    StopSequence(new Quaternion(0, .8f, 0, -.6f));
                }
            }
        }

        else if (GameManager.isLightPuzzleSolved && !isSecondSequenceFinished)
        {
            if (!started)
            {
                StartSequence(4);
            }
            else if (!sequencesDone[0])
            {
                LookTo(rotationTargets[2], 3);
                MoveTo(positionTargets[1], elapsed / duration);
                elapsed += Time.deltaTime;
                PlayStepSound();

                if (elapsed > duration)
                {
                    ResetSequence(ref sequencesDone[0], positionTargets[1], rotationTargets[2], ref testSequenceFinished);
                }
            }
            else if (!sequencesDone[1])
            {
                LookTo(rotationTargets[2], 1);
                MoveTo(positionTargets[2], elapsed / duration);
                elapsed += Time.deltaTime;
                PlayStepSound();

                if (elapsed > duration)
                {
                    ResetSequence(ref sequencesDone[1], positionTargets[2], rotationTargets[2], ref testSequenceFinished);
                }
            }
            else if (!sequencesDone[2])
            {
                LookTo(rotationTargets[3], 1);
                MoveTo(positionTargets[2], elapsed / duration);
                elapsed += Time.deltaTime;

                if (elapsed > duration)
                {
                    ResetSequence(ref sequencesDone[2], positionTargets[2], rotationTargets[3], ref testSequenceFinished);
                }
            }
            else if (!sequencesDone[3])
            {
                GameObject.Find("Monster").transform.position = new Vector3(-34.78f, .4f, 3.05f);
                if(!monsterSoundPlayed)
                {
                    SoundSystem.PlaySound("Audio/Cave/Monster/Monster-Growl (5)", .5f, 1, 10, 0, GameObject.Find("Monster"));
                    monsterSoundPlayed = true;
                }
                LookTo(rotationTargets[4], .5f);
                MoveTo(positionTargets[2], elapsed / duration);
                elapsed += Time.deltaTime;

                if (elapsed > duration)
                {
                    ResetSequence(ref sequencesDone[3], positionTargets[2], rotationTargets[4], ref isSecondSequenceFinished);
                    GameManager.secondCaveReached = true;
                    Destroy(GameObject.Find("SoundSystem").GetComponents<AudioSource>()[1]);
                    StopSequence(new Quaternion(0, .2f, 0, -.6f));
                }
            }
        }
    }

    private void ResetSequence(ref bool sequenceDone, GameObject finalPosition, GameObject finalRotation, ref bool sequenceFinished)
    {
        startPosition = transform.position;
        elapsed = 0;
        stepTimePassed = 0;
        transform.position = finalPosition.transform.position;
        transform.rotation = Quaternion.LookRotation(finalRotation.transform.position - transform.position);
        transform.GetChild(0).rotation = Quaternion.LookRotation(finalRotation.transform.position - transform.position);
        sequenceDone = true;
        sequenceFinished = true;
    }

    void LookTo(GameObject rotationTarget, float speed)
    {
        var targetRotation = Quaternion.LookRotation(rotationTarget.transform.position - transform.position);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).rotation, targetRotation, speed * 2 * Time.deltaTime);
    }

    void MoveTo(GameObject positionTarget, float speed)
    {
        transform.position = Vector3.Lerp(startPosition, positionTarget.transform.position, speed);
    }

    void StartSequence(int steps)
    {
        sequencesDone = new bool[steps];
        started = true;
        startPosition = transform.position;
        gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        sequenceUI.SetActive(true);
    }

    void StopSequence(Quaternion rootRotation)
    {
        started = false;
        gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().resetRotation = true;
        gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().rootRotation = rootRotation;
        sequenceUI.SetActive(false);
    }

    void PlayStepSound()
    {
        if (stepTimePassed + 1.3f < elapsed)
        {
            stepTimePassed = elapsed;
            AudioSource a = gameObject.GetComponent<AudioSource>();
            int n = Random.Range(1, gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_FootstepSounds.Length);
            a.clip = gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_FootstepSounds[n];
            a.Play();
        }
    }
}
