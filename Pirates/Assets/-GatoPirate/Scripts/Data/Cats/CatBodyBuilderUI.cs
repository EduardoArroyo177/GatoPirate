using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatBodyBuilderUI : MonoBehaviour
{
    [Header("Cat data")]
    [SerializeField]
    private CatData catData;

    [Header("Cat sprite renderer references")]
    [SerializeField]
    private Image catHeadImage;
    [SerializeField]
    private Image catBodyImage;
    [SerializeField]
    private Image catLeftPawImage;
    [SerializeField]
    private Image catRightPawImage;
    [SerializeField]
    private Image catLeftBottomPawImage;
    [SerializeField]
    private Image catRightBottomPawImage;
    [SerializeField]
    private Image catTailImage;
    [SerializeField]
    private Image catFaceImage;

    [Header("Cat skin data")]
    [SerializeField]
    private CatSkinData catSkinData;

    [Header("Cat skin sprite renderer references")]
    [SerializeField]
    private Image catHeadFrontSkinImage;
    [SerializeField]
    private Image catHeadBackSkinImage;
    [SerializeField]
    private Image catBodyFrontSkinImage;
    [SerializeField]
    private Image catBodyBackSkinImage;
    [SerializeField]
    private Image catLeftPawSkinImage;
    [SerializeField]
    private Image catLeftPawAccesorySkinImage;
    [SerializeField]
    private Image catRightPawSkinImage;
    [SerializeField]
    private Image catRightPawAccesorySkinImage;
    [SerializeField]
    private Image catLeftBottomPawSkinImage;
    [SerializeField]
    private Image catRightBottomPawSkinImage;
    [SerializeField]
    private Image catTailSkinImage;


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

    private void InitializeCat()
    {
        if (!CatData)
            return;

        // Cat body 
        catHeadImage.sprite = CatData.CatHeadSprite;
        catHeadImage.color = CatData.CatColor;
        catHeadImage.gameObject.SetActive(true);

        catBodyImage.sprite = CatData.CatBodySprite;
        catBodyImage.color = CatData.CatColor;
        catBodyImage.gameObject.SetActive(true);

        catLeftPawImage.sprite = CatData.CatLeftPawSprite;
        catLeftPawImage.color = CatData.CatColor;
        catLeftPawImage.gameObject.SetActive(true);

        catRightPawImage.sprite = CatData.CatRightPawSprite;
        catRightPawImage.color = CatData.CatColor;
        catRightPawImage.gameObject.SetActive(true);

        catLeftBottomPawImage.sprite = CatData.CatLeftBottomPawSprite;
        catLeftBottomPawImage.color = CatData.CatColor;
        catLeftBottomPawImage.gameObject.SetActive(true);

        catRightBottomPawImage.sprite = CatData.CatRightBottomPawSprite;
        catRightBottomPawImage.color = CatData.CatColor;
        catRightBottomPawImage.gameObject.SetActive(true);

        catTailImage.sprite = CatData.CatTailSprite;
        catTailImage.color = CatData.CatColor;
        catTailImage.gameObject.SetActive(true);

        // TODO: Select random eyes?
        catFaceImage.sprite = CatData.CatEyesSprite;
        catFaceImage.color = CatData.CatColor;
        catFaceImage.gameObject.SetActive(true);
    }

    private void InitializeSkin()
    {
        if (!catSkinData)
        {
            RestartSkin();
            return;
        }

        // Head
        if (CatSkinData.CatHeadFrontSkinSprite)
        {
            catHeadFrontSkinImage.sprite = CatSkinData.CatHeadFrontSkinSprite;
            catHeadFrontSkinImage.color = CatSkinData.SkinColor;
            catHeadFrontSkinImage.gameObject.SetActive(true);
        }
        else
            catHeadFrontSkinImage.gameObject.SetActive(false);

        if (CatSkinData.CatHeadBackSkinSprite)
        {
            catHeadBackSkinImage.sprite = CatSkinData.CatHeadBackSkinSprite;
            catHeadBackSkinImage.color = CatSkinData.SkinColor;
            catHeadBackSkinImage.gameObject.SetActive(true);
        }
        else
            catHeadBackSkinImage.gameObject.SetActive(false);

        // Body
        if (CatSkinData.CatBodyFrontSkinSprite)
        {
            catBodyFrontSkinImage.sprite = CatSkinData.CatBodyFrontSkinSprite;
            catBodyFrontSkinImage.color = CatSkinData.SkinColor;
            catBodyFrontSkinImage.gameObject.SetActive(true);
        }
        else
            catBodyFrontSkinImage.gameObject.SetActive(false);

        if (CatSkinData.CatBodyBackSkinSprite)
        {
            catBodyBackSkinImage.sprite = CatSkinData.CatBodyBackSkinSprite;
            catBodyBackSkinImage.color = CatSkinData.SkinColor;
            catBodyBackSkinImage.gameObject.SetActive(true);
        }
        else
            catBodyBackSkinImage.gameObject.SetActive(false);

        // Left paw
        if (CatSkinData.CatLeftPawSkinSprite)
        {
            catLeftPawSkinImage.sprite = CatSkinData.CatLeftPawSkinSprite;
            catLeftPawSkinImage.color = CatSkinData.SkinColor;
            catLeftPawSkinImage.gameObject.SetActive(true);

        }
        else
            catLeftPawSkinImage.gameObject.SetActive(false);

        if (CatSkinData.CatLeftPawAccesorySkinSprite)
        {
            catLeftPawAccesorySkinImage.sprite = CatSkinData.CatLeftPawAccesorySkinSprite;
            catLeftPawAccesorySkinImage.color = CatSkinData.SkinColor;
            catLeftPawAccesorySkinImage.gameObject.SetActive(true);

        }
        else
            catLeftPawAccesorySkinImage.gameObject.SetActive(false);

        // Right paw
        if (CatSkinData.CatRightPawSkinSprite)
        {
            catRightPawSkinImage.sprite = CatSkinData.CatRightPawSkinSprite;
            catRightPawSkinImage.color = CatSkinData.SkinColor;
            catRightPawSkinImage.gameObject.SetActive(true);

        }
        else
            catRightPawSkinImage.gameObject.SetActive(false);

        if (CatSkinData.CatRightPawAccesorySkinSprite)
        {
            catRightPawAccesorySkinImage.sprite = CatSkinData.CatRightPawAccesorySkinSprite;
            catRightPawAccesorySkinImage.color = CatSkinData.SkinColor;
            catRightPawAccesorySkinImage.gameObject.SetActive(true);

        }
        else
            catRightPawAccesorySkinImage.gameObject.SetActive(false);

        // Bottom paws 
        if (CatSkinData.CatLeftBottomPawSkinSprite)
        {
            catLeftBottomPawSkinImage.sprite = CatSkinData.CatLeftBottomPawSkinSprite;
            catLeftBottomPawSkinImage.color = CatSkinData.SkinColor;
            catLeftBottomPawSkinImage.gameObject.SetActive(true);

        }
        else
            catLeftBottomPawSkinImage.gameObject.SetActive(false);

        if (CatSkinData.CatRightBottomPawSkinSprite)
        {
            catRightBottomPawSkinImage.sprite = CatSkinData.CatRightBottomPawSkinSprite;
            catRightBottomPawSkinImage.color = CatSkinData.SkinColor;
            catRightBottomPawSkinImage.gameObject.SetActive(true);

        }
        else
            catRightBottomPawSkinImage.gameObject.SetActive(false);

        // Tail
        if (CatSkinData.NoTail)
        {
            catTailImage.gameObject.SetActive(false);
        }
        else
        {
            if (CatSkinData.CatTailSkinSprite)
            {
                catTailSkinImage.sprite = CatSkinData.CatTailSkinSprite;
                catTailSkinImage.color = CatSkinData.SkinColor;
                catTailSkinImage.gameObject.SetActive(true);
            }
            else if (CatSkinData.CatTailSpecialSkinSprite)
            {
                catTailSkinImage.sprite = CatSkinData.CatTailSpecialSkinSprite;
                catTailSkinImage.color = CatSkinData.SkinColor;
                catTailSkinImage.gameObject.SetActive(true);

            }
            else
                catTailSkinImage.gameObject.SetActive(false);
        }
    }

    public void RestartCat()
    {
        catHeadImage.sprite = null;
        catBodyImage.sprite = null;
        catLeftPawImage.sprite = null;
        catRightPawImage.sprite = null;
        catLeftBottomPawImage.sprite = null;
        catRightBottomPawImage.sprite = null;
        catTailImage.sprite = null;
        catFaceImage.sprite = null;
    }

    public void RestartSkin()
    {
        catHeadFrontSkinImage.sprite = null;
        catHeadFrontSkinImage.gameObject.SetActive(false);

        catHeadBackSkinImage.sprite = null;
        catHeadBackSkinImage.gameObject.SetActive(false);

        catBodyFrontSkinImage.sprite = null;
        catBodyFrontSkinImage.gameObject.SetActive(false);

        catBodyBackSkinImage.sprite = null;
        catBodyBackSkinImage.gameObject.SetActive(false);

        catLeftPawSkinImage.sprite = null;
        catLeftPawSkinImage.gameObject.SetActive(false);

        catLeftPawAccesorySkinImage.sprite = null;
        catLeftPawAccesorySkinImage.gameObject.SetActive(false);

        catRightPawSkinImage.sprite = null;
        catRightPawSkinImage.gameObject.SetActive(false);

        catRightPawAccesorySkinImage.sprite = null;
        catRightPawAccesorySkinImage.gameObject.SetActive(false);

        catLeftBottomPawSkinImage.sprite = null;
        catLeftBottomPawSkinImage.gameObject.SetActive(false);

        catRightBottomPawSkinImage.sprite = null;
        catRightBottomPawSkinImage.gameObject.SetActive(false);

        catTailSkinImage.sprite = null;
        catTailSkinImage.gameObject.SetActive(false);
    }
}
