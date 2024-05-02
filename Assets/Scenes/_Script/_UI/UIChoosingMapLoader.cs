using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIGameDataManager;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
namespace UIGameDataMap
{
    public class UIChoosingMapLoader : SaiMonoBehaviour
    {
        public MapSO mapSO;

        public void onClickExit()
        {
            Application.Quit();
        }
        public void loadMapButtonDetail()
        {
            MapManager.Instance.LoadMap();

            UIManager.Instance.DetiveGameUI();

            //Card Manager All
            MapCtrl mapCtrl = MapManager.Instance.CurrentMap.GetComponent<MapCtrl>();

            mapCtrl.PortalSpawnerCtrl.MapSO = mapSO;
            mapCtrl.UIInGame.ListCardTowerData.InstantiateObjectsFromData();

            mapCtrl.gameObject.SetActive(true);


            LevelUIManager.Instance.mapbtnGameObjects.Clear();

        }
        public void SetMapSOFromLevelInfo(MapSO mapSO)
        {
            this.mapSO = mapSO;
        }
    }
}