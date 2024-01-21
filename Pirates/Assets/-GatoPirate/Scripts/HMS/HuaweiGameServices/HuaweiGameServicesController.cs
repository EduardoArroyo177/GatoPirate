using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HuaweiGameServicesManager;
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
    public LeaderboardDataEvent PlayerInitialRankDataEvent { get; set; }
    public LeaderboardDataEvent PlayerRankDataEvent { get; set; }
    public LeaderboardDataListEvent LeaderboardRankDataListEvent { get; set; }
    public StringEvent RequestPlayerScoreEvent { get; set; }
    public StringIntEvent SubmitHighScoreEvent { get; set; }
    public VoidEvent ScoreSubmittedEvent { get; set; }

    #endregion

    //private ILoginListener _loginListener;
    //private IGetLeaderboardScoreListener _getLeaderboardScoreListener; // Get player rank
    //private IGetLeaderboardScoresListener _getLeaderboardScoresListener; // Get rank list

    //private ISubmitScoreListener _submitScoreListener;
    //private IGetLeaderboardsListener _getLeaderboardsListener = new MyGetLeaderboardsListener();
    //private IGetLeaderboardListener _getLeaderboardListener = new MyGetLeaderboardListener();
    //private IGetPlayerListener _getPlayerListener = new MyGetPlayerListener();

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(PlayerLoginEvent, PlayerLoginEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(RequestPlayerScoreEvent, RequestPlayerScoreEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string,int>.BuildEventHandler(SubmitHighScoreEvent, SubmitHighScoreEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(RequestLeaderboardsDataEvent, RequestLeaderboardsDataEventCallback));

        
#if UNITY_ANDROID && !UNITY_EDITOR
        //InitService();
        //InitApp();
#endif
    }
    #region Initialization
    private void InitService()
    {
        Debug.Log("starting init");
        //HuaweiGameService.Init(new AntiAddictionListener(), new HInitListener());
        Debug.Log("init finished");
    }

    private void InitApp()
    {
        Debug.Log("starting appInit");
        //HuaweiGameService.AppInit();
        Debug.Log("appInit finished");
    }
    #endregion

    #region Event callbacks
    private void PlayerLoginEventCallback(Void _item)
    {
        Login();
    }

    private void RequestPlayerScoreEventCallback(string _leaderboardID)
    {
       // _getLeaderboardScoreListener = new MyGetLeaderboardScoreListener(PlayerInitialRankDataEvent);
        GetCurrentPlayerScores(_leaderboardID);
    }

    private void RequestLeaderboardsDataEventCallback(string _leaderboardID)
    {
        //_getLeaderboardScoreListener = new MyGetLeaderboardScoreListener(PlayerRankDataEvent);
        GetCurrentPlayerScores(_leaderboardID);
        //_getLeaderboardScoresListener = new MyGetLeaderboardScoresListener(LeaderboardRankDataListEvent);
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
        //Debug.Log("Loign");
        //    _loginListener = new MyLoginListener(LoginSuccessfulEvent);
        //    //AccountAuthParamsHelper authParamsHelper = new AccountAuthParamsHelper();
        //    //authParamsHelper.SetAuthorizationCode().SetAccessToken().SetIdToken().SetUid().SetId().SetEmail().CreateParams();
        //HuaweiGameService.SilentSignIn(_loginListener);

        //HuaweiGameService.Login(_loginListener);
    }

    public void GetCurrentPlayer()
    {
        Debug.Log("start getCurrentPlayer.");
        //HuaweiGameService.GetCurrentPlayer(true, _getPlayerListener);
    }
    #endregion

    #region Leaderboards
    public void GetCurrentPlayerScores(string _leaderboardID)
    {
        //HuaweiGameService.GetCurrentPlayerLeaderboardScore(_leaderboardID, 1, _getLeaderboardScoreListener);
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
