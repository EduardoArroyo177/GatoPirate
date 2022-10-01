using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HuaweiGameServicesManager;
using UnityEngine.HuaweiAppGallery.Listener;
using UnityEngine.HuaweiAppGallery;
using UnityAtoms.BaseAtoms;
using UnityAtoms;

public class HuaweiGameServicesController : MonoBehaviour
{
    #region Events
    // Login
    public VoidEvent PlayerLoginEvent { get; set; }
    public BoolEvent LoginSuccessfulEvent { get; set; }

    // Leaderboards
    public StringEvent RequestLeaderboardsDataEvent { get; set; }
    public LeaderboardDataEvent PlayerRankDataEvent { get; set; }
    public LeaderboardDataListEvent LeaderboardRankDataListEvent { get; set; }
    #endregion

    private ILoginListener _loginListener;
    private IGetLeaderboardScoreListener _getLeaderboardScoreListener; // Get player rank
    private IGetLeaderboardScoresListener _getLeaderboardScoresListener; // Get rank list

    private ISubmitScoreListener _submitScoreListener = new MySubmitScoreListener();
    private IGetLeaderboardsListener _getLeaderboardsListener = new MyGetLeaderboardsListener();
    private IGetLeaderboardListener _getLeaderboardListener = new MyGetLeaderboardListener();
    private IGetPlayerListener _getPlayerListener = new MyGetPlayerListener();

    private List<IAtomEventHandler> _eventHandlers = new();

    private bool playerLogin;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(PlayerLoginEvent, PlayerLoginEventCallback));
        _eventHandlers.Add(EventHandlerFactory<bool>.BuildEventHandler(LoginSuccessfulEvent, LoginSuccessfulEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(RequestLeaderboardsDataEvent, RequestLeaderboardsDataEventCallback));

        _getLeaderboardScoreListener = new MyGetLeaderboardScoreListener(PlayerRankDataEvent);
        _getLeaderboardScoresListener = new MyGetLeaderboardScoresListener(LeaderboardRankDataListEvent);
#if UNITY_ANDROID && !UNITY_EDITOR
        InitService();
        InitApp();
#endif
    }
    #region Initialization
    private void InitService()
    {
        Debug.Log("starting init");
        HuaweiGameService.Init();
        Debug.Log("init finished");
    }

    private void InitApp()
    {
        Debug.Log("starting appInit");
        HuaweiGameService.AppInit();
        Debug.Log("appInit finished");
    }
    #endregion

    #region Event callbacks
    private void PlayerLoginEventCallback(Void _item)
    {
        Login();
    }

    private void LoginSuccessfulEventCallback(bool _loginSuccesful)
    {
        playerLogin = _loginSuccesful;
        // If login is succesful, it will automatically login everytime
        LeaderboardsDataSaveManager.Instance.UpdateLoginStatus(_loginSuccesful);
        // If login failed (user canceled), it won't try to login automatically
        // Show a message to try again later

        if (LeaderboardsDataSaveManager.Instance.GetFirstTimeLoginStatus())
            LeaderboardsDataSaveManager.Instance.UpdateFirstTimeLoginStatus();
    }

    private void RequestLeaderboardsDataEventCallback(string _leaderboardID)
    {
        GetCurrentPlayerScores(_leaderboardID);
        GetSpecificLeaderboard(_leaderboardID);
    }
    #endregion


    #region Player login data
    private void Login()
    {
#if UNITY_EDITOR
        return;
#endif
        if (!playerLogin)
        {
            Debug.Log("starting login");
            _loginListener = new MyLoginListener(LoginSuccessfulEvent);
            AccountAuthParamsHelper authParamsHelper = new AccountAuthParamsHelper();
            authParamsHelper.SetAuthorizationCode().SetAccessToken().SetIdToken().SetUid().SetId().SetEmail().CreateParams();
            HuaweiGameService.Login(_loginListener);
        }
    }

    public void GetCurrentPlayer()
    {
        Debug.Log("start getCurrentPlayer.");
        HuaweiGameService.GetCurrentPlayer(true, _getPlayerListener);
    }
    #endregion

    #region Leaderboards
    public void GetCurrentPlayerScores(string _leaderboardID)
    {
        if (!playerLogin)
            return;
        HuaweiGameService.GetCurrentPlayerLeaderboardScore(_leaderboardID, 1, _getLeaderboardScoreListener);
    }

    public void GetLeaderboardScores(string _leaderboardID)
    {
        if (!playerLogin)
            return;
        Debug.Log("start GetRankingTopScores-String rankingId, int timeDimension, int maxResults, long offsetPlayerRank");
        HuaweiGameService.GetLeaderboardTopScores(_leaderboardID, 1, 10, 0, 0, _getLeaderboardScoresListener);
    }

    
    public void SubmitScore(string _leaderboardID, int score)
    {
        if (!playerLogin)
            return;
        Debug.Log("start AsyncSubmitScore with ranking id: " + _leaderboardID + " score: " + 2);
        HuaweiGameService.AsyncSubmitScore(_leaderboardID, score, _submitScoreListener);
    }

    // Not used
    public void GetSpecificLeaderboard(string _leaderboardID)
    {
        Debug.Log("start GetRankingData");

        HuaweiGameService.GetLeaderboardData(_leaderboardID, true, _getLeaderboardListener);
    }

    public void GetLeaderbaords()
    {
        Debug.Log("start GetRankingsData");

        HuaweiGameService.GetLeaderboardsData(true, _getLeaderboardsListener);
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
