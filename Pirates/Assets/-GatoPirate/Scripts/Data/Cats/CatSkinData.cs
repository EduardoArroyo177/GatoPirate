using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatSkin", menuName = "Gato Pirate/Cat data/Create new cat skin data", order = 1)]
public class CatSkinData : ScriptableObject
{
    [Header("Skin data")]
    [SerializeField]
    private SkinType skinType;
    [SerializeField]
    private ItemTier skinTier;
    [SerializeField]
    private string skinName;

    [Header("Cat skin sprites")]
    [Header("Head")]
    [SerializeField]
    private Sprite catHeadFrontSkinSprite;
    [SerializeField]
    private Sprite catHeadBackSkinSprite;

    [Header("Body")]
    [SerializeField]
    private Sprite catBodyFrontSkinSprite;
    [SerializeField]
    private Sprite catBodyBackSkinSprite;

    [Header("Left paw")]
    [SerializeField]
    private Sprite catLeftPawSkinSprite;
    [SerializeField]
    private Sprite catLeftPawAccesorySkinSprite;

    [Header("Right paw")]
    [SerializeField]
    private Sprite catRightPawSkinSprite;
    [SerializeField]
    private Sprite catRightPawAccesorySkinSprite;

    [Header("Bottom paws")]
    [SerializeField]
    private Sprite catLeftBottomPawSkinSprite;
    [SerializeField]
    private Sprite catRightBottomPawSkinSprite;

    [Header("Tail")]
    [SerializeField]
    private Sprite catTailSkinSprite;
    [SerializeField]
    private Sprite catTailSpecialSkinSprite;
    [SerializeField]
    private bool noTail;

    [Header("Color")]
    [SerializeField]
    private Color skinColor;

    [Header("Catalogue data")]
    [Header("Cat data")]
    [SerializeField]
    private string skinDescription;
    [SerializeField]
    private Sprite skinPreviewSprite;
    [SerializeField]
    private int skinPrice;
    [SerializeField]
    private CurrencyType currencyType;

    [Header("Unlock data")]
    [SerializeField]
    private Island islandToUnlock;
    [SerializeField]
    private bool isUnlocked;

    // Skin data
    public SkinType SkinType { get => skinType; set => skinType = value; }
    public ItemTier SkinTier { get => skinTier; set => skinTier = value; }
    public string SkinName { get => skinName; set => skinName = value; }

    // Skin sprites
    public Sprite CatHeadFrontSkinSprite { get => catHeadFrontSkinSprite; set => catHeadFrontSkinSprite = value; }
    public Sprite CatHeadBackSkinSprite { get => catHeadBackSkinSprite; set => catHeadBackSkinSprite = value; }
    
    public Sprite CatBodyFrontSkinSprite { get => catBodyFrontSkinSprite; set => catBodyFrontSkinSprite = value; }
    public Sprite CatBodyBackSkinSprite { get => catBodyBackSkinSprite; set => catBodyBackSkinSprite = value; }
    
    public Sprite CatLeftPawSkinSprite { get => catLeftPawSkinSprite; set => catLeftPawSkinSprite = value; }
    public Sprite CatLeftPawAccesorySkinSprite { get => catLeftPawAccesorySkinSprite; set => catLeftPawAccesorySkinSprite = value; }
    
    public Sprite CatRightPawSkinSprite { get => catRightPawSkinSprite; set => catRightPawSkinSprite = value; }
    public Sprite CatRightPawAccesorySkinSprite { get => catRightPawAccesorySkinSprite; set => catRightPawAccesorySkinSprite = value; }
    
    public Sprite CatLeftBottomPawSkinSprite { get => catLeftBottomPawSkinSprite; set => catLeftBottomPawSkinSprite = value; }
    public Sprite CatRightBottomPawSkinSprite { get => catRightBottomPawSkinSprite; set => catRightBottomPawSkinSprite = value; }
    
    public Sprite CatTailSkinSprite { get => catTailSkinSprite; set => catTailSkinSprite = value; }
    public Sprite CatTailSpecialSkinSprite { get => catTailSpecialSkinSprite; set => catTailSpecialSkinSprite = value; }
    public bool NoTail { get => noTail; set => noTail = value; }

    public Color SkinColor { get => skinColor; set => skinColor = value; }

    // Catalogue data
    public string SkinDescription { get => skinDescription; set => skinDescription = value; }
    public Sprite SkinPreviewSprite { get => skinPreviewSprite; set => skinPreviewSprite = value; }
    public int SkinPrice { get => skinPrice; set => skinPrice = value; }
    public CurrencyType CurrencyType { get => currencyType; set => currencyType = value; }

    // Unlock data
    public Island IslandToUnlock { get => islandToUnlock; set => islandToUnlock = value; }
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }

}
