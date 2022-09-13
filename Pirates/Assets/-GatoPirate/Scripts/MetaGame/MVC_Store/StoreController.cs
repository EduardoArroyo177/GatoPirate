using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    [SerializeField]
    private StoreView storeView;
    [SerializeField]
    private List<StoreItemView> storeItemViewList;

    public VoidEvent OpenStoreEvent { get; set; }
    public StringEvent PurchaseStoreItemEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(OpenStoreEvent, OpenStoreEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(PurchaseStoreItemEvent, PurchaseStoreItemEventCallback));
        InitializeItemViewList();
        // Initialize whatever stuff is needed for IAP
    }

    private void InitializeItemViewList()
    {
        for (int index = 0; index < storeItemViewList.Count; index++)
        {
            storeItemViewList[index].PurchaseStoreItemEvent = PurchaseStoreItemEvent;
            // TODO: Update id with the correct string from Huawei IAP
            storeItemViewList[index].StoreItemID = $"Item_{index}";
            // TODO: Fill information with correct Huawei IAP data
            // Index 0 is always special offer
            //storeItemViewList[index].SetStoreItemData()
        }
    }

    #region Event callbacks
    private void OpenStoreEventCallback(Void _item)
    {
        storeView.gameObject.SetActive(true);
    }

    private void PurchaseStoreItemEventCallback(string _storeItemID)
    {
        Debug.Log($"Purchasing {_storeItemID}");
    }
    #endregion


    #region OnDestroy
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
