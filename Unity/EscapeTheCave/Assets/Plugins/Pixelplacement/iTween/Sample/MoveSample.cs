using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{
    void Start()
    {

    }

    public void MoveAnimation(Vector3 targetPosition)
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", targetPosition, "easeType", "easeInOutExpo", "time", 2));
    }

    public IEnumerator DestroyObject(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }

    public IEnumerator EnableCollider(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}

