using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCrewController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private SpriteRenderer catSpriteRenderer;

    // TODO: For testing only?
    public Sprite CatSprite; // {get;set;}
    public Sprite CatSkinSprite; // {get;set;}
    public Color CatSpriteColor; // {get;set;}

    // TODO: Remove this call, method will be manually called from somewhere else
    private void Awake()
    {
        InitializeCat();
    }

    public void InitializeCat()
    {
        catSpriteRenderer.sprite = CatSprite;
        catSpriteRenderer.color = CatSpriteColor;
        if (CatSkinSprite)
        { 
            // Instantiate new child object with skin in front of cat?
        }
    }
}
