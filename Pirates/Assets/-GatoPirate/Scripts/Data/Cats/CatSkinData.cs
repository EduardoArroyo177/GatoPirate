using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatSkin", menuName = "Gato Pirate/Cat data/Create new cat skin data", order = 1)]
public class CatSkinData : ScriptableObject
{
    [Header("Cat skin sprites")]
    [SerializeField]
    private Sprite catHeadSkinSprite;
    [SerializeField]
    private Sprite catBodySkinSprite;
    [SerializeField]
    private Sprite catLeftPawSkinSprite;
    [SerializeField]
    private Sprite catRightPawSkinSprite;
    [SerializeField]
    private Sprite catLeftBottomPawSkinSprite;
    [SerializeField]
    private Sprite catRightBottomPawSkinSprite;
    [SerializeField]
    private Sprite catTailSkinSprite;

    [Header("Cat skin color")]
    [SerializeField]
    private Color catSkinColor;

    public Sprite CatHeadSkinSprite { get => catHeadSkinSprite; set => catHeadSkinSprite = value; }
    public Sprite CatBodySkinSprite { get => catBodySkinSprite; set => catBodySkinSprite = value; }
    public Sprite CatLeftPawSkinSprite { get => catLeftPawSkinSprite; set => catLeftPawSkinSprite = value; }
    public Sprite CatRightPawSkinSprite { get => catRightPawSkinSprite; set => catRightPawSkinSprite = value; }
    public Sprite CatLeftBottomPawSkinSprite { get => catLeftBottomPawSkinSprite; set => catLeftBottomPawSkinSprite = value; }
    public Sprite CatRightBottomPawSkinSprite { get => catRightBottomPawSkinSprite; set => catRightBottomPawSkinSprite = value; }
    public Sprite CatTailSkinSprite { get => catTailSkinSprite; set => catTailSkinSprite = value; }
    public Color CatSkinColor { get => catSkinColor; set => catSkinColor = value; }
}
