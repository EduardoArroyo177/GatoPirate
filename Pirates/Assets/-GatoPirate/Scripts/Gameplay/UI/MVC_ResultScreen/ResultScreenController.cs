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
    public VoidEvent CurrenciesUpdatedEvent { get; set; }
    // Ad events
    public VoidEvent LoadReviveAdEvent { get; set; }
    public VoidEvent LoadDoubleRewardAdEvent { get; set; }
    public VoidEvent LoadCombatFinishedAdEvent { get; set; }
    public VoidEvent ReviveSuccessEvent { get; set; }
    public VoidEvent DoubleRewardSuccessEvent { get; set; }
    // Audio events
    public CombatSoundEvent TriggerCombatSoundEvent { get; set; }
    // IAP events
    public VoidEvent RemoveAdsPurchasedEvent { get; set; }

    // Properties
    public int ReviveCurrencyPrice { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();
    private bool canWinChest;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CharacterType>.BuildEventHandler(ShowResultScreenEvent, ShowResultScreenEventCallback));
        _eventHandlers.Add(EventHandlerFactory<bool>.BuildEventHandler(WinChestEvent, WinChestEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(ReviveSuccessEvent, ReviveSuccessEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(DoubleRewardSuccessEvent, DoubleRewardSuccessEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(RemoveAdsPurchasedEvent, RemoveAdsPurchasedEventCallback));
        resultScreenView.ResultScreenController = this;
        reviveScreenView.ResultScreenController = this;
        reviveScreenView.PanelCurrenciesController.CurrenciesUpdatedEvent = CurrenciesUpdatedEvent;
        reviveScreenView.PanelCurrenciesController.Initialize();
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
        // Close reviveID screen
        reviveScreenView.gameObject.SetActive(false);
        // Trigger success sound
        TriggerCombatSoundEvent.Raise(CombatSounds.REVIVE_SUCCESS);
    }

    private void DoubleRewardSuccessEventCallback(Void _item)
    {
        // TODO: Update all rewards here (wood and gems/chests)
        int startingRewardAmnt = CurrencyManager.Instance.GetCombatEarnedCoins();
        // Save double reward
        CurrencyDataSaveManager.Instance.UpdateEarnedCurrency(CurrencyType.GOLDEN_COINS, CurrencyManager.Instance.GetCombatEarnedCoins());
        int totalAmnt = CurrencyDataSaveManager.Instance.GetEarnedCurrencyAmount(CurrencyType.GOLDEN_COINS);
        // Play animation
        StartCoroutine(CurrencyCounterAnimation(startingRewardAmnt, totalAmnt));
        // Hide watch ad button
        resultScreenView.Btn_watchAd.SetActive(false);
        resultScreenView.Btn_freeReward.SetActive(false);
    }

    private void RemoveAdsPurchasedEventCallback(Void _item)
    {
        resultScreenView.RemoveAdsPurchased();
        reviveScreenView.RemoveAdsPurchased();
    }
    #endregion

    private void LoadResultScreen()
    {

        resultScreenView.gameObject.SetActive(true);
        // Trigger result screen shown sound
        TriggerCombatSoundEvent.Raise(CombatSounds.RESULT_SCREEN_SHOWN);

        // Save data in combat data
        CurrencyDataSaveManager.Instance.UpdateEarnedCurrency(CurrencyType.GOLDEN_COINS, CurrencyManager.Instance.GetCombatEarnedCoins());
        // TODO: Uncomment this when wood is available in the game
        //CurrencyDataSaveManager.Instance.UpdateEarnedCurrency(CurrencyType.WOOD, CurrencyManager.Instance.GetCombatEarnedWood());

        // TODO: Set chest earned data here
        //if (combatWon && canWinChest)
        //{

        //}

        if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.FIRST_COMBAT))
            TutorialDataSaveManager.Instance.UpdateTutorialCompleted(TutorialType.FIRST_COMBAT);

        LeaderboardsDataSaveManager.Instance.UpdateLeaderboardScore(LeaderboardType.COMBATS_WON, 1);
    }

    private IEnumerator CurrencyCounterAnimation(int _startingAmnt, int _totalAmnt = 0)
    {
        int start = _startingAmnt;
        float timer = 0;
        int score;
        int totalAmount;
        if (_totalAmnt == 0)
            totalAmount = CurrencyManager.Instance.GetCombatEarnedCoins();
        else
            totalAmount = _totalAmnt;
        resultScreenView.Pnl_earnedCoins.SetActive(true);
        // Trigger starting reward sound
        TriggerCombatSoundEvent.Raise(CombatSounds.COINS_REWARD_SHOWN);
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
        // Trigger finished sound
        TriggerCombatSoundEvent.Raise(CombatSounds.COINS_REWARD_FINISHED);

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
        // TODO: Update this with the correct currency
        // Set coins price
        reviveScreenView.SetRevivePrice(ReviveCurrencyPrice);
        // Update button if there's no enough coins
        int currentCoins = CurrencyDataSaveManager.Instance.GetCurrencyAmount(CurrencyType.GOLDEN_COINS);
        if (currentCoins < ReviveCurrencyPrice)
            reviveScreenView.SetCoinsButtonAsLocked();
        // Show revive screen
        CurrenciesUpdatedEvent.Raise();
        reviveScreenView.gameObject.SetActive(true);
        // Trigger revive screen shown sound
        TriggerCombatSoundEvent.Raise(CombatSounds.REVIVE_SCREEN_SHOWN);
    }

    #region Public methods
    // Result screen
    // This is trigger after a certain tween
    public void TriggerResultScreenAnimations()
    {
        StartCoroutine(CurrencyCounterAnimation(0));
    }

    public void WatchAdForDoubleReward()
    {
        LoadDoubleRewardAdEvent.Raise();
    }

    public void LoadMainMenuScene()
    {
        LoadMainMenuSceneEvent.Raise();
    }

    // Revive screen
    public void ReviveWithCoins()
    {
        // Reduce coins from data save
        CurrencyDataSaveManager.Instance.UpdateCurrency(CurrencyType.GOLDEN_COINS, -ReviveCurrencyPrice);
        ReviveSuccessEvent.Raise();
    }

    public void ReviveWithAd()
    { 
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
