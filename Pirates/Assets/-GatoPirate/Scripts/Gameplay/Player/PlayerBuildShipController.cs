using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildShipController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] crewSlotList;
    [SerializeField]
    private GameObject catGeneric;

    public CatCrewController[] CatCrewControllerObjectsList { get; set; }

    // TODO: Make this as property after testing
    public List<GameObject> crewMembersList;// { get; set; }

    public void Initialize()
    {
        crewMembersList = new List<GameObject>();
        SetCrewCats();
    }

    private void SetCrewCats()
    {
        GameObject crewCatHelper;

        for (int index = 0; index < CatCrewControllerObjectsList.Length; index++)
        {
            crewCatHelper = Instantiate(catGeneric);
            crewCatHelper.transform.position = crewSlotList[index].transform.position;
            crewCatHelper.transform.SetParent(crewSlotList[index].transform);

            CatCrewController catControllerHelper = crewCatHelper.GetComponent<CatCrewController>();
            catControllerHelper.CatSprite = CatCrewControllerObjectsList[index].CatSprite;
            catControllerHelper.CatSkinSprite = CatCrewControllerObjectsList[index].CatSkinSprite;
            catControllerHelper.CatSpriteColor = CatCrewControllerObjectsList[index].CatSpriteColor;
            catControllerHelper.InitializeCat();
            crewMembersList.Add(crewCatHelper);
        }
    }
}
