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

    private void Awake()
    {
        if(CatData)
            InitializeCat();

        if (catSkinData)
            InitializeSkin();
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

        // TODO: Select random eyes?
        catFaceRenderer.sprite = CatData.CatEyesSprite;
        catFaceRenderer.color = CatData.CatColor;
    }

    public void InitializeSkin()
    {
        // Head
        if (catSkinData.CatHeadFrontSkinSprite)
        {
            catHeadFrontSkinRenderer.sprite = catSkinData.CatHeadFrontSkinSprite;
            catHeadFrontSkinRenderer.color = catSkinData.SkinColor;
        }

        if (catSkinData.CatHeadBackSkinSprite)
        {
            catHeadBackSkinRenderer.sprite = catSkinData.CatHeadBackSkinSprite;
            catHeadBackSkinRenderer.color = catSkinData.SkinColor;
        }

        // Body
        if (catSkinData.CatBodyFrontSkinSprite)
        {
            catBodyFrontSkinRenderer.sprite = catSkinData.CatBodyFrontSkinSprite;
            catBodyFrontSkinRenderer.color = catSkinData.SkinColor;
        }

        if (catSkinData.CatBodyBackSkinSprite)
        {
            catBodyBackSkinRenderer.sprite = catSkinData.CatBodyBackSkinSprite;
            catBodyBackSkinRenderer.color = catSkinData.SkinColor;
        }

        // Left paw
        if (catSkinData.CatLeftPawSkinSprite)
        {
            catLeftPawSkinRenderer.sprite = catSkinData.CatLeftPawSkinSprite;
            catLeftPawSkinRenderer.color = catSkinData.SkinColor;
        }

        if (catSkinData.CatLeftPawAccesorySkinSprite)
        {
            catLeftPawAccesorySkinRenderer.sprite = catSkinData.CatLeftPawAccesorySkinSprite;
            catLeftPawAccesorySkinRenderer.color = catSkinData.SkinColor;
        }

        // Right paw
        if (catSkinData.CatRightPawSkinSprite)
        {
            catRightPawSkinRenderer.sprite = catSkinData.CatRightPawSkinSprite;
            catRightPawSkinRenderer.color = catSkinData.SkinColor;
        }

        if (catSkinData.CatRightPawAccesorySkinSprite)
        {
            catRightPawAccesorySkinRenderer.sprite = catSkinData.CatRightPawAccesorySkinSprite;
            catRightPawAccesorySkinRenderer.color = catSkinData.SkinColor;
        }

        // Bottom paws 
        if (catSkinData.CatLeftBottomPawSkinSprite)
        {
            catLeftBottomPawSkinRenderer.sprite = catSkinData.CatLeftBottomPawSkinSprite;
            catLeftBottomPawSkinRenderer.color = catSkinData.SkinColor;
        }

        if (catSkinData.CatRightBottomPawSkinSprite)
        {
            catRightBottomPawSkinRenderer.sprite = catSkinData.CatRightBottomPawSkinSprite;
            catRightBottomPawSkinRenderer.color = catSkinData.SkinColor;
        }

        // Tail
        if (catSkinData.NoTail)
        {
            catTailRenderer.gameObject.SetActive(false);
        }
        else
        {
            if (catSkinData.CatTailSkinSprite)
            {
                catTailSkinRenderer.sprite = catSkinData.CatTailSkinSprite;
                catTailSkinRenderer.color = catSkinData.SkinColor;
            }

            if (catSkinData.CatTailSpecialSkinSprite)
            {
                catTailSkinRenderer.sprite = catSkinData.CatTailSpecialSkinSprite;
                catTailSkinRenderer.color = catSkinData.SkinColor;
            }
        }
    }
}
