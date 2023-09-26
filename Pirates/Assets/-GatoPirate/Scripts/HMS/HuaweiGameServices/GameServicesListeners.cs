using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.HuaweiAppGallery.Listener;
using UnityEngine.HuaweiAppGallery.Model;

public class MyLoginListener : ILoginListener
{
    private BoolEvent LoginSuccessfulEvent;

    public MyLoginListener(BoolEvent loginSuccessfulEvent = null)
    {
        LoginSuccessfulEvent = loginSuccessfulEvent;
    }

    public void OnSuccess(SignInAccountProxy signInAccountProxy)
    {
        if (signInAccountProxy == null)
        {
            Debug.Log("signInAccountProxy == null");
            return;
        }

        string msg = "get login success with signInAccountProxy info: \n";
        msg += String.Format("displayName:{0}, email:{1}, uid:{2}, openId:{3}, unionId:{4}, accessToken:{5}, serverAuthCode:{6}, idToken:{7}",
        signInAccountProxy.DisplayName, signInAccountProxy.Email, signInAccountProxy.Uid, signInAccountProxy.OpenId, signInAccountProxy.UnionId,
        signInAccountProxy.AccessToken, signInAccountProxy.ServerAuthCode, signInAccountProxy.IdToken);
        Debug.Log(msg);

        LoginSuccessfulEvent?.Raise(true);
    }

    public void OnSignOut()
    {
        string msg = "sign out success.";
        Debug.Log(msg);
    }

    public void OnFailure(int code, string message)
    {
        string msg = "account method failed, code:" + code + " message:" + message;
        Debug.Log(msg);
        LoginSuccessfulEvent?.Raise(false);
    }
}

#region Init
public class MyInitExitListener : IAntiAddictionListener
{
    public void OnExit()
    {
        Debug.Log("Exit correctly");
    }
}

public class MyInitListener : IInitListener
{
    public void OnFailure(int code, string message)
    {
        Debug.Log($"An error has occurred code: {code} message: {message}");
    }

    public void OnSuccess()
    {
        Debug.Log("Init successful");
    }
}
#endregion

#region Player
// Player
public class MyGetPlayerListener : IGetPlayerListener
{
    public void OnSuccess(Player player)
    {
        if (player == null)
        {
            Debug.Log("player == null");
            return;
        }
        var msg = "getCurrentPlayer succeed. \n";
        msg += string.Format(
            "displayName:{0}, playerId:{1}, signTimestamp:{2}, playerSign:{3}, level:{4}, openId:{5}, unionId:{6}",
            player.DisplayName, player.PlayerId, player.SignTimestamp, player.PlayerSign, player.Level, player.OpenId, player.UnionId
        );
        Debug.Log(msg);
        //playerId = player.PlayerId;
    }

    public void OnFailure(int code, string message)
    {
        var msg = "getCurrentPlayer failed, code:" + code + " message:" + message;
        Debug.Log(msg);
    }
}
#endregion

#region Leaderboards
public class MySubmitScoreListener : ISubmitScoreListener
{
    private VoidEvent ScoreSubmittedEvent;
    public MySubmitScoreListener(VoidEvent _scoreSubmittedEvent = null)
    {
        ScoreSubmittedEvent = _scoreSubmittedEvent;
    }
    public void OnSuccess(ScoreSubmission scoreSubmission)
    {
        if (scoreSubmission == null)
        {
            Debug.Log("socreSubmission == null");
            return;
        }
        string msg = "success submitted.";
        msg += string.Format("leaderboard id:{0}, playerId:{1}, scoreResults: \n", scoreSubmission.LeaderboardId,
            scoreSubmission.PlayerId);
        
        foreach (KeyValuePair<int, ScoreSubmission.Result> r in scoreSubmission.ScoreResults)
        {
            msg += string.Format("key: {0}, rawScore:{1}, formattedScore:{2}, scoreTag:{3}, isBest:{4}; \n", r.Key,
                r.Value.RawScore, r.Value.FormattedScore, r.Value.ScoreTag, r.Value.IsBest);
        }

        Debug.Log(msg);
        ScoreSubmittedEvent?.Raise();
    }

    public void OnFailure(int code, string message)
    {
        var msg = "subscore failed, code:" + code + " message:" + message;
        Debug.Log(msg);
    }
}

public class MyGetLeaderboardsListener : IGetLeaderboardsListener
{
    public void OnSuccess(List<LeaderboardProxy> leaderboards)
    {
        if (leaderboards == null)
        {
            Debug.Log("leaderboards == null");
            return;
        }
        var msg = "get leader board data succeed with count: " + leaderboards.Count + "\n";
        foreach (var l in leaderboards)
        {
            msg += string.Format("leaderBoardId: {0}, display name:{1}, score order:{2} \n", l.LeaderboardId,
                l.LeaderboardDisplayName, l.LeaderboardScoreOrder);

        }

        Debug.Log(msg);
    }

    public void OnFailure(int code, string message)
    {
        var msg = "get leader board data failed, code:" + code + " message:" + message;
        Debug.Log(msg);
    }
}

public class MyGetLeaderboardListener : IGetLeaderboardListener
{
    public void OnSuccess(LeaderboardProxy leaderboardProxy)
    {
        if (leaderboardProxy == null)
        {
            Debug.Log("leaderboard == null");
            return;
        }
        var msg = "get leader board data succeed. \n";
        msg += string.Format("leaderboard Id: {0}, display name: {1}, score order:{2}",
            leaderboardProxy.LeaderboardId, leaderboardProxy.LeaderboardDisplayName,
            leaderboardProxy.LeaderboardScoreOrder);
        Debug.Log(msg);
    }

    public void OnFailure(int code, string message)
    {
        var msg = "get leader board data failed, code:" + code + " message:" + message;
        Debug.Log(msg);
    }
}

public class MyGetLeaderboardScoresListener : IGetLeaderboardScoresListener
{
    private LeaderboardDataListEvent LeaderboardRankDataListEvent;

    public MyGetLeaderboardScoresListener(LeaderboardDataListEvent _leaderboardRankDataListEvent = null)
    {
        LeaderboardRankDataListEvent = _leaderboardRankDataListEvent;
    }

    public void OnSuccess(LeaderboardScores leaderboardScores)
    {
        if (leaderboardScores == null)
        {
            Debug.Log("get succeed, but leaderboardsData == null");
            return;
        }
        List<LeaderboardScoreData> leaderboardScoreDataList = new List<LeaderboardScoreData>();
        LeaderboardScoreData leaderboardScoreDataHelper;
        
        var msg = "get succeed. \n";
        msg += string.Format("leaderboard id: {0}, display name:{1} \n",
            leaderboardScores.LeaderboardProxy.LeaderboardId,
            leaderboardScores.LeaderboardProxy.LeaderboardDisplayName);

        foreach (var score in leaderboardScores.LeaderboardScoreList)
        {
            msg += string.Format("rank:{0}, score:{1}, timespan:{2}, player rank:{3}, scoreTag:{4}, player name: {5}, \n",
                score.DisplayRank,
                score.PlayerRawScore, score.TimeSpan, score.PlayerRank, score.ScoreTag,
                score.ScoreOwnerPlayer.DisplayName);

            leaderboardScoreDataHelper = new LeaderboardScoreData();
            leaderboardScoreDataHelper.playerDisplayRank = score.DisplayRank;
            leaderboardScoreDataHelper.playerName = score.ScoreOwnerPlayer.DisplayName;
            leaderboardScoreDataHelper.playerScore = score.PlayerRawScore;
            leaderboardScoreDataHelper.playerRank = score.PlayerRank;
            leaderboardScoreDataList.Add(leaderboardScoreDataHelper);
        }
        Debug.Log(msg);
        LeaderboardRankDataListEvent?.Raise(leaderboardScoreDataList);
    }

    public void OnFailure(int code, string message)
    {
        var msg = "get leaderboard scores failed, code:" + code + " message:" + message;
        Debug.Log(msg);
    }
}

public class MyGetLeaderboardScoreListener : IGetLeaderboardScoreListener
{
    private LeaderboardDataEvent PlayerRankDataEvent;

    public MyGetLeaderboardScoreListener(LeaderboardDataEvent _playerRankDataEvent = null)
    {
        PlayerRankDataEvent = _playerRankDataEvent;
    }

    public void OnSuccess(LeaderboardScore score)
    {
        if (score == null)
        {
            Debug.Log("leaderboardScore == null");
            return;
        }
        var msg = "get currentplayer leaderboard succeed. \n";
        msg += string.Format("rank:{0}, score:{1}, timespan:{2}, player rank:{3}, scoreTag:{4}, \n",
            score.DisplayRank,
            score.PlayerRawScore, score.TimeSpan, score.PlayerRank, score.ScoreTag);

        LeaderboardScoreData currentPlayerScores = new LeaderboardScoreData();
        currentPlayerScores.playerDisplayRank = score.DisplayRank;
        currentPlayerScores.playerName = score.ScoreOwnerPlayer.DisplayName;
        currentPlayerScores.playerRank = score.PlayerRank;
        currentPlayerScores.playerScore = score.PlayerRawScore;

        Debug.Log(msg);
        PlayerRankDataEvent?.Raise(currentPlayerScores);
    }

    public void OnFailure(int code, string message)
    {
        var msg = "get currentplayer leaderboard score failed, code:" + code + " message:" + message;
        Debug.Log(msg);
    }
}

#endregion