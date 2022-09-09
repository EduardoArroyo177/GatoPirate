using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class SoundsEffectsController : MonoBehaviour
{
    [SerializeField]
    private AudioClip specialCannonReady;

    public CombatSoundEvent TriggerCombatSoundEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();
    private AudioSource audioSource;

    public void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        _eventHandlers.Add(EventHandlerFactory<CombatSounds>.BuildEventHandler(TriggerCombatSoundEvent, TriggerCombatSoundEventCallback));
    }

    #region Event callbacks
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
            //case CombatSounds.WEAK_SPOT_SHOWN:
            //    return automaticCannonHit;
            //case CombatSounds.WEAK_SPOT_HIT:
            //    return basicCannonShot;
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
