using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatRecruitmentNavigationView : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button btn_catsMenu;
    [SerializeField]
    private Button btn_skinsMenu;

    [Header("Colors")]
    [SerializeField]
    private Color currentActiveBtnColor;
    [SerializeField]
    private Color notSelectedBtnColor;

    [Header("Panels")]
    [SerializeField]
    private GameObject[] scrollViewCatList;
    [SerializeField]
    private GameObject pnl_catCatalogueNavigationView;
    [SerializeField]
    private GameObject[] scrollViewSkinList;
    [SerializeField]
    private GameObject pnl_skinCatalogueNavigationView;

    public void SelectCatsMenu()
    {
        // Close skin panels
        for (int index = 0; index < scrollViewSkinList.Length; index++)
        {
            scrollViewSkinList[index].SetActive(false);
        }
        // Open cats panel 0
        scrollViewCatList[0].SetActive(true);
        // disable cats menu button
        btn_catsMenu.interactable = false;
        btn_catsMenu.image.color = currentActiveBtnColor;
        pnl_catCatalogueNavigationView.SetActive(true);
        // Enable skins menu button
        btn_skinsMenu.interactable = true;
        btn_skinsMenu.image.color = notSelectedBtnColor;
        pnl_skinCatalogueNavigationView.SetActive(false);
        // restart skin navigation
        pnl_skinCatalogueNavigationView.GetComponent<CatalogueNavigationView>().RestartCatalogue();
    }

    public void SelectSkinsMenu()
    {
        // Close cats panels
        for (int index = 0; index < scrollViewCatList.Length; index++)
        {
            scrollViewCatList[index].SetActive(false);
        }
        // Open skins panel 0
        scrollViewSkinList[0].SetActive(true);
        // Disbale skins menu button
        btn_skinsMenu.interactable = false;
        btn_skinsMenu.image.color = currentActiveBtnColor;
        pnl_skinCatalogueNavigationView.SetActive(true);

        // Enable cats menu button
        btn_catsMenu.interactable = true;
        btn_catsMenu.image.color = notSelectedBtnColor;
        pnl_catCatalogueNavigationView.SetActive(false);
        // restart cats navigation
        pnl_catCatalogueNavigationView.GetComponent<CatalogueNavigationView>().RestartCatalogue();

    }
}
