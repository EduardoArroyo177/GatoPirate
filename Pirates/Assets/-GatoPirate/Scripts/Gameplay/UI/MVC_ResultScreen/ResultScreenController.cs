using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using TMPro;
using UnityAtoms.BaseAtoms;

public class ResultScreenController : MonoBehaviour
{
    [SerializeField]
    private ResultScreenView resultScreenView;

    // TODO: Revive screen referente

    public CharacterTypeEvent ShowResultScreenEvent { get; set; }
    public BoolEvent WinChestEvent { get; set; }
    public VoidEvent LoadMainMenuSceneEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();
    private bool canWinChest;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CharacterType>.BuildEventHandler(ShowResultScreenEvent, ShowResultScreenEventCallback));
        _eventHandlers.Add(EventHandlerFactory<bool>.BuildEventHandler(WinChestEvent, WinChestEventCallback));
        resultScreenView.ResultScreenController = this;
    }

    #region Event callbacks
    private void ShowResultScreenEventCallback(CharacterType _winnerCharacterType)
    {
        // Winner information
        if (_winnerCharacterType.Equals(CharacterType.PLAYER))
        {
            LoadResultScreen();
        }
        else
        {
            LoadReviveScreen();
        }
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
    #endregion

    private void LoadResultScreen()
    {
        resultScreenView.gameObject.SetActive(true);

        // TODO: Save data in combat data

        // TODO: Set chest earned data here
        //if (combatWon && canWinChest)
        //{

        //}
    }

    private IEnumerator CurrencyCounterAnimation()
    {
        int start = 0;
        float timer = 0;
        int score;
        int totalAmount = CurrencyManager.Instance.GetCombatEarnedCoins();

        resultScreenView.Pnl_earnedCoins.SetActive(true);
        while (timer < resultScreenView.ResourcesAnimationDuration)
        {
            float progress = timer / resultScreenView.ResourcesAnimationDuration;
            score = (int)Mathf.Lerp(start, totalAmount, progress);
            resultScreenView.Lbl_earnedCoins.text = $"{score}";
            yield return null;
            timer += Time.deltaTime;
        }

        score = totalAmount;
        resultScreenView.Lbl_earnedCoins.text = $"x {score}";
        // TODO: Trigger animation or vfx for the total earned coins

        // TODO: Copy and paste previous block and update wood and gems if needed
        if (CurrencyManager.Instance.GetCombatEarnedWood() > 0)
        {
            // Play and show wood resource animation
        }
    }

    private void LoadReviveScreen()
    {

    }

    #region Public methods
    // This is trigger after a certain tween
    public void TriggerResultScreenAnimations()
    {
        StartCoroutine("CurrencyCounterAnimation");
    }

    public void WatchAdForDoubleReward()
    {
        Debug.Log("SHOW AD!");
        // TODO: Implement ad call
        // TODO: Implement currency animation by increasing to double
    }

    public void LoadMainMenuScene()
    {
        Debug.Log("LOADING MAIN MENU SCENE?");
        LoadMainMenuSceneEvent.Raise();
    }
    #endregion

}
