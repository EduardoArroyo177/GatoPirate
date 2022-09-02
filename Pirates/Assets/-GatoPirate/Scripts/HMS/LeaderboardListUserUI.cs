using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardListUserUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI rankTxt;
    [SerializeField]
    private TextMeshProUGUI userNameTxt;
    [SerializeField]
    private TextMeshProUGUI scoreTxt;

    public void SetScoreData(string rank, string playerName, string score)
    {
        rankTxt.text = rank;
        userNameTxt.text = playerName;
        scoreTxt.text = score;
    }
}
