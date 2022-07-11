using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBodyBuilder : MonoBehaviour
{
    [Header("Cat data")]
    [SerializeField]
    private CatData catData;

    [Header("Cat sprite renderer references")]
    [SerializeField]
    private SpriteRenderer catHeadRenderer;
    [SerializeField]
    private SpriteRenderer catBodyRenderer;
    [SerializeField]
    private SpriteRenderer catLeftPawRenderer;
    [SerializeField]
    private SpriteRenderer catRightPawRenderer;
    [SerializeField]
    private SpriteRenderer catLeftBottomPawRenderer;
    [SerializeField]
    private SpriteRenderer catRightBottomPawRenderer;
    [SerializeField]
    private SpriteRenderer catTailRenderer;

    [Header("Cat skin data")]
    [SerializeField]
    private CatSkinData catSkinData;

    [Header("Cat skin sprite renderer references")]
    [SerializeField]
    private SpriteRenderer catHeadSkinRenderer;
    [SerializeField]
    private SpriteRenderer catBodySkinRenderer;
    [SerializeField]
    private SpriteRenderer catLeftPawSkinRenderer;
    [SerializeField]
    private SpriteRenderer catRightPawSkinRenderer;
    [SerializeField]
    private SpriteRenderer catLeftBottomPawSkinRenderer;
    [SerializeField]
    private SpriteRenderer catRightBottomPawSkinRenderer;
    [SerializeField]
    private SpriteRenderer catTailSkinRenderer;

    public CatData CatData { get => catData; set => catData = value; }

    private void Awake()
    {
        if(CatData)
            InitializeCat();
    }

    public void InitializeCat()
    {
        // Cat body 
        catHeadRenderer.sprite = CatData.CatHeadSprite;
        catHeadRenderer.color = CatData.CatColor;

        catBodyRenderer.sprite = CatData.CatBodySprite;
        catBodyRenderer.color = CatData.CatColor;

        catLeftPawRenderer.sprite = CatData.CatLeftPawSprite;
        catLeftPawRenderer.color = CatData.CatColor; 

        catRightPawRenderer.sprite = CatData.CatRightPawSprite;
        catRightPawRenderer.color = CatData.CatColor;

        catLeftBottomPawRenderer.sprite = CatData.CatLeftBottomPawSprite;
        catLeftBottomPawRenderer.color = CatData.CatColor;

        catRightBottomPawRenderer.sprite = CatData.CatRightBottomPawSprite;
        catRightBottomPawRenderer.color = CatData.CatColor;

        catTailRenderer.sprite = CatData.CatTailSprite;
        catTailRenderer.color = CatData.CatColor;

        // Cat skin 
        if (!catSkinData)
            return;

        if (catSkinData.CatHeadSkinSprite)
        {
            catHeadSkinRenderer.sprite = catSkinData.CatHeadSkinSprite;
            catHeadSkinRenderer.color = catSkinData.CatSkinColor;
        }

        if (catSkinData.CatBodySkinSprite)
        {
            catBodySkinRenderer.sprite = catSkinData.CatBodySkinSprite;
            catBodySkinRenderer.color = catSkinData.CatSkinColor;
        }

        if (catSkinData.CatLeftPawSkinSprite)
        {
            catLeftPawSkinRenderer.sprite = catSkinData.CatLeftPawSkinSprite;
            catLeftPawSkinRenderer.color = catSkinData.CatSkinColor;
        }

        if (catSkinData.CatRightPawSkinSprite)
        {
            catRightPawSkinRenderer.sprite = catSkinData.CatRightPawSkinSprite;
            catRightPawSkinRenderer.color = catSkinData.CatSkinColor;
        }

        if (catSkinData.CatLeftBottomPawSkinSprite)
        {
            catLeftBottomPawSkinRenderer.sprite = catSkinData.CatLeftBottomPawSkinSprite;
            catLeftBottomPawSkinRenderer.color = catSkinData.CatSkinColor;
        }

        if (catSkinData.CatRightBottomPawSkinSprite)
        {
            catRightBottomPawSkinRenderer.sprite = catSkinData.CatRightBottomPawSkinSprite;
            catRightBottomPawSkinRenderer.color = catSkinData.CatSkinColor;
        }

        if (catSkinData.CatTailSkinSprite)
        {
            catTailSkinRenderer.sprite = catSkinData.CatTailSkinSprite;
            catTailSkinRenderer.color = catSkinData.CatSkinColor;
        }
    }
}
