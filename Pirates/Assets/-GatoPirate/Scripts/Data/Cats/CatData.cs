using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cat", menuName = "Gato Pirate/Cat data/Create new cat data", order = 1)]
public class CatData : ScriptableObject
{
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
    private bool isTailInC;

    [Header("Cat color")]
    [SerializeField]
    private Color catColor;

    public Sprite CatHeadSprite { get => catHeadSprite; set => catHeadSprite = value; }
    public Sprite CatBodySprite { get => catBodySprite; set => catBodySprite = value; }
    public Sprite CatLeftPawSprite { get => catLeftPawSprite; set => catLeftPawSprite = value; }
    public Sprite CatRightPawSprite { get => catRightPawSprite; set => catRightPawSprite = value; }
    public Sprite CatLeftBottomPawSprite { get => catLeftBottomPawSprite; set => catLeftBottomPawSprite = value; }
    public Sprite CatRightBottomPawSprite { get => catRightBottomPawSprite; set => catRightBottomPawSprite = value; }
    public Sprite CatTailSprite { get => catTailSprite; set => catTailSprite = value; }
    public Color CatColor { get => catColor; set => catColor = value; }
    public bool IsTailInC { get => isTailInC; set => isTailInC = value; }
}
