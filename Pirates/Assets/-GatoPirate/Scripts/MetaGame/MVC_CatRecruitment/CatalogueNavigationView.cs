using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogueNavigationView : MonoBehaviour
{
    [Header("Catalog panels")]
    [SerializeField]
    private GameObject[] pnl_catalogues;

    [Header("UI references")]
    [SerializeField]
    private GameObject btn_arrowNext;
    [SerializeField]
    private GameObject btn_arrowPrevious;
    [SerializeField]
    private CircleIndicatorView[] img_circleIndicators;

    public int currentCatalogIndex = 0;
    public int CatalogueListLength { get; set; }

    public void Initialize()
    {
        CatalogueListLength = pnl_catalogues.Length;
        for (int index = 0; index < CatalogueListLength; index++)
        {
            img_circleIndicators[index].gameObject.SetActive(true);
            img_circleIndicators[index].Initialize();
        }
    }

    public void Initialize(int _catalogueSize)
    {
        CatalogueListLength = _catalogueSize;
        if (CatalogueListLength > 1)
            btn_arrowNext.SetActive(true);

        for (int index = 0; index < CatalogueListLength; index++)
        {
            img_circleIndicators[index].gameObject.SetActive(true);
            img_circleIndicators[index].Initialize();
        }
    }

    public void NextCatalogue()
    {
        currentCatalogIndex++;

        if (currentCatalogIndex < CatalogueListLength)
        {
            btn_arrowPrevious.SetActive(true);

            pnl_catalogues[currentCatalogIndex - 1].SetActive(false);
            pnl_catalogues[currentCatalogIndex].SetActive(true);

            img_circleIndicators[currentCatalogIndex - 1].SetAsInactive();
            img_circleIndicators[currentCatalogIndex].SetAsActive();
        }

        if (currentCatalogIndex + 1 >= CatalogueListLength) // Last item
        {
            btn_arrowNext.SetActive(false);
        }

    }

    public void PreviousCatalogue()
    {
        currentCatalogIndex--;

        if (currentCatalogIndex >= 0)
        {
            btn_arrowNext.SetActive(true);
            pnl_catalogues[currentCatalogIndex + 1].SetActive(false);
            pnl_catalogues[currentCatalogIndex].SetActive(true);

            img_circleIndicators[currentCatalogIndex + 1].SetAsInactive();
            img_circleIndicators[currentCatalogIndex].SetAsActive();
        }

        if (currentCatalogIndex - 1 < 0)
        {
            btn_arrowPrevious.SetActive(false);
        }
    }

    public void RestartCatalogue()
    {
        currentCatalogIndex = 0;
        // Restart arrows
        btn_arrowPrevious.SetActive(false);
        btn_arrowNext.SetActive(true);
        // Restart circle indicators
        img_circleIndicators[0].SetAsActive();
        for (int index = 1; index < CatalogueListLength; index++)
        {
            img_circleIndicators[index].SetAsInactive();
        }
    }
}
