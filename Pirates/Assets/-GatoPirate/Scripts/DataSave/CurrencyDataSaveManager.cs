using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class CurrencyDataSaveManager : SceneSingleton<CurrencyDataSaveManager>
{
    [Header("Currencies")]
    [SerializeField]
    private DataSaveCurrencyListStructure ownedCurrencyList;
    [SerializeField]
    private DataSaveCurrencyListStructure earnedCurrencyList;

    public const string CURRENCY_SAVING_DATA_KEY = "CURRENCY_OWNED";
    public const int INITIAL_COINS_AMOUNT = 1000;
    public const int INITIAL_WOOD_AMOUNT = 1000;
    public const int INITIAL_GEMS_AMOUNT = 5;
    public const string EARNED_CURRENCY_IN_BATTLE_KEY = "EARNED_CURRENCY";

    public VoidEvent CurrenciesUpdatedEvent { get; set; }
    public DataSaveCurrencyListStructure OwnedCurrencyList { get => ownedCurrencyList; set => ownedCurrencyList = value; }
    public DataSaveCurrencyListStructure EarnedCurrencyList { get => earnedCurrencyList; set => earnedCurrencyList = value; }

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

        string earnedDataSave = PlayerPrefs.GetString(EARNED_CURRENCY_IN_BATTLE_KEY);
        if (string.IsNullOrEmpty(earnedDataSave))
        {
            EarnedCurrencyList = new DataSaveCurrencyListStructure();
            // Golde coins
            DataSaveCurrencyStructure earnedCurrencyStructure = new DataSaveCurrencyStructure();
            earnedCurrencyStructure.CurrencyType = CurrencyType.GOLDEN_COINS.ToString();
            earnedCurrencyStructure.Amount = 0;
            EarnedCurrencyList.CurrencyList.Add(earnedCurrencyStructure);

            // Wood
            earnedCurrencyStructure = new DataSaveCurrencyStructure();
            earnedCurrencyStructure.CurrencyType = CurrencyType.WOOD.ToString();
            earnedCurrencyStructure.Amount = 0;
            EarnedCurrencyList.CurrencyList.Add(earnedCurrencyStructure);

            // Gems
            earnedCurrencyStructure = new DataSaveCurrencyStructure();
            earnedCurrencyStructure.CurrencyType = CurrencyType.PREMIUM_GEM.ToString();
            earnedCurrencyStructure.Amount = 0;
            EarnedCurrencyList.CurrencyList.Add(earnedCurrencyStructure);

            SaveEarnedCurrencyData();
        }
        else
        {
            EarnedCurrencyList = JsonUtility.FromJson<DataSaveCurrencyListStructure>(earnedDataSave);
        }
        Debug.Log("DONE LOADING DATA");
    }
    #endregion

    #region Update data
    // Current currency
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
    // Battle currency
    public int GetEarnedCurrencyAmount(CurrencyType _currencyType)
    {
        int index = EarnedCurrencyList.CurrencyList.FindIndex(x => x.CurrencyType.Equals(_currencyType.ToString()));
        if (index < 0)
        {
            Debug.LogError($"An error occured while retrieving currency {_currencyType}");
            return -1;
        }

        return EarnedCurrencyList.CurrencyList[index].Amount;
    }

    public void UpdateEarnedCurrency(CurrencyType _currencyType, int _amount)
    {
        int index = EarnedCurrencyList.CurrencyList.FindIndex(x => x.CurrencyType.Equals(_currencyType.ToString()));

        if (index < 0)
        {
            Debug.LogError($"An error occured while retrieving currency {_currencyType}");
            return;
        }

        EarnedCurrencyList.CurrencyList[index].Amount += _amount;
        SaveEarnedCurrencyData();
    }
    #endregion

    #region Save data
    private void SaveCurrencyData()
    {
        PlayerPrefs.SetString(CURRENCY_SAVING_DATA_KEY, JsonUtility.ToJson(OwnedCurrencyList));
    }

    private void SaveEarnedCurrencyData()
    {
        PlayerPrefs.SetString(EARNED_CURRENCY_IN_BATTLE_KEY, JsonUtility.ToJson(EarnedCurrencyList));
    }
    #endregion
}
