using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private CameraLocataionDatas _cameraLocataionDatas;
    private Vector3 target;
    private Transform playerTransform;
    private TestLookMouse _testLookMouse;

    public int mapNum = 0;
    
    public float lerpSpeed;
    public float deflerpSpeed;
    public float yoffset = 2f;

    private bool isMoving = false;
    public void SetTarget(Vector3 target, int num)
    {
        this.target = target;
        mapNum = num;
        StopCoroutine("MoveCamera");
        StartCoroutine("MoveCamera");
    }

    IEnumerator MoveCamera()
    {
        isMoving = true;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
            transform.position = Vector3.Lerp(transform.position,target, lerpSpeed * Time.unscaledDeltaTime);
        }

        transform.position = target;
        isMoving = false;
    }

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        _testLookMouse = playerTransform.gameObject.GetComponentInChildren<TestLookMouse>();
    }

    private void Update()
    {
        if (playerTransform == null) return;

        var offset = _testLookMouse.PlayerTomouseVector3;

        if (!isMoving)
        {
            var targetPos = playerTransform.position + offset;
            Debug.Log("offset X : " + offset.x + " offset Y : " + offset.y);
            targetPos = new Vector3(
                Mathf.Clamp(targetPos.x, _cameraLocataionDatas.xMin[mapNum], _cameraLocataionDatas.xMax[mapNum]),
                Mathf.Clamp(targetPos.y, _cameraLocataionDatas.yMin[mapNum], _cameraLocataionDatas.yMax[mapNum]),
                -10f);

            transform.position = Vector3.Lerp(transform.position, targetPos, deflerpSpeed * Time.deltaTime);
        }
    }
}
