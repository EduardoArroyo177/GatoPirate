using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardScoreData
{
    public string playerDisplayRank;
    public string playerName;
    public long playerScore;
    public long playerRank;
}

public class LeaderboardsManager : MonoBehaviour
{
    [SerializeField]
    private LeaderboardListUserUI[] listUserUI;
    [SerializeField]
    private Transform userListContent;
    [SerializeField]
    private LeaderboardListUserUI currentPlayerListUserUI;

    private List<GameObject> listUsers = new List<GameObject>();

    private HuaweiGameServicesManager huaweiGameServicesManager;

    private void OnEnable()
    {
        if (!huaweiGameServicesManager)
        {
            huaweiGameServicesManager = GetComponent<HuaweiGameServicesManager>();
        }
#if !UNITY_EDITOR
        huaweiGameServicesManager.InitService();

        CleanList();

        if (!huaweiGameServicesManager.playerLogin)
            huaweiGameServicesManager.Login();
        else
            GetLeaderboards();
#endif
    }

    private void Awake()
    {
        //if(!ServiceLocator.LeaderboardManagerExists())
        //{
        //    ServiceLocator.leaderboardsManager = this;
        //}
    }

    private void GetLeaderboards()
    {
        huaweiGameServicesManager.GetCurrentPlayerScores();
        //huaweiGameServicesManager.GetLeaderboardScores();
    }

    public void SetCurrentPlayerLeaderboardScore(LeaderboardScoreData currentPlayerScores)
    {
        currentPlayerListUserUI.SetScoreData(currentPlayerScores.playerDisplayRank,
            currentPlayerScores.playerName, currentPlayerScores.playerScore.ToString());

        huaweiGameServicesManager.GetLeaderboardScores();
    }

    private void CleanList()
    {
        for (int i = 0; i < listUserUI.Length; i++)
        {
            listUserUI[i].gameObject.SetActive(false);
        }
    }


    public void SetLeaderboardScores(List<LeaderboardScoreData> leaderboardScoreDataList)
    {
        for (int i = 0; i < leaderboardScoreDataList.Count; i++)
        {
            Debug.Log($"{leaderboardScoreDataList[i].playerDisplayRank} {leaderboardScoreDataList[i].playerName}");
            listUserUI[i].SetScoreData(leaderboardScoreDataList[i].playerDisplayRank, leaderboardScoreDataList[i].playerName, leaderboardScoreDataList[i].playerScore.ToString());
            listUserUI[i].gameObject.SetActive(true);
        }
    }



    public void SubmitScore(int score)
    {
        huaweiGameServicesManager.SubmitScore(score);
    }
}
