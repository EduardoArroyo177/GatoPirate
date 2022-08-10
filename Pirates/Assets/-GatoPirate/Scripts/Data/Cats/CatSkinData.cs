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
    private string skinName;
    [SerializeField]
    private Sprite skinPreviewSprite;

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
}
