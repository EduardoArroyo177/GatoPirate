using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class LeaderboardsController : MonoBehaviour
{
    [SerializeField]
    private LeaderboardsView leaderboardsView;

    [Header("Leaderboards")]
    [SerializeField]
    private string combatsWonLeaderboardID;

    public VoidEvent OpenLeaderboardsEvent { get; set; }
    public StringEvent RequestLeaderboardsDataEvent { get; set; }
    public LeaderboardDataEvent PlayerRankDataEvent { get; set; }
    public LeaderboardDataListEvent LeaderboardRankDataListEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(OpenLeaderboardsEvent, OpenLeaderboardsEventCallback));
        _eventHandlers.Add(EventHandlerFactory<LeaderboardScoreData>.BuildEventHandler(PlayerRankDataEvent, PlayerRankDataEventCallback));
        _eventHandlers.Add(EventHandlerFactory<List<LeaderboardScoreData>>.BuildEventHandler(LeaderboardRankDataListEvent, LeaderboardRankDataListEventCallback));
    }

    #region Event callbacks
    private void OpenLeaderboardsEventCallback(Void _item)
    {
        Debug.Log("HUG?");
        leaderboardsView.gameObject.SetActive(true);
        // Ask if player has login
        if (LeaderboardsDataSaveManager.Instance.GetLoginStatus())
        {
            // Trigger get leaderboards data
            RequestLeaderboardsDataEvent.Raise(combatsWonLeaderboardID);
            // TODO: Show loading panel
        }
        else
        {
            // Show login panel
            leaderboardsView.ShowLoginMissingView();
        }
    }

    private void PlayerRankDataEventCallback(LeaderboardScoreData _playerScoreData)
    {
        leaderboardsView.CurrentPlayerRankView.SetRankIcon(GetRankSprite((int)_playerScoreData.playerRank));
        leaderboardsView.CurrentPlayerRankView.SetPlayerRankData(_playerScoreData.playerDisplayRank,
            _playerScoreData.playerScore.ToString(), _playerScoreData.playerName);
        if (_playerScoreData.playerRank < 4)
            leaderboardsView.CurrentPlayerRankView.SetAsFirstPlaces();
    }

    private void LeaderboardRankDataListEventCallback(List<LeaderboardScoreData> _leaderboardData)
    {
        GameObject rankViewHelper;
        PlayerRankView _playerRankView;
        for (int index = 0; index < _leaderboardData.Count; index++)
        {
            rankViewHelper = Instantiate(leaderboardsView.PlayerRankView);
            rankViewHelper.transform.SetParent(leaderboardsView.RankViewContent);
            _playerRankView = rankViewHelper.GetComponent<PlayerRankView>();
            _playerRankView.SetRankIcon(GetRankSprite((int)_leaderboardData[index].playerRank));
            _playerRankView.SetPlayerRankData(_leaderboardData[index].playerDisplayRank,
                _leaderboardData[index].playerScore.ToString(),
                _leaderboardData[index].playerName);
            if (_leaderboardData[index].playerRank < 4)
                _playerRankView.SetAsFirstPlaces();
        }
    }
    #endregion

    private Sprite GetRankSprite(int _rank)
    {
        if (_rank == 1)
            return leaderboardsView.FirstPlaceSprite;
        else if (_rank == 2)
            return leaderboardsView.SecondPlaceSprite;
        else if (_rank == 3)
            return leaderboardsView.ThirdPlaceSprite;
        else
            return leaderboardsView.GenericPositionSprite;
    }
    

    #region OnDestroy
    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion
}
