using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cat", menuName = "Gato Pirate/Cat data/Create new cat data", order = 1)]
public class CatData : ScriptableObject
{
    [Header("Cat data")]
    [SerializeField]
    private Cats catType;
    [SerializeField]
    private string catID;
    [SerializeField]
    private string catName;
    [SerializeField]
    private Sprite catSprite;

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
    private Color catColor;

    public Cats CatType { get => catType; set => catType = value; }
    public string CatID { get => catID; set => catID = value; }
    public string CatName { get => catName; set => catName = value; }
    public Sprite CatSprite { get => catSprite; set => catSprite = value; }

    public Sprite CatHeadSprite { get => catHeadSprite; set => catHeadSprite = value; }
    public Sprite CatBodySprite { get => catBodySprite; set => catBodySprite = value; }
    public Sprite CatLeftPawSprite { get => catLeftPawSprite; set => catLeftPawSprite = value; }
    public Sprite CatRightPawSprite { get => catRightPawSprite; set => catRightPawSprite = value; }
    public Sprite CatLeftBottomPawSprite { get => catLeftBottomPawSprite; set => catLeftBottomPawSprite = value; }
    public Sprite CatRightBottomPawSprite { get => catRightBottomPawSprite; set => catRightBottomPawSprite = value; }
    public Sprite CatTailSprite { get => catTailSprite; set => catTailSprite = value; }
    public Sprite CatEyesSprite { get => catEyesSprite; set => catEyesSprite = value; }
    public Color CatColor { get => catColor; set => catColor = value; }

}
