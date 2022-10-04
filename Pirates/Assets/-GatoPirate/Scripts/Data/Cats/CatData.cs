using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cat", menuName = "Gato Pirate/Cat data/Create new cat data", order = 1)]
public class CatData : ScriptableObject
{
    [Header("Cat data")]
    [SerializeField]
    private CatType catType;
    [SerializeField]
    private ItemTier catTier;
    [SerializeField]
    private string catName;

    [Header("Cat sprites")]
    [SerializeField]
    private Sprite catHeadSprite;
    [SerializeField]
    private Sprite catBodySprite;
    [SerializeField]
    private Sprite catLeftPawSprite;
    [SerializeField]
    private Sprite catRightPawSprite;
    [SerializeField]
    private Sprite catLeftBottomPawSprite;
    [SerializeField]
    private Sprite catRightBottomPawSprite;
    [SerializeField]
    private Sprite catTailSprite;
    [SerializeField]
    private Sprite catEyesSprite;

    [Header("Color")]
    [SerializeField]
    private Color catColor = Color.white;

    [Header("Catalogue data")]
    [Header("Cat data")]
    [SerializeField, TextArea]
    private string catDescription;
    [SerializeField]
    private Sprite catPreviewSprite;
    [SerializeField]
    private int catPrice;
    [SerializeField]
    private CurrencyType currencyType;

    [Header("Unlock data")]
    [SerializeField]
    private Island islandToUnlock;
    [SerializeField]
    private bool isUnlocked;

    [Header("Sounds")]
    [SerializeField]
    private CatMeowSounds meowSound;


    // Cat data
    public CatType CatType { get => catType; set => catType = value; }
    public ItemTier CatTier { get => catTier; set => catTier = value; }
    public string CatName { get => catName; set => catName = value; }

    // Cat sprites
    public Sprite CatHeadSprite { get => catHeadSprite; set => catHeadSprite = value; }
    public Sprite CatBodySprite { get => catBodySprite; set => catBodySprite = value; }
    public Sprite CatLeftPawSprite { get => catLeftPawSprite; set => catLeftPawSprite = value; }
    public Sprite CatRightPawSprite { get => catRightPawSprite; set => catRightPawSprite = value; }
    public Sprite CatLeftBottomPawSprite { get => catLeftBottomPawSprite; set => catLeftBottomPawSprite = value; }
    public Sprite CatRightBottomPawSprite { get => catRightBottomPawSprite; set => catRightBottomPawSprite = value; }
    public Sprite CatTailSprite { get => catTailSprite; set => catTailSprite = value; }
    public Sprite CatEyesSprite { get => catEyesSprite; set => catEyesSprite = value; }
    public Color CatColor { get => catColor; set => catColor = value; }

    // Catalogue data
    public string CatDescription { get => catDescription; set => catDescription = value; }
    public Sprite CatPreviewSprite { get => catPreviewSprite; set => catPreviewSprite = value; }
    public int CatPrice { get => catPrice; set => catPrice = value; }
    public CurrencyType CurrencyType { get => currencyType; set => currencyType = value; }

    // Unlock data
    public Island IslandToUnlock { get => islandToUnlock; set => islandToUnlock = value; }
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }
    
    // Sounds
    public CatMeowSounds MeowSound { get => meowSound; set => meowSound = value; }
}
