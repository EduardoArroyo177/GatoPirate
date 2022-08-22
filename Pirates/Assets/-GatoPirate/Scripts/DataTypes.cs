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
    WOOD,
    PREMIUM_GEM
}

#region Cats
public enum CatType
{
    GENERIC,
    CAPTAIN,
    ORANGE,
    BLACK,
    WHITE,
    SIAMESE,
    ORANGE_TOULOUSE
}

public enum SkinType
{ 
    NONE,
    GANDALF,
    ARISTOCATS_MARIE,
    ARISTOCATS_TOULOUSE,
    BATMAN,
    PANDA,
    SUPERMAN
}

// TODO: Update tier to be generic
public enum ItemTier
{
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