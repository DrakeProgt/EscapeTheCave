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
                StartSequence(5);
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
                    GameObject.Find("MonsterSequence").GetComponent<SelfMovement>().Goto(new Vector3(-34.78f, 0.6f, 3.137f));
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
                duration = 5.0f;
                if(!monsterSoundPlayed)
                {
                    GameObject.Find("FogGround").SetActive(false);
                    GameObject.Find("SoundSystem").GetComponents<AudioSource>()[1].enabled = false;
                    SoundSystem.PlaySound("Audio/Cave/Monster/Monster-Growl (5)", .5f, 1, 10, 0, GameObject.Find("MonsterSequence"));
                    monsterSoundPlayed = true;
                }
                LookTo(rotationTargets[4], 2f);
                MoveTo(positionTargets[2], elapsed / duration);
                elapsed += Time.deltaTime;

                if (elapsed > duration)
                {
                    GameObject.Find("MonsterSequence").GetComponent<SelfMovement>().speed = 15;
                    GameObject.Find("MonsterSequence").GetComponent<SelfMovement>().Goto(new Vector3(-38.98f, .4f, -32.86f));
                    SoundSystem.PlaySound("Audio/Cave/Monster/Monster-Scream (2)", 1f, 1, 10, 0, GameObject.Find("MonsterSequence"));
                    ResetSequence(ref sequencesDone[3], positionTargets[2], rotationTargets[4], ref testSequenceFinished);
                }
            }
            
            else if (!sequencesDone[4])
            {
                duration = 5.0f;
                LookTo(rotationTargets[5], 2f);
                elapsed += Time.deltaTime;

                if (elapsed > duration)
                {
                    Quaternion currentRotation = transform.GetChild(0).rotation;
                    GameObject.Find("MonsterSequence").SetActive(false);
                    GameManager.monsterPosition.y = -1000;
                    GameManager.secondCaveReached = true;
                    ResetSequence(ref sequencesDone[4], positionTargets[2], rotationTargets[5], ref isSecondSequenceFinished);
                    StopSequence(currentRotation);
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
//        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).rotation, targetRotation, speed * Time.deltaTime);
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
