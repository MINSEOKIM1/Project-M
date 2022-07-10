using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestGameManager : MonoBehaviour
{
    public static TestGameManager Instance { get; private set; }
    public MapManager MapManager { get; private set; }
    public UIManager UIManager { get; private set; }

    public int gameLevel = 1;
    void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this);
            return;
        }
        
        Instance = this;

        MapManager = GetComponentInChildren<MapManager>();
        UIManager = GetComponentInChildren<UIManager>();
    }
}
