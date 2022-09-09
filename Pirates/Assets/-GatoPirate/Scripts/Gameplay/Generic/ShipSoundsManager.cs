using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class ShipSoundsManager : MonoBehaviour
{
    [Header("Cannon sounds")]
    [SerializeField]
    private AudioClip automaticCannonShot;
    [SerializeField]
    private AudioClip automaticCannonHit;
    [SerializeField]
    private AudioClip basicCannonShot;
    [SerializeField]
    private AudioClip basicCannonHit;
    [SerializeField]
    private AudioClip normalCannonShot;
    [SerializeField]
    private AudioClip normalCannonHit;
    [SerializeField]
    private AudioClip specialCannonShot;
    [SerializeField]
    private AudioClip specialCannonHit;

    public CombatShipSoundEvent TriggerShipSoundEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    private AudioSource audioSource;

    public void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        _eventHandlers.Add(EventHandlerFactory<CombatShipSounds>.BuildEventHandler(TriggerShipSoundEvent, TriggerShipSoundEventCallback));
    }

    #region Event callbacks
    private void TriggerShipSoundEventCallback(CombatShipSounds _soundType)
    {
        audioSource.PlayOneShot(GetAudioFromType(_soundType));
    }
    #endregion

    private AudioClip GetAudioFromType(CombatShipSounds _sound)
    {
        switch (_sound)
        {
            case CombatShipSounds.AUTOMATIC_CANNON_SHOT:
                return automaticCannonShot;
            case CombatShipSounds.AUTOMATIC_CANNON_HIT:
                return automaticCannonHit;
            case CombatShipSounds.BASIC_CANNON_SHOT:
                return basicCannonShot;
            case CombatShipSounds.BASIC_CANNON_HIT:
                return basicCannonHit;
            case CombatShipSounds.NORMAL_CANNON_SHOT:
                return normalCannonShot;
            case CombatShipSounds.NORMAL_CANNON_HIT:
                return normalCannonHit;
            case CombatShipSounds.SPECIAL_CANNON_SHOT:
                return specialCannonShot;
            case CombatShipSounds.SPECIAL_CANNON_HIT:
                return specialCannonHit;
            default:
                return automaticCannonShot;
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
