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
    private float shipLevelAttackMultiplier; // Used
    [SerializeField]
    private float shipLevelCoolDownMultiplier; // Used
    [SerializeField]
    private float shipLevelBallSpeedMultiplier; // Used
    [SerializeField]
    private float shipLevelHealthMultiplier;
    [SerializeField]
    private float shipLevelSpecialAttackMultiplier; // Used

    [Header("Health")]
    [SerializeField]
    private int shipHealth;

    [Header("Cannon balls")]
    [SerializeField]
    private int cannonBallSpeed; // Used
    [SerializeField]
    private int cannonBallDamage; // Used
    [SerializeField]
    private float cannonCoolDownTime;

    [Header("Attacks")]
    [SerializeField]
    private int basicAttackDamage;
    [SerializeField]
    private float basicAttackCoolDownTime;
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
    public int CannonBallDamage { get => cannonBallDamage; set => cannonBallDamage = value; }
    public float CannonCoolDownTime { get => cannonCoolDownTime; set => cannonCoolDownTime = value; }
    public int BasicAttackDamage { get => basicAttackDamage; set => basicAttackDamage = value; }
    public float BasicAttackCoolDownTime { get => basicAttackCoolDownTime; set => basicAttackCoolDownTime = value; }
    public int SpecialAttackDamage { get => specialAttackDamage; set => specialAttackDamage = value; }
    public float SpecialAttackChargeTime { get => specialAttackChargeTime; set => specialAttackChargeTime = value; }
}
