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

    // Input events
    public BoolEvent LoginSuccessfulEvent { get; set; }
    public VoidEvent OpenLeaderboardsEvent { get; set; }
    public LeaderboardDataEvent PlayerInitialRankDataEvent { get; set; }
    public LeaderboardDataEvent PlayerRankDataEvent { get; set; }
    public LeaderboardDataListEvent LeaderboardRankDataListEvent { get; set; }
    public VoidEvent ScoreSubmittedEvent { get; set; }

    // Output events
    public VoidEvent PlayerLoginEvent { get; set; }
    public StringEvent RequestPlayerScoreEvent { get; set; }
    public StringEvent RequestLeaderboardsDataEvent { get; set; }
    public StringIntEvent SubmitHighScoreEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<bool>.BuildEventHandler(LoginSuccessfulEvent, LoginSuccessfulEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(OpenLeaderboardsEvent, OpenLeaderboardsEventCallback));
        _eventHandlers.Add(EventHandlerFactory<LeaderboardScoreData>.BuildEventHandler(PlayerInitialRankDataEvent, PlayerInitialRankDataEventCallback));
        _eventHandlers.Add(EventHandlerFactory<LeaderboardScoreData>.BuildEventHandler(PlayerRankDataEvent, PlayerRankDataEventCallback));
        _eventHandlers.Add(EventHandlerFactory<List<LeaderboardScoreData>>.BuildEventHandler(LeaderboardRankDataListEvent, LeaderboardRankDataListEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(ScoreSubmittedEvent, ScoreSubmittedEventCallback));

        leaderboardsView.LeaderboardsController = this;
    }

    #region Event callbacks
    private void LoginSuccessfulEventCallback(bool _loginSuccess)
    {
        if (_loginSuccess)
        {
            LeaderboardsDataSaveManager.Instance.IsLoggedIn = true;
            // Hide
            leaderboardsView.ShowLoginMissingView(false);
            Invoke("RequestPlayerScoreData", 0.5f);
        }
    }

    private void OpenLeaderboardsEventCallback(Void _item)
    {
        leaderboardsView.gameObject.SetActive(true);

        // Check if we're logged in
        if (LeaderboardsDataSaveManager.Instance.IsLoggedIn)
        {
            if (!LeaderboardsDataSaveManager.Instance.IsLeaderboardDataLoaded)
            {
                Invoke("RequestPlayerScoreData", 0.5f);
            }
        }
        else
        {
            PlayerLoginEvent.Raise();
        }
    }

    private void PlayerInitialRankDataEventCallback(LeaderboardScoreData _playerScoreData)
    {
        if (_playerScoreData.playerRank > 0)
        {
            leaderboardsView.CurrentPlayerRankView.SetRankIcon(GetRankSprite((int)_playerScoreData.playerRank));
            leaderboardsView.CurrentPlayerRankView.SetPlayerRankData(_playerScoreData.playerDisplayRank,
                _playerScoreData.playerScore.ToString(), _playerScoreData.playerName);
            if (_playerScoreData.playerRank < 4)
                leaderboardsView.CurrentPlayerRankView.SetAsFirstPlaces();
        }

        if (LeaderboardsDataSaveManager.Instance.GetLeaderboardScore(LeaderboardType.COMBATS_WON) > 0)
        {
            // Need to submit score first
            LeaderboardsDataSaveManager.Instance.ScoreToUpdate = (int)_playerScoreData.playerScore
                + LeaderboardsDataSaveManager.Instance.GetLeaderboardScore(LeaderboardType.COMBATS_WON);
            SubmitScore();
        }
        else
        {
            Invoke("RequestLeaderboardsData", 0.5f);
        }
    }

    private void PlayerRankDataEventCallback(LeaderboardScoreData _playerScoreData)
    {
        if (_playerScoreData.playerRank < 0)
            // TODO: Show "nothing to see here"
            return;

        leaderboardsView.CurrentPlayerRankView.SetRankIcon(GetRankSprite((int)_playerScoreData.playerRank));
        leaderboardsView.CurrentPlayerRankView.SetPlayerRankData(_playerScoreData.playerDisplayRank,
            _playerScoreData.playerScore.ToString(), _playerScoreData.playerName);
        if (_playerScoreData.playerRank < 4)
            leaderboardsView.CurrentPlayerRankView.SetAsFirstPlaces();
    }

    private void LeaderboardRankDataListEventCallback(List<LeaderboardScoreData> _leaderboardData)
    {
        StartCoroutine(FillLeaderboardData(_leaderboardData));
    }

    private void ScoreSubmittedEventCallback(Void _item)
    {
        LeaderboardsDataSaveManager.Instance.UpdateLeaderboardScore(LeaderboardType.COMBATS_WON, 0);
        LeaderboardsDataSaveManager.Instance.ScoreToUpdate = 0;
        Invoke("RequestLeaderboardsData", 0.5f);
    }

    #endregion
    private IEnumerator FillLeaderboardData(List<LeaderboardScoreData> _leaderboardData)
    {
        GameObject rankViewHelper;
        PlayerRankView playerRankView;
        yield return new WaitForSeconds(1.0f);

        for (int index = 0; index < _leaderboardData.Count; index++)
        {
            rankViewHelper = Instantiate(leaderboardsView.PlayerRankView, leaderboardsView.RankViewContent);
            playerRankView = rankViewHelper.GetComponent<PlayerRankView>();
            playerRankView.SetRankIcon(GetRankSprite((int)_leaderboardData[index].playerRank));
            playerRankView.SetPlayerRankData(_leaderboardData[index].playerDisplayRank,
                _leaderboardData[index].playerScore.ToString(),
                _leaderboardData[index].playerName);
            if (_leaderboardData[index].playerRank < 4)
                playerRankView.SetAsFirstPlaces();
        }

        LeaderboardsDataSaveManager.Instance.IsLeaderboardDataLoaded = true;
        // Hide loading panel
        leaderboardsView.ShowLoadingDataView(false);
    }

    private void SubmitScore()
    {
        // TODO: Show updating score panel instead of loading data panel
        leaderboardsView.ShowLoadingDataView(true);
        SubmitHighScoreEvent.Raise(combatsWonLeaderboardID, LeaderboardsDataSaveManager.Instance.ScoreToUpdate);
    }

    private void RequestPlayerScoreData()
    {
        Debug.Log("Requesting player score");
        // Show loading panel
        leaderboardsView.ShowLoadingDataView(true);
        RequestPlayerScoreEvent.Raise(combatsWonLeaderboardID);
    }

    private void RequestLeaderboardsData()
    {
        // Show loading panel
        leaderboardsView.ShowLoadingDataView(true);
        RequestLeaderboardsDataEvent.Raise(combatsWonLeaderboardID);
    }

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

    #region Public methods
    public void RequestLogin()
    {
        PlayerLoginEvent.Raise();
    }
    #endregion

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
