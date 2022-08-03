using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemVisualization", menuName = "Gato Pirate/Cat data/Create new catalog item visualization data", order = 1)]
public class ItemCatalogueVisualizationData : ScriptableObject
{
    [Header("Cat store data")]
    [SerializeField]
    private Cats catType;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string itemDescription;
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private int itemPrice;

    [Header("Cat unlock data")]
    [SerializeField]
    private Island islandToUnlock;
    [SerializeField]
    private bool isUnlocked;

    public Cats CatType { get => catType; set => catType = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public string ItemDescription { get => itemDescription; set => itemDescription = value; }
    public Sprite ItemSprite { get => itemSprite; set => itemSprite = value; }
    public int ItemPrice { get => itemPrice; set => itemPrice = value; }

    public Island IslandToUnlock { get => islandToUnlock; set => islandToUnlock = value; }
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }
}
