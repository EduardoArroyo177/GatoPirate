using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PurchasesDataSaveManager : SceneSingleton<PurchasesDataSaveManager>
{
    [SerializeField]
    private DataSaveNonConsumableIAPListStructure purchasedIAP;

    public VoidEvent RemoveAdsPurchasedEvent { get; set; }

    public const string IAP_SAVING_DATA_KEY = "PURCHASED_IAP";

    public DataSaveNonConsumableIAPListStructure PurchasedIAP { get => purchasedIAP; set => purchasedIAP = value; }

    #region Initialization
    public void LoadPurchaseIAPSavedData()
    {
        string dataSave = PlayerPrefs.GetString(IAP_SAVING_DATA_KEY);
        if (string.IsNullOrEmpty(dataSave))
        {
            PurchasedIAP = new DataSaveNonConsumableIAPListStructure();
            DataSaveNonConsumableIAPStructure initialIAPStructure = new DataSaveNonConsumableIAPStructure();
            initialIAPStructure.PurchasedIAPType = NonConsumableIAP.REMOVE_ADS.ToString();
            initialIAPStructure.IsPurchased = false;
            PurchasedIAP.PurchasedIAPList.Add(initialIAPStructure);

            SaveIAPData();
        }
        else
        {
            // Load saved data
            PurchasedIAP = JsonUtility.FromJson<DataSaveNonConsumableIAPListStructure>(dataSave);
        }
    }
    #endregion

    #region Update data
    public bool GetPurchasedNonConsumableStatus(NonConsumableIAP _iapType)
    {
        int index = PurchasedIAP.PurchasedIAPList.FindIndex(x => x.PurchasedIAPType.Equals(_iapType.ToString()));

        if (index < 0)
        {
            Debug.LogError($"An error occured while retrieving IAP data: {_iapType}");
            return false;
        }

        return PurchasedIAP.PurchasedIAPList[index].IsPurchased;
    }

    public void UpdatePurchasedNonConsumable(NonConsumableIAP _iapType)
    {
        int index = PurchasedIAP.PurchasedIAPList.FindIndex(x => x.PurchasedIAPType.Equals(_iapType.ToString()));

        if (index < 0)
        {
            Debug.LogError($"An error occured while retrieving IAP data: {_iapType}");
            return;
        }

        PurchasedIAP.PurchasedIAPList[index].IsPurchased = true;
        SaveIAPData();
        RemoveAdsPurchasedEvent.Raise();
    }

    public void CallForPurchasedIAP()
    {
        for (int index = 0; index < PurchasedIAP.PurchasedIAPList.Count; index++)
        {
            if (PurchasedIAP.PurchasedIAPList[index].IsPurchased)
            {
                if (PurchasedIAP.PurchasedIAPList[index].PurchasedIAPType.Equals(NonConsumableIAP.REMOVE_ADS.ToString()))
                {
                    RemoveAdsPurchasedEvent.Raise();
                }
            }
            
        }
    }
    #endregion

    #region Save data
    private void SaveIAPData()
    {
        PlayerPrefs.SetString(IAP_SAVING_DATA_KEY, JsonUtility.ToJson(PurchasedIAP));
    }
    #endregion
}
