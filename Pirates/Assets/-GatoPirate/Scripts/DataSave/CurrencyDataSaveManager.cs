using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class CurrencyDataSaveManager : SceneSingleton<CurrencyDataSaveManager>
{
    [Header("Currencies")]
    [SerializeField]
    private DataSaveCurrencyListStructure ownedCurrencyList;

    public const string CURRENCY_SAVING_DATA_KEY = "CURRENCY_OWNED";
    public const int INITIAL_COINS_AMOUNT = 1000;
    public const int INITIAL_WOOD_AMOUNT = 1000;
    public const int INITIAL_GEMS_AMOUNT = 5;

    public VoidEvent CurrenciesUpdatedEvent { get; set; }
    public DataSaveCurrencyListStructure OwnedCurrencyList { get => ownedCurrencyList; set => ownedCurrencyList = value; }


    #region Initialization
    public void LoadCurrencySavedData()
    {
        string dataSave = PlayerPrefs.GetString(CURRENCY_SAVING_DATA_KEY);
        if (string.IsNullOrEmpty(dataSave))
        {
            OwnedCurrencyList = new DataSaveCurrencyListStructure();
            // Golde coins
            DataSaveCurrencyStructure initialCurrencyStructure = new DataSaveCurrencyStructure();
            initialCurrencyStructure.CurrencyType = CurrencyType.GOLDEN_COINS.ToString();
            initialCurrencyStructure.Amount = INITIAL_COINS_AMOUNT;
            OwnedCurrencyList.CurrencyList.Add(initialCurrencyStructure);

            // Wood
            initialCurrencyStructure = new DataSaveCurrencyStructure();
            initialCurrencyStructure.CurrencyType = CurrencyType.WOOD.ToString();
            initialCurrencyStructure.Amount = INITIAL_WOOD_AMOUNT;
            OwnedCurrencyList.CurrencyList.Add(initialCurrencyStructure);

            // Gems
            initialCurrencyStructure = new DataSaveCurrencyStructure();
            initialCurrencyStructure.CurrencyType = CurrencyType.PREMIUM_GEM.ToString();
            initialCurrencyStructure.Amount = INITIAL_GEMS_AMOUNT;
            OwnedCurrencyList.CurrencyList.Add(initialCurrencyStructure);

            SaveCurrencyData();
        }
        else
        {
            // Load saved data
            OwnedCurrencyList = JsonUtility.FromJson<DataSaveCurrencyListStructure>(dataSave);
        }
    }
    #endregion

    #region Update data
    public int GetCurrencyAmount(CurrencyType _currencyType)
    {
        int index = OwnedCurrencyList.CurrencyList.FindIndex(x => x.CurrencyType.Equals(_currencyType.ToString()));
        if (index < 0)
        {
            Debug.LogError($"An error occured while retrieving currency {_currencyType}");
            return -1;
        }

        return OwnedCurrencyList.CurrencyList[index].Amount;
    }

    public void UpdateCurrency(CurrencyType _currencyType, int _amount)
    {
        int index = OwnedCurrencyList.CurrencyList.FindIndex(x => x.CurrencyType.Equals(_currencyType.ToString()));

        if (index < 0)
        {
            Debug.LogError($"An error occured while retrieving currency {_currencyType}");
            return;
        }

        OwnedCurrencyList.CurrencyList[index].Amount += _amount;
        SaveCurrencyData();
        CurrenciesUpdatedEvent.Raise();
    }
    #endregion

    #region Save data
    private void SaveCurrencyData()
    {
        PlayerPrefs.SetString(CURRENCY_SAVING_DATA_KEY, JsonUtility.ToJson(OwnedCurrencyList));
    }
    #endregion
}
