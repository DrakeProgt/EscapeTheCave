using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SequenceController : MonoBehaviour
{

    [SerializeField]
    GameObject positionTarget, rotationTargetOne, rotationTargetTwo, sequenceUI;
    bool started, firstSequenceDone, secondSequenceDone;
    float duration, elapsed;
    Vector3 startPosition;

    public bool test;


    void Start()
    {
        elapsed = 0;
        duration = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isWordPuzzleSolved)
        {
            //gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().test = test;
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

                transform.position = Vector3.Lerp(startPosition, positionTarget.transform.position, elapsed / duration);
                elapsed += Time.deltaTime;

                if (elapsed > duration)
                {
                    elapsed = 0;
                    firstSequenceDone = true;
                    transform.position = positionTarget.transform.position;
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
                    gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
                    transform.rotation = Quaternion.LookRotation(rotationTargetTwo.transform.position - transform.position);
                    transform.GetChild(0).rotation = Quaternion.LookRotation(rotationTargetTwo.transform.position - transform.position);
                    sequenceUI.SetActive(false);
                    secondSequenceDone = true;
                }
            }
        }
    }


    float EqualVecs(Vector3 lhs, Vector3 rhs)
    {
        return Mathf.Abs((lhs.x - rhs.x) + (lhs.y - rhs.y) + (lhs.z - lhs.z));
    }
}
