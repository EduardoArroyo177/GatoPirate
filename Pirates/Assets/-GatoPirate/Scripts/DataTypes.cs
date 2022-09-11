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

public enum CombatShipSounds
{ 
    AUTOMATIC_CANNON_SHOT,
    AUTOMATIC_CANNON_HIT,
    BASIC_CANNON_SHOT,
    BASIC_CANNON_HIT,
    NORMAL_CANNON_SHOT,
    NORMAL_CANNON_HIT,
    SPECIAL_CANNON_SHOT,
    SPECIAL_CANNON_HIT
}

public enum CombatSounds
{ 
    SPECIAL_CANNON_READY,
    WEAK_SPOT_ACTIVE,
    WEAK_SPOT_HIT
}
#endregion

public enum CatMeowSounds
{ 
    SELECTED_CAT1,
    SELECTED_CAT2,
    SELECTED_CAT3
}

public enum ShipSounds
{ 
    SHIP_SELECTED,
    SHIP_SAILING,
    SHIP_SINKING
}
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