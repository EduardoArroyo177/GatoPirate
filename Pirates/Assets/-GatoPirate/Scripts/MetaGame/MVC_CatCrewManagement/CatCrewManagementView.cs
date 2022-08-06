using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCrewManagementView : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject catView;

    [Header("UI references")]
    [SerializeField]
    private Transform ownedCatsContent1;
    [SerializeField]
    private Transform ownedCatsContent2;
    [SerializeField]
    private Transform ownedCatsContent3;
    [SerializeField]
    private Transform ownedCatsContent4;
    [SerializeField]
    private Transform ownedCatsContent5;
    [SerializeField]
    private Transform ownedCatsContent6;
    [SerializeField]
    private Transform ownedCatsContent7;
    [SerializeField]
    private Transform ownedCatsContent8;
    [SerializeField]
    private Transform ownedCatsContent9;

    public GameObject CatView { get => catView; set => catView = value; }
    public Transform OwnedCatsContent1 { get => ownedCatsContent1; set => ownedCatsContent1 = value; }
}
