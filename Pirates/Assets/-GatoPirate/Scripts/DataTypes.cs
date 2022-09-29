using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Scenes
public enum GameScenes
{
    Combat,
    MainMenu
}
#endregion

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
    WEAK_SPOT_HIT,
    RESULT_SCREEN_SHOWN,
    COINS_REWARD_SHOWN,
    COINS_REWARD_FINISHED,
    REVIVE_SCREEN_SHOWN,
    REVIVE_SUCCESS
}
#endregion

#region UI Sounds
public enum CatMeowSounds
{ 
    SELECTED_CAT1,
    SELECTED_CAT2,
    SELECTED_CAT3,
    SKIN_CHANGED_CAT1,
    SKIN_CHANGED_CAT2,
    SKIN_CHANGED_CAT3,
    CREW_SWITCHED_CAT1,
    CREW_SWITCHED_CAT2,
    CREW_SWITCHED_CAT3
}

public enum ShipSounds
{ 
    SHIP_SELECTED,
    SHIP_SAILING,
    SHIP_SINKING
}

public enum UISounds
{ 
    STORE_MUSIC,
    STORE_ITEM_PURCHASED,
    MENU_RESULT_SCREEN_MUSIC,
    MENU_RESULT_SCREEN_EARNED_COINS,
    MENU_RESULT_SCREEN_ADDED_COINS
}
#endregion

#region Currency
public enum CurrencyType
{ 
    GOLDEN_COINS,
    WOOD,
    PREMIUM_GEM
}
#endregion

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

#region Tutorial
public enum TutorialType
{ 
    FIRST_COMBAT,
    COMBAT,
    COMBAT_WEAK_SPOT,
    COMBAT_RESOURCES_BOX,
    META_GAME,
    META_GAME_RECRUITMENT,
    META_GAME_ISLAND,
    META_GAME_CREW,
    FREE_FIRST_RECRUITMENT
}
#endregion

#region IAP
public enum NonConsumableIAP
{ 
    REMOVE_ADS
}
#endregion

#region Islands
public enum Island
{ 
    ISLAND1,
    ISLAND2,
    ISLAND3
}
#endregion