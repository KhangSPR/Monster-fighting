﻿using System;
using System.Collections.Generic;
using UIGameDataMap;
using UnityEngine;
/// <summary>
/// This script create the grid of level buttons in LevelMenu
/// </summary>

public class LevelUIManager : MonoBehaviour
{
    private static LevelUIManager instance;                             //instance variable
    public static LevelUIManager Instance { get => instance; }          //instance getter

    [SerializeField] private LevelButton levelBtnPrefab;              //ref to LevelButton prefab
    [SerializeField] private Transform areasHolder;
    [SerializeField] private LevelInfo levelInfo;
    [SerializeField] private PortalHouseBtn portalHouseBtn;
    public LevelInfo LevelInfo => levelInfo;

    [SerializeField] private Material[] materials;

    public Material[] Materials { get { return materials; } }
    public List<LevelButton> mapbtnGameObjects;
    public LevelButton CurrentLevelButton
    {
        get
        {
            if (mapbtnGameObjects != null && mapbtnGameObjects.Count > m_CurrentIndex)
            {
                return mapbtnGameObjects[m_CurrentIndex];
            }
            else
            {
                // Trả về null hoặc giá trị mặc định tùy theo yêu cầu của bạn
                return null; // hoặc trả về giá trị mặc định
            }
        }
    }

    [SerializeField] MapSO[] mapSOArray;
    public MapSO mapSOCurrent { get => mapSOArray[m_CurrentIndex]; }
    public MapSO[] MapSOArray => mapSOArray;
    private string mapSOFolder = "Map/";
    [SerializeField] int m_CurrentIndex;

    private void Start()
    {
        mapSOArray = loadMapSO(mapSOFolder);

        //Reset
        m_CurrentIndex = GetLevelLastUnLockArea();

        portalHouseBtn.SetPortals(mapSOCurrent);
        //this.InitializeUI();
        //LoadAllLevelByArea(0); // Code nay cua Nguyen
        //if()
    }
    private void Awake()
    {
        if (LevelUIManager.instance != null)
        {
            Debug.LogError("Only 1 LevelUIManager Warning");
        }
        LevelUIManager.instance = this;

    }
    public MapSO[] loadMapSO(string path)
    {
        MapSO[] mapSOs = UnityEngine.Resources.LoadAll<MapSO>(path);
        Array.Sort(mapSOs, (x, y) => x.id.CompareTo(y.id)); //Sort
        return mapSOs;
    }
    public MapSO GetMapSO(int id, MapType mapType, Difficult difficult)
    {
        foreach (MapSO mapSO in mapSOArray)
        {
            // Kiểm tra các thuộc tính của MapSO để tìm MapSO phù hợp
            if (mapSO.id == id && mapSO.mapType == mapType && mapSO.difficult == difficult)
            {
                return mapSO; // Trả về MapSO nếu tìm thấy
            }
        }

        return null; // Trả về null nếu không tìm thấy MapSO phù hợp
    }

    public void DeloadAllLevelCreated()
    {
        foreach (var map in mapbtnGameObjects)
        {
            Destroy(map.gameObject);
        }
    }
    public void LoadAllLevelByArea(string areaName)
    {
        var areaComponent = areasHolder.Find("Maps").Find("Map_Scroll").Find("Map_Pages").Find(areaName);
        if (areaComponent != null)
        {
            //Debug.Log($"Vùng {areaName} có tồn tại");
            var lvBtnHolder = areaComponent.Find("LvBtnHolder");
            var lvPosition = lvBtnHolder.Find("LvPostion");
            int levelBtnHolderCount = lvPosition.childCount; // lấy số lượng các level có trong vùng
            if (levelBtnHolderCount > 0)
            {
                for (int i = 0; i < levelBtnHolderCount; i++)
                {
                    var levelPosition = lvPosition.GetChild(i).position;
                    //Debug.Log(lvBtnHolder.childCount + " lvBtnHolder.childCount");
                    var MapBtnClone = Instantiate(levelBtnPrefab, levelPosition, Quaternion.identity, lvBtnHolder); // khởi tạo MapBtn
                    mapbtnGameObjects.Add(MapBtnClone);
                }
            }
        }
        else
        {
            Debug.Log($"Vùng {areaName} không tồn tại hoặc chưa được thêm vào");
        }
        Debug.Log("Load Level của vùng có tên " + areaName);
    }
    public void LoadAllLevelByArea(int areaIndex)
    {
        string areaName = areasHolder.Find("Maps_Icon").GetChild(areaIndex).name;
        var areaComponent = areasHolder.Find("Maps").Find("Map_Scroll").Find("Map_Pages").Find(areaName);
        if (areaComponent != null)
        {
            //Debug.Log($"Vùng {areaName} có tồn tại");
            var lvBtnHolder = areaComponent.Find("LvBtnHolder");
            var lvPosition = lvBtnHolder.Find("LvPostion");
            int levelBtnHolderCount = lvPosition.childCount; // lấy số lượng các level có trong vùng
            if (levelBtnHolderCount > 0)
            {
                for (int i = 0; i < levelBtnHolderCount; i++)
                {
                    var levelPosition = lvPosition.GetChild(i).position;
                    var MapBtnClone = Instantiate(levelBtnPrefab, levelPosition, Quaternion.identity, lvBtnHolder); // khởi tạo MapBtn
                }
            }
        }
        else
        {
            Debug.Log($"Vùng {areaName} không tồn tại hoặc chưa được thêm vào");
        }
        Debug.Log("Load Level của vùng có vị trí số " + areaIndex + " có tên : " + areaName);
    }
    //Set Butoon Index
    int GetLevelLastUnLockArea()
    {
        List<AreasData> areas = LevelSystemManager.Instance.aiso.areasData;

        // Khởi tạo biến để lưu vị trí đầu tiên của cấp độ chưa mở khóa
        int firstLockedLevelIndex = -1;

        // Duyệt qua từng khu vực
        foreach (AreasData area in areas)
        {
            // Lấy danh sách các cấp độ trong khu vực hiện tại
            List<LevelData> levels = area.levelsData;

            // Duyệt qua từng cấp độ
            foreach (LevelData level in levels)
            {
                // Kiểm tra nếu cấp độ chưa mở khóa
                if (!level.isUnlocked)
                {
                    // Lưu vị trí của cấp độ chưa mở khóa
                    firstLockedLevelIndex = level.levelIndex;
                    return firstLockedLevelIndex - 1; //Take the following unlocked position 1 unit
                }
            }

            // Nếu đã tìm thấy cấp độ chưa mở khóa, thoát khỏi vòng lặp
            if (firstLockedLevelIndex != -1)
            {
                break;
            }
        }
        return 0;
    }

    //public void InitializeUI()
    //{
    //    LevelItem[] levelItemsArray = LevelSystemManager.Instance.LevelData.levelItemArray;  // Lấy mảng dữ liệu cấp độ

    //    lengArrayLevel = levelItemsArray.Length - 1;

    //    for (int i = levelItemsArray.Length - 1; i >= 0; i--)
    //    {
    //        RectTransform rectTransform = levelItemsArray[i].position; // Lấy RectTransform từ levelItemArray
    //        Vector3 worldPosition = rectTransform.TransformPoint(rectTransform.rect.center); // Chuyển đổi tọa độ thành tọa độ thế giới


    //        LevelButton levelButton = Instantiate(levelBtnPrefab, worldPosition, Quaternion.identity, levelBtnGridHolder); // Tạo mới LevelButton tại tọa độ thế giới
    //        //levelButton.SetLevelButton(levelItemsArray[i], i, i == LevelSystemManager.Instance.LevelData.lastUnlockedLevel, mapSO[i]); // Thiết lập LevelButton

    //        //levelButton.SetlevelInfo();


    //        levelButtons.Add(levelButton);


    //        if (mapSO[i].boss)
    //        {
    //            // Scale 
    //            levelButton.transform.localScale = new Vector3(1.6f, 1.87f, 1f);
    //        }

    //        //levelButton.SetlMapSO(mapSO[i]);
    //        levelButton.SetLvInfo(levelInfo);
    //    }
    //}
}
