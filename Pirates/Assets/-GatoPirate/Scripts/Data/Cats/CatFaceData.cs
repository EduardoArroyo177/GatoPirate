using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatFace", menuName = "Gato Pirate/Cat data/Create new cat face data", order = 1)]
public class CatFaceData : ScriptableObject
{
    [SerializeField]
    private CatFaceType catFaceType;
    [SerializeField]
    private List<Sprite> catFaceSpriteList;

    public CatFaceType CatFaceType { get => catFaceType; set => catFaceType = value; }
    public List<Sprite> CatFaceSpriteList { get => catFaceSpriteList; set => catFaceSpriteList = value; }
}
