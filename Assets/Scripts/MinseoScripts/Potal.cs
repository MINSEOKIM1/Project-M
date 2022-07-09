using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public int nextMapNum;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            TestGameManager.Instance.MapManager.MoveMap(nextMapNum);
        }
    }
}
