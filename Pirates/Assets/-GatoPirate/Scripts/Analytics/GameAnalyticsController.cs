using GameAnalyticsSDK.Events;
using GameAnalyticsSDK;
using HuaweiService;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAnalyticsController : SceneSingleton<GameAnalyticsController>
{
    public void EarnedCurrencyEvent(CurrencyType _currencyType, int _amount, CurrencyOrigin _currencyOrigin)
    {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, _currencyType.ToString().Replace("_", "")
            ,_amount, _currencyOrigin.ToString(), _currencyOrigin.ToString());
    }
}
