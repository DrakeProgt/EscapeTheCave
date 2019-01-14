using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceController : MonoBehaviour
{

    [SerializeField] GameObject positionTarget, rotationTargetOne, rotationTargetTwo, sequenceUI;
    bool done = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isWordPuzzleSolved && !done)
        {
            StartCoroutine(MoveObjectToPosition(transform, positionTarget.transform.position, 8));
        }
    }

    IEnumerator MoveObjectToPosition(Transform trans, Vector3 end_position, float duration)
    {
        gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        sequenceUI.SetActive(true);

        Vector3 start_position = trans.position;
        float elapsed = 0;

        // move to first position and look at starsign
        while (elapsed < duration)
        {
            Debug.Log("Calculating...");
            Debug.Log(elapsed);
            Debug.Log(duration);
            var targetRotation = Quaternion.LookRotation(rotationTargetOne.transform.position - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
            transform.GetChild(0).rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

            trans.position = Vector3.Lerp(start_position, end_position, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Done...");
        trans.position = end_position;
        transform.rotation = Quaternion.LookRotation(rotationTargetOne.transform.position - transform.position);
        transform.GetChild(0).rotation = Quaternion.LookRotation(rotationTargetOne.transform.position - transform.position);

        float elapsedNew = 0;
        while (elapsedNew < duration)
        {
            Debug.Log("Calculating again...");
            var targetRotation = Quaternion.LookRotation(rotationTargetTwo.transform.position - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
            transform.GetChild(0).rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

            elapsedNew += Time.deltaTime;
            yield return null;
        }

        gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        sequenceUI.SetActive(false);
        done = true;
    }

    float EqualVecs(Vector3 lhs, Vector3 rhs)
    {
        return Mathf.Abs((lhs.x - rhs.x) + (lhs.y - rhs.y) + (lhs.z - lhs.z));
    }
}
