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
    public CharacterTypeEvent ShowResultScreenEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CharacterType>.BuildEventHandler(ShowResultScreenEvent, ShowResultScreenEventCallback));
    }

    private void ShowResultScreenEventCallback(CharacterType _winnerCharacterType)
    {
        pnl_resultScreen.SetActive(true);
        lbl_winner.text = _winnerCharacterType.ToString();
    }
}
