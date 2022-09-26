using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class MetaGameShipController : MonoBehaviour
{
    
    [SerializeField]
    private MetaGameShipView shipOptionsView;

    [Header("Combat data")]
    [SerializeField]
    private PlayerShipData playerShipData;
    [SerializeField]
    private IslandEnemyShipDataList islandEnemyShipList;

    public CombatData CombatData { get; set; }

    public VoidEvent OpenShipOptionsEvent { get; set; }
    public VoidEvent CloseShipCameraEvent { get; set; }
    public VoidEvent LoadCombatSceneEvent { get; set; }
    public VoidEvent OpenCatCrewManagementNoIDEvent { get; set; }
    public VoidEvent StartCombatEvent { get; set; }



    private List<IAtomEventHandler> _eventHandlers = new();
    private CatData catData;
    private CatSkinData skinData;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(OpenShipOptionsEvent, OpenShipOptionsEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StartCombatEvent, StartCombatEventCallback));

        shipOptionsView.ShipOptionsController = this;
    }

    #region Event callbacks
    private void OpenShipOptionsEventCallback(Void _item)
    {
        shipOptionsView.gameObject.SetActive(true);
    }

    private void StartCombatEventCallback(Void _item)
    {
        StartCombat();
    }
    #endregion

    #region Public methods
    public void CloseCamera()
    {
        CloseShipCameraEvent.Raise();
    }

    public void OpenCatCrewManagement()
    {
        OpenCatCrewManagementNoIDEvent.Raise();
    }
    #endregion

    public void StartCombat()
    {
        // Load current crew cats
        List<DataSaveCatStructure> catCrewList
            = CatsDataSaveManager.Instance.GetCatShipCrewStructureData();
        CombatData.CatCrewDataList = new CatCombatData[catCrewList.Count];
        CatCombatData catCombatDataHelper;
        // Set cat data and skin data into combat data
        for (int index = 0; index < catCrewList.Count; index++)
        {
            catData = CatsModel.Instance.GetCatData(catCrewList[index].CatType);
            skinData = CatsModel.Instance.GetSkinData(catCrewList[index].SkinType);
            catCombatDataHelper = new CatCombatData();
            catCombatDataHelper.CatData = catData;
            catCombatDataHelper.SkinData = skinData;
            CombatData.CatCrewDataList[catCrewList[index].IslandSlot] = catCombatDataHelper;
        }
        // Load enemy and player ships data
        CombatData.PlayerShipData = playerShipData;
        CombatData.EnemyShipData = islandEnemyShipList.EnemyShipDataList[Random.Range(0, islandEnemyShipList.EnemyShipDataList.Count)];
        // TODO: Trigger ship animation
        LoadCombatSceneEvent.Raise();
    }

    public void LoadCombatScene()
    {
        LoadCombatSceneEvent.Raise();
    }

    public void CloseShipOptions()
    {
        CloseShipCameraEvent.Raise();
        shipOptionsView.gameObject.SetActive(false);
    }

    #region On Destroy
    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion
}
