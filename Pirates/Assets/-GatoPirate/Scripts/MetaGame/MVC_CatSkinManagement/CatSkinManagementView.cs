using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSkinManagementView : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject skinView;

    [Header("None skin")]
    [SerializeField]
    private string noneSkinName;    // TODO: Update this string with localization
    [SerializeField]
    private Sprite noneSkinSprite;

    [Header("UI references")]
    [SerializeField]
    private Transform skinsCatalogueContent;

    public CatSkinManagementController CatSkinManagementController { get; set; }

    public GameObject SkinView { get => skinView; set => skinView = value; }

    public string NoneSkinName { get => noneSkinName; set => noneSkinName = value; }
    public Sprite NoneSkinSprite { get => noneSkinSprite; set => noneSkinSprite = value; }
    
    public Transform SkinsCatalogueContent { get => skinsCatalogueContent; set => skinsCatalogueContent = value; }

    #region Buttons
    public void Close()
    {
        CatSkinManagementController.Close();
    }

    public void Accept()
    {
        CatSkinManagementController.SaveAndClose();
    }
    #endregion
}
