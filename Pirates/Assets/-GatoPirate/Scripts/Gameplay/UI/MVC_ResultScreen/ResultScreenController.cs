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
    [SerializeField]
    private ReviveScreenView reviveScreenView;

    // Events
    public CharacterTypeEvent ShowResultScreenEvent { get; set; }
    public BoolEvent WinChestEvent { get; set; }
    public VoidEvent LoadMainMenuSceneEvent { get; set; }
    // Ad events
    public VoidEvent LoadReviveAdEvent { get; set; }
    public VoidEvent LoadDoubleRewardAdEvent { get; set; }
    public VoidEvent LoadCombatFinishedAdEvent { get; set; }
    public VoidEvent ReviveSuccessEvent { get; set; }
    public VoidEvent DoubleRewardSuccessEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();
    private bool canWinChest;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CharacterType>.BuildEventHandler(ShowResultScreenEvent, ShowResultScreenEventCallback));
        _eventHandlers.Add(EventHandlerFactory<bool>.BuildEventHandler(WinChestEvent, WinChestEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(ReviveSuccessEvent, ReviveSuccessEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(DoubleRewardSuccessEvent, DoubleRewardSuccessEventCallback));
        resultScreenView.ResultScreenController = this;
        reviveScreenView.ResultScreenController = this;
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

    private void ReviveSuccessEventCallback(Void _item)
    {
        // The callback should restart ship health
        // Unpause the game
        // Close reviveID screen
        reviveScreenView.gameObject.SetActive(false);
    }

    private void DoubleRewardSuccessEventCallback(Void _item)
    { 
        
    }
    #endregion

    private void LoadResultScreen()
    {
        resultScreenView.gameObject.SetActive(true);

        // TODO: Save data in combat data
        CurrencyDataSaveManager.Instance.UpdateEarnedCurrency(CurrencyType.GOLDEN_COINS, CurrencyManager.Instance.GetCombatEarnedCoins());
        // TODO: Uncomment this when wood is available in the game
        //CurrencyDataSaveManager.Instance.UpdateEarnedCurrency(CurrencyType.WOOD, CurrencyManager.Instance.GetCombatEarnedWood());
        
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
        resultScreenView.Lbl_earnedCoins.text = $"{score}";
        // TODO: Trigger animation or vfx for the total earned coins

        // TODO: Copy and paste previous block and update wood and gems if needed
        if (CurrencyManager.Instance.GetCombatEarnedWood() > 0)
        {
            // Play and show wood resource animation
        }

        // TODO: Trigger chest earned animation when previous is finished
    }

    private void LoadReviveScreen()
    {
        // TODO: Set coins price
        // TODO: Update button if there's no enough coins
        reviveScreenView.gameObject.SetActive(true);
    }

    #region Public methods
    // Result screen
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
        LoadMainMenuSceneEvent.Raise();
    }

    // Revive screen
    public void ReviveWithCoins()
    { 
        // TODO:
        // Reduce coins from data save
        // Restart ship health
        // Unpause game
        // Close reviveID screen
    }

    public void ReviveWithAd()
    { 
        // TODO:
        // Trigger an ad
        LoadReviveAdEvent.Raise();
    }
    #endregion

    #region OnDestroy
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
