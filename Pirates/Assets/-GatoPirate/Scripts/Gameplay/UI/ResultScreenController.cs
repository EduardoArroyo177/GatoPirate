using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using TMPro;
using UnityAtoms.BaseAtoms;

public class ResultScreenController : MonoBehaviour
{
    [SerializeField]
    private GameObject pnl_resultScreen;
    [SerializeField]
    private TextMeshProUGUI lbl_winner;
    [SerializeField]
    private TextMeshProUGUI lbl_earnedCoins;
    [SerializeField]
    private TextMeshProUGUI lbl_earnedWood;

    public CharacterTypeEvent ShowResultScreenEvent { get; set; }
    public BoolEvent WinChestEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();
    private bool combatWon;
    private bool canWinChest;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CharacterType>.BuildEventHandler(ShowResultScreenEvent, ShowResultScreenEventCallback));
        _eventHandlers.Add(EventHandlerFactory<bool>.BuildEventHandler(WinChestEvent, WinChestEventCallback));
    }

    private void ShowResultScreenEventCallback(CharacterType _winnerCharacterType)
    {
        // Winner information
        if (_winnerCharacterType.Equals(CharacterType.PLAYER))
        {
            combatWon = true;
            lbl_winner.text = "Ganaste!";
        }
        else
        {
            combatWon = false;
            lbl_winner.text = "Perdiste :(";
        }

        // Rewards information
        lbl_earnedCoins.text = $"x {CurrencyManager.Instance.GetCombatEarnedCoins()}";
        lbl_earnedWood.text = $"x {CurrencyManager.Instance.GetCombatEarnedWood()}";

        // TODO: Set chest won data here
        if (combatWon && canWinChest)
        {
            Debug.Log("GANó un CHEST!!!");
        }

        pnl_resultScreen.SetActive(true);
    }

    private void WinChestEventCallback(bool _canWinChest)
    {
        canWinChest = _canWinChest;
        if (_canWinChest)
        {
            // TODO: Set chest to earn data            
        }
        else
        { 
            // TODO: Probably don't do anything
        }
    }
}
