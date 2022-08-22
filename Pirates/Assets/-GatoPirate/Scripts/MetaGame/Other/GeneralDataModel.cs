using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDataModel : SceneSingleton<GeneralDataModel>
{
    [SerializeField]
    private Sprite coinsSprite;
    [SerializeField]
    private Sprite woodSprite;
    [SerializeField]
    private Sprite gemSprite;

    public Sprite GetCurrencySprite(CurrencyType _currencyType)
    {
        switch (_currencyType)
        {
            case CurrencyType.GOLDEN_COINS:
                return coinsSprite;
            case CurrencyType.WOOD:
                return woodSprite;
            case CurrencyType.PREMIUM_GEM:
                return gemSprite;
            default:
                return coinsSprite;
        }
    }
}
