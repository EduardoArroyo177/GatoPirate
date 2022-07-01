using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using TMPro;

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

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CharacterType>.BuildEventHandler(ShowResultScreenEvent, ShowResultScreenEventCallback));
    }

    private void ShowResultScreenEventCallback(CharacterType _winnerCharacterType)
    {
        // Winner information
        if (_winnerCharacterType.Equals(CharacterType.PLAYER))
            lbl_winner.text = "Ganaste!";
        else
            lbl_winner.text = "Perdiste :(";

        // Rewards information
        lbl_earnedCoins.text = $"x {CurrencyManager.Instance.GetCombatEarnedCoins()}";
        lbl_earnedWood.text = $"x {CurrencyManager.Instance.GetCombatEarnedWood()}";

        pnl_resultScreen.SetActive(true);

        
    }
}
