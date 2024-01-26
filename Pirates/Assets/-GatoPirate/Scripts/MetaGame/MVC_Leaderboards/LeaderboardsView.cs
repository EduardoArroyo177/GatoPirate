using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class LeaderboardsView : MonoBehaviour
{
   
    [SerializeField]
    private GameObject pnl_LoadingData;
    [SerializeField]
    private Transform rankViewContent;
    [SerializeField]
    private GameObject pnl_NoRecordsFound;

    [Header("Current player rank")]
    [SerializeField]
    private PlayerRankView currentPlayerRankView;

    [Header("Prefab")]
    [SerializeField]
    private GameObject playerRankView;

    [Header("Rank icon sprites")]
    [SerializeField]
    private Sprite firstPlaceSprite;
    [SerializeField]
    private Sprite secondPlaceSprite;
    [SerializeField]
    private Sprite thirdPlaceSprite;
    [SerializeField]
    private Sprite genericPositionSprite;

    [Header("Log in")]
    [SerializeField]
    private GameObject pnl_LoginMissing;
    [SerializeField]
    private TextMeshProUGUI lbl_content;

    public LeaderboardsController LeaderboardsController { get; set; }

    // Events
    public BoolEvent OpenScreenEvent { get; set; }

    public Transform RankViewContent { get => rankViewContent; set => rankViewContent = value; }

    // Current player
    public PlayerRankView CurrentPlayerRankView { get => currentPlayerRankView; set => currentPlayerRankView = value; }
    // Prefab
    public GameObject PlayerRankView { get => playerRankView; set => playerRankView = value; }
    // Sprites
    public Sprite FirstPlaceSprite { get => firstPlaceSprite; set => firstPlaceSprite = value; }
    public Sprite SecondPlaceSprite { get => secondPlaceSprite; set => secondPlaceSprite = value; }
    public Sprite ThirdPlaceSprite { get => thirdPlaceSprite; set => thirdPlaceSprite = value; }
    public Sprite GenericPositionSprite { get => genericPositionSprite; set => genericPositionSprite = value; }

    // Login needed
    public void ShowLoginMissingView(bool _show)
    {
        pnl_LoginMissing.SetActive(_show);
    }

    // Loading data
    public void ShowLoadingDataView(bool _show)
    {
        pnl_LoadingData.SetActive(_show); 
    }

    // No records found
    public void ShowNoRecordsFoundView(bool _show)
    {
        pnl_NoRecordsFound.SetActive(true);
    }

    public void ShowLoginRequested()
    {
        lbl_content.text = "Signing in...";
    }

    public void ShowLeaderboardsRequested()
    {
        lbl_content.text = "Requesting leaderboards...";
    }

    public void ShowLeaderboardsPopUp(bool _open)
    {
        gameObject.SetActive(_open);
        OpenScreenEvent.Raise(_open);
    }

    #region Button methods
    public void Login()
    {
        LeaderboardsController.RequestLogin();
    }
    #endregion
}
