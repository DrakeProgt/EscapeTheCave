using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SequenceController : MonoBehaviour
{

    [SerializeField]
    GameObject positionTargetOne, positionTargetTwo, positionTargetThree, rotationTargetOne, rotationTargetTwo, rotationTargetThree, rotationTargetFour, sequenceUI;
    bool started, firstSequenceDone, secondSequenceDone, thirdSequenceDone;
    float duration, elapsed;
    Vector3 startPosition;

    bool isFirstSequenceFinished = true, isSecondSequenceFinished;

    float stepTimePassed = 0;

    Dictionary<string, bool> sequencesDone;

    void Start()
    {
        elapsed = 0;
        duration = 8.5f;
        sequencesDone = new Dictionary<string, bool>();
    }

    void sequence(int steps)
    {
        if (!started)
        {
            started = true;
            startPosition = transform.position;
            gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            sequenceUI.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isWordPuzzleSolved && !isFirstSequenceFinished)
        {
            if (!started)
            {
                started = true;
                startPosition = transform.position;
                gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
                sequenceUI.SetActive(true);
            } 
            else if (!firstSequenceDone)
            {
                var targetRotation = Quaternion.LookRotation(rotationTargetOne.transform.position - transform.position);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 3 * Time.deltaTime);
                transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).rotation, targetRotation, 3 * Time.deltaTime);

                transform.position = Vector3.Lerp(startPosition, positionTargetOne.transform.position, elapsed / duration);
                elapsed += Time.deltaTime;
                PlaySound();

                if (elapsed > duration)
                {
                    elapsed = 0;
                    stepTimePassed = 0;
                    firstSequenceDone = true;
                    transform.position = positionTargetOne.transform.position;
                    transform.rotation = Quaternion.LookRotation(rotationTargetOne.transform.position - transform.position);
                    transform.GetChild(0).rotation = Quaternion.LookRotation(rotationTargetOne.transform.position - transform.position);
                }
            }
            else if (!secondSequenceDone)
            {
                var targetRotation = Quaternion.LookRotation(rotationTargetTwo.transform.position - transform.position);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
                transform.GetChild(0).rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);

                elapsed += Time.deltaTime;

                if (elapsed > duration)
                {
                    elapsed = 0;
                    transform.rotation = Quaternion.LookRotation(rotationTargetTwo.transform.position - transform.position);
                    transform.GetChild(0).rotation = Quaternion.LookRotation(rotationTargetTwo.transform.position - transform.position);
                    firstSequenceDone = false;
                    isFirstSequenceFinished = true;
                    started = false;
                    StopSequence(new Quaternion(0, .8f, 0, -.6f));
                }
            }
        }

        if(GameManager.isLightPuzzleSolved && !isSecondSequenceFinished)
        {
            if (!started)
            {
                started = true;
                startPosition = transform.position;
                gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
                sequenceUI.SetActive(true);
            }
            else if (!firstSequenceDone)
            {
                var targetRotation = Quaternion.LookRotation(rotationTargetThree.transform.position - transform.position);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 3 * Time.deltaTime);
                transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).rotation, targetRotation, 3 * Time.deltaTime);

                transform.position = Vector3.Lerp(startPosition, positionTargetTwo.transform.position, elapsed / duration);
                elapsed += Time.deltaTime;
                PlaySound();

                if (elapsed > duration)
                {
                    elapsed = 0;
                    startPosition = transform.position;
                    firstSequenceDone = true;
                    transform.position = positionTargetTwo.transform.position;
                    transform.rotation = Quaternion.LookRotation(rotationTargetThree.transform.position - transform.position);
                    transform.GetChild(0).rotation = Quaternion.LookRotation(rotationTargetThree.transform.position - transform.position);
                }
            }
            else if (!secondSequenceDone)
            {
                var targetRotation = Quaternion.LookRotation(rotationTargetThree.transform.position - transform.position);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
                transform.GetChild(0).rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
                transform.position = Vector3.Lerp(startPosition, positionTargetThree.transform.position, elapsed / duration);

                elapsed += Time.deltaTime;

                if (elapsed > duration)
                {
                    startPosition = transform.position;
                    elapsed = 0;
                    transform.position = positionTargetThree.transform.position;
                    transform.rotation = Quaternion.LookRotation(rotationTargetThree.transform.position - transform.position);
                    transform.GetChild(0).rotation = Quaternion.LookRotation(rotationTargetThree.transform.position - transform.position);
                    secondSequenceDone = true;
                }
            } else if(!thirdSequenceDone)
            {
                var targetRotation = Quaternion.LookRotation(rotationTargetFour.transform.position - transform.position);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
                transform.GetChild(0).rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
                transform.position = Vector3.Lerp(startPosition, positionTargetThree.transform.position, elapsed / duration);

                elapsed += Time.deltaTime;

                if (elapsed > duration)
                {
                    elapsed = 0;
                    transform.position = positionTargetThree.transform.position;
                    transform.rotation = Quaternion.LookRotation(rotationTargetThree.transform.position - transform.position);
                    transform.GetChild(0).rotation = Quaternion.LookRotation(rotationTargetThree.transform.position - transform.position);
                    secondSequenceDone = true;
                    isSecondSequenceFinished = true;
                    StopSequence(new Quaternion(0, .2f, 0, -.6f));
                }
            }
        }
    }

    void StopSequence(Quaternion rootRotation)
    {
        gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().rootRotation = rootRotation;
        sequenceUI.SetActive(false);
    }

    void PlaySound()
    {
        if(stepTimePassed + 1.3f < elapsed)
        {
            stepTimePassed = elapsed;
            // Play sound
            AudioSource a = gameObject.GetComponent<AudioSource>();
            int n = Random.Range(1, gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_FootstepSounds.Length);
            a.clip = gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_FootstepSounds[n];
            a.Play();
        }
    }
}
