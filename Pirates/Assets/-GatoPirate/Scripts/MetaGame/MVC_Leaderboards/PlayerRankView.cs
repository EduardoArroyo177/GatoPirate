using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRankView : MonoBehaviour
{
    [SerializeField]
    private Image img_rankIcon;
    [SerializeField]
    private TextMeshProUGUI lbl_rank;
    [SerializeField]
    private TextMeshProUGUI lbl_score;
    [SerializeField]
    private TextMeshProUGUI lbl_playerName;

    [Header("First places")]
    [SerializeField]
    private GameObject img_firstPlaceAnim;

    public void SetRankIcon(Sprite _rankIconSprite)
    {
        img_rankIcon.sprite = _rankIconSprite;
    }

    public void SetPlayerRankData(string _rank, string _score, string _playerName)
    {
        
        lbl_rank.text = _rank;
        lbl_score.text = _score;    
        lbl_playerName.text = _playerName;
    }

    public void SetAsFirstPlaces()
    {
        img_firstPlaceAnim.SetActive(true);
    }
}
