using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private CameraLocataionDatas _cameraLocataionDatas;
    [SerializeField] private PlayerLocationDatas _playerLocationDatas;
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject player;
    void Start()
    {
        MoveMap(0);
        MoveMap(0);
    }
    public void MoveMap(int n) // to map[n] (n = 0, 1, 2, ...)
    {
        CameraMoving script = _camera.GetComponent<CameraMoving>();
        script.SetTarget(_cameraLocataionDatas.maps[n], n);
        Debug.Log(player.transform.position.x);
        player.transform.position = _playerLocationDatas.locations[n];
    }

    public void MoveStage()
    {
        StartCoroutine("StageChange");
    }

    IEnumerator StageChange()
    {
        Time.timeScale = 0;
        TestGameManager.Instance.UIManager.StartFadein();
        yield return new WaitForSecondsRealtime(2f);
        TestGameManager.Instance.gameLevel++;
        Time.timeScale = 1f;
        MoveMap(0);
        yield return new WaitForSecondsRealtime(0.5f);
        TestGameManager.Instance.UIManager.StartFadeout();
    }
}
