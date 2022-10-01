using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardsDataSaveManager : SceneSingleton<LeaderboardsDataSaveManager>
{
    [SerializeField]
    private DataSaveLeaderboardListStructure leaderboardsData;

    public const string LEADERBOARDS_SAVING_DATA_KEY = "LEADERBOARDS_SCORE";

    public DataSaveLeaderboardListStructure LeaderboardsData { get => leaderboardsData; set => leaderboardsData = value; }

    #region Initialization
    public void LoadLeaderboardsSavedData()
    {
        string dataSave = PlayerPrefs.GetString(LEADERBOARDS_SAVING_DATA_KEY);
        if (string.IsNullOrEmpty(dataSave))
        {
            LeaderboardsData = new DataSaveLeaderboardListStructure();
            // No login attempt yet
            LeaderboardsData.FirstTimeLoginAttempted = true;
            LeaderboardsData.IsLoginAccepted = false;
            // Combats won leaderboard
            DataSaveLeaderboardStructure initialLeaderboardsStructure = new DataSaveLeaderboardStructure();
            initialLeaderboardsStructure.LeaderboardType = LeaderboardType.COMBATS_WON.ToString();
            initialLeaderboardsStructure.PendingCombatScore = 0;
            LeaderboardsData.LeaderboardsList.Add(initialLeaderboardsStructure);

            SaveLeaderboardsData();
        }
        else
        {
            // Load saved data
            LeaderboardsData = JsonUtility.FromJson<DataSaveLeaderboardListStructure>(dataSave);
        }
    }
    #endregion

    #region Update data
    public void UpdateFirstTimeLoginStatus()
    {
        LeaderboardsData.FirstTimeLoginAttempted = false;
        SaveLeaderboardsData();
    }

    public bool GetFirstTimeLoginStatus()
    {
        return LeaderboardsData.FirstTimeLoginAttempted;
    }

    public void UpdateLoginStatus(bool _loginSuccesful)
    {
        LeaderboardsData.IsLoginAccepted = true;
        SaveLeaderboardsData();
    }

    public bool GetLoginStatus()
    {
        return LeaderboardsData.IsLoginAccepted;
    }

    public int GetLeaderboardScore(LeaderboardType _leaderboardType)
    {
        int index = LeaderboardsData.LeaderboardsList.FindIndex(x => x.LeaderboardType.Equals(_leaderboardType.ToString()));

        if (index < 0)
        {
            Debug.LogError($"An error occured while retrieving leaderboard data: {_leaderboardType}");
            return 0;
        }

        return LeaderboardsData.LeaderboardsList[index].PendingCombatScore;
    }

    public void UpdateLeaderboardScore(LeaderboardType _leaderboardType, int _newScore)
    {
        int index = LeaderboardsData.LeaderboardsList.FindIndex(x => x.LeaderboardType.Equals(_leaderboardType.ToString()));

        if (index < 0)
        {
            Debug.LogError($"An error occured while retrieving leaderboard data: {_leaderboardType}");
            return;
        }

        LeaderboardsData.LeaderboardsList[index].PendingCombatScore = _newScore;
        SaveLeaderboardsData();
    }
    #endregion

    #region Save data
    private void SaveLeaderboardsData()
    {
        PlayerPrefs.SetString(LEADERBOARDS_SAVING_DATA_KEY, JsonUtility.ToJson(LeaderboardsData));
    }
    #endregion
}
