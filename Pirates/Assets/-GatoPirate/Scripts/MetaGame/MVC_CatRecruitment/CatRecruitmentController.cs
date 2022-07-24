using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRecruitmentController : MonoBehaviour
{
    [SerializeField]
    private CatRecruitmentModel catRecruitmentModel;
    [SerializeField]
    private CatRecruitmentView catRecruitmentView;

    public void Initialize()
    {
        FillCatCatalogueData();
    }

    private void FillCatCatalogueData()
    {
        GameObject itemViewHelper;
        CatalogueItemView catalogueItemViewHelper;
        CatCatalogueVisualizationData catVisualizationHelper;
        for (int index = 0; index < catRecruitmentModel.CatBasicCatalogueList.Length; index++)
        {
            catVisualizationHelper = catRecruitmentModel.CatBasicCatalogueList[index];
            itemViewHelper = Instantiate(catRecruitmentView.CatCatalogueItemView);
            itemViewHelper.transform.SetParent(catRecruitmentView.BasicCatCatalogueContent);
            catalogueItemViewHelper = itemViewHelper.GetComponent<CatalogueItemView>();
            catalogueItemViewHelper.SetIDAndName(catVisualizationHelper.ItemID,
                catVisualizationHelper.ItemName);
            catalogueItemViewHelper.SetSprite(catVisualizationHelper.ItemSprite);
            catalogueItemViewHelper.SetPurchasePrice(catVisualizationHelper.ItemPrice);
            if (catVisualizationHelper.IsUnlocked)
                catalogueItemViewHelper.SetItemUnlocked();
            else
                catalogueItemViewHelper.SetItemLocked();
            // TODO: Add view helper to a list if needed
        }
    }
}
