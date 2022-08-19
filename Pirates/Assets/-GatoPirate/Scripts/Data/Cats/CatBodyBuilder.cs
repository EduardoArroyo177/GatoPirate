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
        {
            RestartSkinData();
            return;
        }

        // Head
        if (CatSkinData.CatHeadFrontSkinSprite)
        {
            catHeadFrontSkinRenderer.sprite = CatSkinData.CatHeadFrontSkinSprite;
            catHeadFrontSkinRenderer.color = CatSkinData.SkinColor;
        }
        else
            catHeadFrontSkinRenderer.sprite = null;

        if (CatSkinData.CatHeadBackSkinSprite)
        {
            catHeadBackSkinRenderer.sprite = CatSkinData.CatHeadBackSkinSprite;
            catHeadBackSkinRenderer.color = CatSkinData.SkinColor;
        }
        else
            catHeadBackSkinRenderer.sprite = null;

        // Body
        if (CatSkinData.CatBodyFrontSkinSprite)
        {
            catBodyFrontSkinRenderer.sprite = CatSkinData.CatBodyFrontSkinSprite;
            catBodyFrontSkinRenderer.color = CatSkinData.SkinColor;
        }
        else
            catBodyFrontSkinRenderer.sprite = null;

        if (CatSkinData.CatBodyBackSkinSprite)
        {
            catBodyBackSkinRenderer.sprite = CatSkinData.CatBodyBackSkinSprite;
            catBodyBackSkinRenderer.color = CatSkinData.SkinColor;
        }
        else
            catBodyBackSkinRenderer.sprite = null;

        // Left paw
        if (CatSkinData.CatLeftPawSkinSprite)
        {
            catLeftPawSkinRenderer.sprite = CatSkinData.CatLeftPawSkinSprite;
            catLeftPawSkinRenderer.color = CatSkinData.SkinColor;
        }
        else
            catLeftPawSkinRenderer.sprite = null;

        if (CatSkinData.CatLeftPawAccesorySkinSprite)
        {
            catLeftPawAccesorySkinRenderer.sprite = CatSkinData.CatLeftPawAccesorySkinSprite;
            catLeftPawAccesorySkinRenderer.color = CatSkinData.SkinColor;
        }
        else
            catLeftPawAccesorySkinRenderer.sprite = null;

        // Right paw
        if (CatSkinData.CatRightPawSkinSprite)
        {
            catRightPawSkinRenderer.sprite = CatSkinData.CatRightPawSkinSprite;
            catRightPawSkinRenderer.color = CatSkinData.SkinColor;
        }
        else
            catRightPawSkinRenderer.sprite = null;

        if (CatSkinData.CatRightPawAccesorySkinSprite)
        {
            catRightPawAccesorySkinRenderer.sprite = CatSkinData.CatRightPawAccesorySkinSprite;
            catRightPawAccesorySkinRenderer.color = CatSkinData.SkinColor;
        }
        else
            catRightPawAccesorySkinRenderer.sprite = null;

        // Bottom paws 
        if (CatSkinData.CatLeftBottomPawSkinSprite)
        {
            catLeftBottomPawSkinRenderer.sprite = CatSkinData.CatLeftBottomPawSkinSprite;
            catLeftBottomPawSkinRenderer.color = CatSkinData.SkinColor;
        }
        else
            catLeftBottomPawSkinRenderer.sprite = null;

        if (CatSkinData.CatRightBottomPawSkinSprite)
        {
            catRightBottomPawSkinRenderer.sprite = CatSkinData.CatRightBottomPawSkinSprite;
            catRightBottomPawSkinRenderer.color = CatSkinData.SkinColor;
        }
        else
            catRightBottomPawSkinRenderer.sprite = null;

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
            else if (CatSkinData.CatTailSpecialSkinSprite)
            {
                catTailRenderer.sprite = null;
                catTailSkinRenderer.sprite = CatSkinData.CatTailSpecialSkinSprite;
                catTailSkinRenderer.color = CatSkinData.SkinColor;
            }
            else
                catTailSkinRenderer.sprite = null;
        }
    }

    public void RestartCatData()
    {
        catHeadRenderer.sprite = null;
        catBodyRenderer.sprite = null;
        catLeftPawRenderer.sprite = null;
        catRightPawRenderer.sprite = null;
        catLeftBottomPawRenderer.sprite = null;
        catRightBottomPawRenderer.sprite = null;
        catTailRenderer.sprite = null;
        catFaceRenderer.sprite = null;

    }

    public void RestartSkinData()
    {
        catHeadFrontSkinRenderer.sprite = null;
        catHeadBackSkinRenderer.sprite = null;
        catBodyFrontSkinRenderer.sprite = null;
        catBodyBackSkinRenderer.sprite = null;
        catLeftPawSkinRenderer.sprite = null;
        catLeftPawAccesorySkinRenderer.sprite = null;
        catRightPawSkinRenderer.sprite = null;
        catRightPawAccesorySkinRenderer.sprite = null;
        catLeftBottomPawSkinRenderer.sprite = null;
        catRightBottomPawSkinRenderer.sprite = null;
        catTailSkinRenderer.sprite = null;
    }
}
