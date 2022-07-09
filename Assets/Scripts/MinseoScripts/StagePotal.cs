using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePotal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            TestGameManager.Instance.MapManager.MoveStage();
        }
    }
}
