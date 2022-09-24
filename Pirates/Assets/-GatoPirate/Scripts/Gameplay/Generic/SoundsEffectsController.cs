using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class SoundsEffectsController : MonoBehaviour
{
    [Header("Combat sounds")]
    [SerializeField]
    private AudioClip specialCannonReady;
    [SerializeField]
    private AudioClip weakSpotActive;
    [SerializeField]
    private AudioClip weakSpotHit;

    [Header("Result screen sounds")]
    [SerializeField]
    private AudioClip resultScreenShown;
    [SerializeField]
    private AudioClip coinsRewardShown;
    [SerializeField]
    private AudioClip coinsRewardFinished;

    [Header("Revive screen sounds")]
    [SerializeField]
    private AudioClip reviveScreenShown;
    [SerializeField]
    private AudioClip reviveSuccess;

    public CombatSoundEvent TriggerCombatSoundEvent { get; set; }
    public FloatEvent SetSoundsVolumeEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();
    private AudioSource audioSource;

    public void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        _eventHandlers.Add(EventHandlerFactory<CombatSounds>.BuildEventHandler(TriggerCombatSoundEvent, TriggerCombatSoundEventCallback));
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(SetSoundsVolumeEvent, SetSoundsVolumeEventCallback));
    }

    #region Event callbacks
    private void SetSoundsVolumeEventCallback(float _volume)
    {
        audioSource.volume = _volume;
    }

    private void TriggerCombatSoundEventCallback(CombatSounds _soundType)
    {
        audioSource.PlayOneShot(GetAudioFromType(_soundType));
    }
    #endregion

    private AudioClip GetAudioFromType(CombatSounds _sound)
    {
        switch (_sound)
        {
            case CombatSounds.SPECIAL_CANNON_READY:
                return specialCannonReady;
            case CombatSounds.WEAK_SPOT_ACTIVE:
                return weakSpotActive;
            case CombatSounds.WEAK_SPOT_HIT:
                return weakSpotHit;
            case CombatSounds.RESULT_SCREEN_SHOWN:
                return resultScreenShown;
            case CombatSounds.COINS_REWARD_SHOWN:
                return coinsRewardShown;
            case CombatSounds.COINS_REWARD_FINISHED:
                return coinsRewardFinished;
            case CombatSounds.REVIVE_SCREEN_SHOWN:
                return reviveScreenShown;
            case CombatSounds.REVIVE_SUCCESS:
                return reviveSuccess;
            default:
                return specialCannonReady;
        }
    }


    #region On Destroy
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
