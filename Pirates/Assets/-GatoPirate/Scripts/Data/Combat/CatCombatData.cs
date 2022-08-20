using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CatCombatData 
{
    [SerializeField]
    private CatData catData;
    [SerializeField]
    private CatSkinData skinData;

    public CatData CatData { get => catData; set => catData = value; }
    public CatSkinData SkinData { get => skinData; set => skinData = value; }
}
