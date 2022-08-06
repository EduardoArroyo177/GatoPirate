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
    [SerializeField]
    private SpriteRenderer catFaceRenderer;

    [Header("Cat skin data")]
    [SerializeField]
    private CatSkinData catSkinData;

    [Header("Cat skin sprite renderer references")]
    [SerializeField]
    private SpriteRenderer catHeadFrontSkinRenderer;
    [SerializeField]
    private SpriteRenderer catHeadBackSkinRenderer;
    [SerializeField]
    private SpriteRenderer catBodyFrontSkinRenderer;
    [SerializeField]
    private SpriteRenderer catBodyBackSkinRenderer;
    [SerializeField]
    private SpriteRenderer catLeftPawSkinRenderer;
    [SerializeField]
    private SpriteRenderer catLeftPawAccesorySkinRenderer;
    [SerializeField]
    private SpriteRenderer catRightPawSkinRenderer;
    [SerializeField]
    private SpriteRenderer catRightPawAccesorySkinRenderer;
    [SerializeField]
    private SpriteRenderer catLeftBottomPawSkinRenderer;
    [SerializeField]
    private SpriteRenderer catRightBottomPawSkinRenderer;
    [SerializeField]
    private SpriteRenderer catTailSkinRenderer;

    public CatData CatData { get => catData; set => catData = value; }
    public CatSkinData CatSkinData { get => catSkinData; set => catSkinData = value; }

    private void Awake()
    {
        InitializeData();
    }

    public void InitializeData()
    {
        InitializeCat();
        InitializeSkin();
    }

    public void InitializeCat()
    {
        if (!CatData)
            return;

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

        // TODO: Select random eyes?
        catFaceRenderer.sprite = CatData.CatEyesSprite;
        catFaceRenderer.color = CatData.CatColor;
    }

    public void InitializeSkin()
    {
        if (!catSkinData)
            return;

        // Head
        if (CatSkinData.CatHeadFrontSkinSprite)
        {
            catHeadFrontSkinRenderer.sprite = CatSkinData.CatHeadFrontSkinSprite;
            catHeadFrontSkinRenderer.color = CatSkinData.SkinColor;
        }

        if (CatSkinData.CatHeadBackSkinSprite)
        {
            catHeadBackSkinRenderer.sprite = CatSkinData.CatHeadBackSkinSprite;
            catHeadBackSkinRenderer.color = CatSkinData.SkinColor;
        }

        // Body
        if (CatSkinData.CatBodyFrontSkinSprite)
        {
            catBodyFrontSkinRenderer.sprite = CatSkinData.CatBodyFrontSkinSprite;
            catBodyFrontSkinRenderer.color = CatSkinData.SkinColor;
        }

        if (CatSkinData.CatBodyBackSkinSprite)
        {
            catBodyBackSkinRenderer.sprite = CatSkinData.CatBodyBackSkinSprite;
            catBodyBackSkinRenderer.color = CatSkinData.SkinColor;
        }

        // Left paw
        if (CatSkinData.CatLeftPawSkinSprite)
        {
            catLeftPawSkinRenderer.sprite = CatSkinData.CatLeftPawSkinSprite;
            catLeftPawSkinRenderer.color = CatSkinData.SkinColor;
        }

        if (CatSkinData.CatLeftPawAccesorySkinSprite)
        {
            catLeftPawAccesorySkinRenderer.sprite = CatSkinData.CatLeftPawAccesorySkinSprite;
            catLeftPawAccesorySkinRenderer.color = CatSkinData.SkinColor;
        }

        // Right paw
        if (CatSkinData.CatRightPawSkinSprite)
        {
            catRightPawSkinRenderer.sprite = CatSkinData.CatRightPawSkinSprite;
            catRightPawSkinRenderer.color = CatSkinData.SkinColor;
        }

        if (CatSkinData.CatRightPawAccesorySkinSprite)
        {
            catRightPawAccesorySkinRenderer.sprite = CatSkinData.CatRightPawAccesorySkinSprite;
            catRightPawAccesorySkinRenderer.color = CatSkinData.SkinColor;
        }

        // Bottom paws 
        if (CatSkinData.CatLeftBottomPawSkinSprite)
        {
            catLeftBottomPawSkinRenderer.sprite = CatSkinData.CatLeftBottomPawSkinSprite;
            catLeftBottomPawSkinRenderer.color = CatSkinData.SkinColor;
        }

        if (CatSkinData.CatRightBottomPawSkinSprite)
        {
            catRightBottomPawSkinRenderer.sprite = CatSkinData.CatRightBottomPawSkinSprite;
            catRightBottomPawSkinRenderer.color = CatSkinData.SkinColor;
        }

        // Tail
        if (CatSkinData.NoTail)
        {
            catTailRenderer.gameObject.SetActive(false);
        }
        else
        {
            if (CatSkinData.CatTailSkinSprite)
            {
                catTailSkinRenderer.sprite = CatSkinData.CatTailSkinSprite;
                catTailSkinRenderer.color = CatSkinData.SkinColor;
            }

            if (CatSkinData.CatTailSpecialSkinSprite)
            {
                catTailSkinRenderer.sprite = CatSkinData.CatTailSpecialSkinSprite;
                catTailSkinRenderer.color = CatSkinData.SkinColor;
            }
        }
    }

    public void RestartData()
    {
        catHeadRenderer.sprite = null;
        catBodyRenderer.sprite = null;
        catLeftPawRenderer.sprite = null;
        catRightPawRenderer.sprite = null;
        catLeftBottomPawRenderer.sprite = null;
        catRightBottomPawRenderer.sprite = null;
        catTailRenderer.sprite = null;
        catFaceRenderer.sprite = null;

        catHeadFrontSkinRenderer.sprite = null;
        catHeadBackSkinRenderer.sprite = null;
        catBodyFrontSkinRenderer.sprite = null;
        catBodyBackSkinRenderer.sprite = null;
        catLeftPawSkinRenderer.sprite = null;
        catLeftPawAccesorySkinRenderer.sprite = null;
        catRightPawSkinRenderer.sprite = null;
        catRightPawAccesorySkinRenderer.sprite = null;
        catLeftBottomPawSkinRenderer.sprite = null;
        catRightBottomPawSkinRenderer = null;
        catTailSkinRenderer.sprite = null;
    }
}
