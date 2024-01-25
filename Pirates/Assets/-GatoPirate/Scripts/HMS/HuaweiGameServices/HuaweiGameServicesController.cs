using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HuaweiGameServicesManager;
using UnityAtoms.BaseAtoms;
using UnityAtoms;
using HmsPlugin;
using HuaweiMobileServices.Base;
using HuaweiMobileServices.Utils;
using HuaweiMobileServices.Game;
using HuaweiMobileServices.Id;
using System;

public class HuaweiGameServicesController : MonoBehaviour
{
    #region Events
    // Login
    public VoidEvent PlayerLoginEvent { get; set; }

    // Leaderboards
    public BoolEvent LeaderboardsDataRetrievedEvent { get; set; }
    public StringEvent RequestLeaderboardsDataEvent { get; set; }
    public LeaderboardDataEvent PlayerInitialRankDataEvent { get; set; }
    public LeaderboardDataEvent PlayerRankDataEvent { get; set; }
    public LeaderboardDataListEvent LeaderboardRankDataListEvent { get; set; }
    public StringEvent RequestPlayerScoreEvent { get; set; }
    public StringIntEvent SubmitHighScoreEvent { get; set; }
    public VoidEvent ScoreSubmittedEvent { get; set; }
    #endregion

    #region Huawei stuff
    private AccountAuthService authService;
    private AccountAuthParams authParams;
    public Action<AuthAccount> SignInSuccess { get; set; }
    public Action<HMSException> SignInFailure { get; set; }
    #endregion

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(PlayerLoginEvent, PlayerLoginEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(RequestPlayerScoreEvent, RequestPlayerScoreEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string,int>.BuildEventHandler(SubmitHighScoreEvent, SubmitHighScoreEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(RequestLeaderboardsDataEvent, RequestLeaderboardsDataEventCallback));

        // Huawei game services
        HMSGameServiceManager.Instance.OnGetPlayerInfoSuccess = OnGetPlayerInfoSuccess;
        HMSGameServiceManager.Instance.OnGetPlayerInfoFailure = OnFailureListener;

#if UNITY_ANDROID && !UNITY_EDITOR
#endif
    }
    #region Initialization
    private void InitJosApps()
    {
        authParams = new AccountAuthParamsHelper(AccountAuthParams.DEFAULT_AUTH_REQUEST_PARAM_GAME).CreateParams();
        var appParams = new AppParams(authParams);
        Debug.Log("HMS GAMES: Setted app");
        IJosAppsClient josAppsClient = JosApps.GetJosAppsClient();
        Debug.Log("HMS GAMES: jossClient");
        josAppsClient.Init(appParams);
        HuaweiAccountLoginManager.Instance.JosInit = true;

        Debug.Log("HMS GAMES: jossClient init");
        Invoke("InitGameManagers", 1.5f);
    }

    private void InitGameManagers()
    {
        HMSLeaderboardManager.Instance.rankingsClient = Games.GetRankingsClient();
    }
    #endregion

    #region Event callbacks
    private void PlayerLoginEventCallback(UnityAtoms.Void _item)
    {
        Login();
    }

    private void RequestPlayerScoreEventCallback(string _leaderboardID)
    {
        if (HuaweiAccountLoginManager.Instance.JosInit)
        {
            ShowLeaderboards();
        }
        else
        {
            InitJosApps();
            Invoke("ShowLeaderboards", 4.0f);
        }
    }

    private void RequestLeaderboardsDataEventCallback(string _leaderboardID)
    {
        GetCurrentPlayerScores(_leaderboardID);
        GetLeaderboardScores(_leaderboardID);
    }

    private void SubmitHighScoreEventCallback(string _leaderboardID, int _score)
    {
        //_submitScoreListener = new MySubmitScoreListener(ScoreSubmittedEvent);
        SubmitScore(_leaderboardID, _score);
    }
    #endregion


    #region Player login data
    public void Login()
    {
#if UNITY_EDITOR
        return;
#endif
        HuaweiAccountLoginManager.Instance.Login();
    }

    public void GetCurrentPlayer()
    {
        Debug.Log("start getCurrentPlayer.");
        //HuaweiGameService.GetCurrentPlayer(true, _getPlayerListener);
    }
    #endregion

    #region Leaderboards
    private void ShowLeaderboards()
    {
        Debug.Log("HMS Showing leaderboards");
        LeaderboardsDataRetrievedEvent.Raise(true);
        HMSLeaderboardManager.Instance.ShowLeaderboards();
        HMSLeaderboardManager.Instance.OnShowLeaderboardsSuccess = OnShowLeaderboardsSuccess;
        HMSLeaderboardManager.Instance.OnShowLeaderboardsFailure = OnShowLeaderboardsFailure;

    }

    public void GetCurrentPlayerScores(string _leaderboardID)
    {
        //HMSLeaderboardManager.Instance.ShowLeaderboards();
        //HMSLeaderboardManager.Instance.OnShowLeaderboardsSuccess = OnShowLeaderboardsSuccess;
        //HMSLeaderboardManager.Instance.OnShowLeaderboardsFailure = OnShowLeaderboardsFailure;

        //HuaweiGameService.GetCurrentPlayerLeaderboardScore(_leaderboardID, 1, _getLeaderboardScoreListener);
    }

    private void OnShowLeaderboardsSuccess()
    {
        LeaderboardsDataRetrievedEvent.Raise(true);
        Debug.Log("HMS ShowLeaderboards SUCCESS.");
    }

    private void OnShowLeaderboardsFailure(HMSException exception)
    {
        LeaderboardsDataRetrievedEvent.Raise(false);
        Debug.LogError("HMS ShowLeaderboards ERROR: " + exception);
    }

    public void GetLeaderboardScores(string _leaderboardID)
    {
        Debug.Log("start GetRankingTopScores-String rankingId, int timeDimension, int maxResults, long offsetPlayerRank");
        //HuaweiGameService.GetLeaderboardTopScores(_leaderboardID, 1, 10, 0, 0, _getLeaderboardScoresListener);
    }

    
    public void SubmitScore(string _leaderboardID, int score)
    {
        Debug.Log("start AsyncSubmitScore with ranking id: " + _leaderboardID + " score: " + score);
        //HuaweiGameService.AsyncSubmitScore(_leaderboardID, score, _submitScoreListener);
    }

    // Not used
    public void GetSpecificLeaderboard(string _leaderboardID)
    {
        Debug.Log("start GetRankingData");

        //HuaweiGameService.GetLeaderboardData(_leaderboardID, true, _getLeaderboardListener);
    }

    public void GetLeaderbaords()
    {
        Debug.Log("start GetRankingsData");

        //HuaweiGameService.GetLeaderboardsData(true, _getLeaderboardsListener);
    }

    #endregion

    #region Huawei services callbacks
    private void OnGetPlayerInfoSuccess(Player player)
    {
        Debug.Log($"GetPlayerInfo SUCCESS for {player.DisplayName}");
    }
    private void OnFailureListener(HMSException exception)
    {
        Debug.LogError("OnFailureListener ERROR:" + exception.Message);
    }
    #endregion

    #region On Destroy
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
