using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : SceneSingleton<CurrencyManager>
{
    public int currentGoldenCoins;
    public int currentWood;

    public int combatEarnedGoldenCoins;
    public int combatEarnedWood;

    private void Awake()
    {
        // TODO: Load current values from playerprefs (SAVE SYSTEM)
        currentGoldenCoins = 0;
        currentWood = 0;
    }

    public void EarnGoldenCoins(int _amount)
    {
        combatEarnedGoldenCoins += _amount;
    }

    public int GetCombatEarnedCoins()
    {
        return combatEarnedGoldenCoins;
    }

    public void EarnWood(int _amount)
    {
        combatEarnedWood += _amount;
    }

    public int GetCombatEarnedWood()
    {
        return combatEarnedWood;
    }
}
