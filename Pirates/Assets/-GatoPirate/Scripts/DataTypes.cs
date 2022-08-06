using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Combat
public enum ShipLevelType
{ 
    WOOD,
    SILVER,
    GOLD
}

public enum CannonSide
{ 
    LEFT,
    MIDDLE,
    RIGHT
}

public enum CharacterType
{ 
    PLAYER,
    ENEMY
}

public enum ProjectileType
{
    BASIC,
    NORMAL,
    AUTOMATIC,
    SPECIAL
}
#endregion

public enum CurrencyType
{ 
    GOLDEN_COINS,
    WOOD
}

#region Cats
public enum Cats
{
    GENERIC,
    CAPTAIN,
    ORANGE
}

public enum Skins
{ 
    NONE,
    GANDALF
}

// TODO: Update tier to be generic
public enum ItemTier
{
    CAT_BASIC,
    CAT_SPECIAL,
    SKIN_BASIC,
    SKIN_SPECIAL,
    SKIN_PREMIUM,
    BASIC,
    SPECIAL,
    PREMIUM
}
#endregion

public enum Island
{ 
    ISLAND1,
    ISLAND2,
    ISLAND3
}