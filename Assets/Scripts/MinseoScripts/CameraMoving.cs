using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private Vector3 target;
    public float lerpSpeed;
    public void SetTarget(Vector3 target)
    {
        this.target = target;
        StopCoroutine("MoveCamera");
        StartCoroutine("MoveCamera");
    }

    IEnumerator MoveCamera()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
            transform.position = Vector3.Lerp(transform.position,target, lerpSpeed * Time.unscaledDeltaTime);
        }

        transform.position = target;
    }
    
}
