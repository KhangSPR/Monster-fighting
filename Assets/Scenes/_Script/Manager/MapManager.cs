using System;
using System.Collections;
using System.Collections.Generic;
//using UIGameDataMap;
using UnityEngine;

public class MapManager : SaiMonoBehaviour
{
    private static MapManager instance;
    public static MapManager Instance => instance;


    public GameObject gameMapPrefab;


    private GameObject currentMap = null;
    public GameObject CurrentMap { get { return currentMap; } }



    protected override void Awake()
    {
        if (MapManager.instance != null)
        {
            Debug.LogError("Only 1 MapManager Warning");
        }
        MapManager.instance = this;
    }
    private GameObject loadMapLevel(string path)
    {
        return Resources.Load<GameObject>(path);
    }
    public void LoadMap()
    {
        //Destroy(currentScene);
        currentMap = Instantiate(gameMapPrefab);
        currentMap.SetActive(true);
    }

}
