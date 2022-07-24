using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class CatRecruitmentController : MonoBehaviour
{
    [SerializeField]
    private CatRecruitmentModel catRecruitmentModel;
    [SerializeField]
    private CatRecruitmentView catRecruitmentView;

    // Events
    public StringIntEvent PurchaseCatEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<string, int>.BuildEventHandler(PurchaseCatEvent, PurchaseCatEventCallback));

        FillCatCatalogueData();
        // Fill Cat Skins catalogue data
    }

    private void FillCatCatalogueData()
    {
        // Basic Cats
        GameObject catItemViewHelper;
        CatalogueItemView catalogueCatItemViewHelper;
        CatCatalogueVisualizationData catVisualizationHelper;
        for (int index = 0; index < catRecruitmentModel.CatBasicCatalogueList.Length; index++)
        {
            catVisualizationHelper = catRecruitmentModel.CatBasicCatalogueList[index];
            catItemViewHelper = Instantiate(catRecruitmentView.CatCatalogueItemView);
            catItemViewHelper.transform.SetParent(catRecruitmentView.BasicCatCatalogueContent);
            catalogueCatItemViewHelper = catItemViewHelper.GetComponent<CatalogueItemView>();
            // Events
            catalogueCatItemViewHelper.PurchaseItemEvent = PurchaseCatEvent;
            // Setting data
            catalogueCatItemViewHelper.SetName(catVisualizationHelper.ItemName);
            catalogueCatItemViewHelper.SetSprite(catVisualizationHelper.ItemSprite);
            catalogueCatItemViewHelper.SetPurchasePrice(catVisualizationHelper.ItemPrice);

            // TODO: Check if currency is enough to buy
            if (catVisualizationHelper.IsUnlocked)
                catalogueCatItemViewHelper.SetItemUnlocked();
            else
                catalogueCatItemViewHelper.SetItemLocked();
            // TODO: Add view helper to a list if needed
        }

        // Premium cats
    }

    #region Event callbacks
    private void PurchaseCatEventCallback(string _catName, int _catPrice)
    {
        Debug.Log($"Purchasing product {_catName} with price {_catPrice}");
        // TODO: Get if cat fits in current Island
        // TODO: Then get slot index
        // TODO: Reduce currency amount with item price
        // Save cat data
        CatsDataSaveManager.Instance.SaveNewCat(IDGenerator.Instance.GetGeneratedID(_catName), _catName);
        // TODO: Update cat island if slots are available
    }
    #endregion

    // On destroy
    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
}
