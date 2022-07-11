using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : ScriptableObject
{
    [Header("Basic ship data")]
    [SerializeField]
    private string shipID;
    [SerializeField]
    private ShipLevelType shipLevelType;
    [SerializeField]
    private int shipLevel;
    [SerializeField]
    private string shipName;

    [Header("Multipliers")]
    [SerializeField]
    private float shipLevelAttackMultiplier; 
    [SerializeField]
    private float shipLevelCoolDownMultiplier; 
    [SerializeField]
    private float shipLevelBallSpeedMultiplier; 
    [SerializeField]
    private float shipLevelHealthMultiplier; 
    [SerializeField]
    private float shipLevelSpecialAttackMultiplier; 

    [Header("Health")]
    [SerializeField]
    private int shipHealth;

    [Header("Cannon ball")]
    [SerializeField]
    private int cannonBallSpeed; 

    // Attacks
    [Header("Basic attack")]
    [SerializeField]
    private int basicAttackDamage;

    [Header("Normal attack")]
    [SerializeField]
    private int normalAttackDamage; 
    [SerializeField]
    private float normalAttackCoolDownTime;

    [Header("Automatic attack")]
    [SerializeField]
    private int automaticAttackDamage;
    [SerializeField]
    private float automaticAttackCoolDownTime;
    [SerializeField]
    private float automaticAttackFireRate;

    [Header("Special attack")]
    [SerializeField]
    private int specialAttackDamage;
    [SerializeField]
    private float specialAttackChargeTime;

    public string ShipID { get => shipID; set => shipID = value; }
    public ShipLevelType ShipLevelType { get => shipLevelType; set => shipLevelType = value; }
    public int ShipLevel { get => shipLevel; set => shipLevel = value; }
    public string ShipName { get => shipName; set => shipName = value; }
    public float ShipLevelAttackMultiplier { get => shipLevelAttackMultiplier; set => shipLevelAttackMultiplier = value; }
    public float ShipLevelCoolDownMultiplier { get => shipLevelCoolDownMultiplier; set => shipLevelCoolDownMultiplier = value; }
    public float ShipLevelBallSpeedMultiplier { get => shipLevelBallSpeedMultiplier; set => shipLevelBallSpeedMultiplier = value; }
    public float ShipLevelHealthMultiplier { get => shipLevelHealthMultiplier; set => shipLevelHealthMultiplier = value; }
    public float ShipLevelSpecialAttackMultiplier { get => shipLevelSpecialAttackMultiplier; set => shipLevelSpecialAttackMultiplier = value; }
    public int ShipHealth { get => shipHealth; set => shipHealth = value; }
    public int CannonBallSpeed { get => cannonBallSpeed; set => cannonBallSpeed = value; }
    // Attacks
    public int BasicAttackDamage { get => basicAttackDamage; set => basicAttackDamage = value; }
    public int NormalAttackDamage { get => normalAttackDamage; set => normalAttackDamage = value; }
    public float NormalAttackCoolDownTime { get => normalAttackCoolDownTime; set => normalAttackCoolDownTime = value; }
    public int AutomaticAttackDamage { get => automaticAttackDamage; set => automaticAttackDamage = value; }
    public float AutomaticAttackCoolDownTime { get => automaticAttackCoolDownTime; set => automaticAttackCoolDownTime = value; }
    public float AutomaticAttackFireRate { get => automaticAttackFireRate; set => automaticAttackFireRate = value; }
    public int SpecialAttackDamage { get => specialAttackDamage; set => specialAttackDamage = value; }
    public float SpecialAttackChargeTime { get => specialAttackChargeTime; set => specialAttackChargeTime = value; }
}
