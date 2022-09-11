using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityAtoms;
using UnityEngine;

public class SoundsManagerMainMenu : MonoBehaviour
{
    [Header("UI Audio")]
    [SerializeField]
    private AudioClip sound_ScreenOpen;
    [SerializeField]
    private AudioClip sound_ScreenClosed;
    [SerializeField]
    private AudioClip sound_ButtonPressed;
    [SerializeField]
    private AudioClip sound_ButtonCancel;
    [SerializeField]
    private AudioClip sound_Tap;

    [Header("Cat sounds")]
    [SerializeField]
    private AudioClip sound_CatMeow1;
    [SerializeField]
    private AudioClip sound_CatMeow2;
    [SerializeField]
    private AudioClip sound_CatMeow3;

    [Header("Ship sounds")]
    [SerializeField]
    private AudioClip sound_ShipSelected;

    public VoidEvent UISoundScreenOpenEvent { get; set; }
    public VoidEvent UISoundScreenClosedEvent { get; set; }
    public VoidEvent UISoundButtonPressedEvent { get; set; }
    public VoidEvent UISoundButtonCancelEvent { get; set; }
    public VoidEvent UISoundTapEvent { get; set; }
    public FloatEvent SetSoundsVolumeEvent { get; set; }

    public CatSoundEvent TriggerCatSoundEvent { get; set; }
    public ShipSoundEvent TriggerShipSoundEvent { get; set; }


    private List<IAtomEventHandler> _eventHandlers = new();
    private AudioSource audioSource;

    public void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UISoundScreenOpenEvent, UISoundScreenOpenEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UISoundScreenClosedEvent, UISoundScreenClosedEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UISoundButtonPressedEvent, UISoundButtonPressedEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UISoundButtonCancelEvent, UISoundButtonCancelEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UISoundTapEvent, UISoundTapEventCallback));
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(SetSoundsVolumeEvent, SetSoundsVolumeEventCallback));
        _eventHandlers.Add(EventHandlerFactory<CatMeowSounds>.BuildEventHandler(TriggerCatSoundEvent, TriggerCatSoundEventCallback));
        _eventHandlers.Add(EventHandlerFactory<ShipSounds>.BuildEventHandler(TriggerShipSoundEvent, TriggerShipSoundEventCallback));

    }

    #region Event callbacks
    private void UISoundScreenOpenEventCallback(Void _item)
    {
        audioSource.Stop();
        audioSource.clip = sound_ScreenOpen;
        audioSource.Play();
    }

    private void UISoundScreenClosedEventCallback(Void _item)
    {
        audioSource.Stop();
        audioSource.clip = sound_ScreenClosed;
        audioSource.Play();
    }

    private void UISoundButtonPressedEventCallback(Void _item)
    {
        audioSource.Stop();
        audioSource.clip = sound_ButtonPressed;
        audioSource.Play();
    }

    private void UISoundButtonCancelEventCallback(Void _item)
    {
        audioSource.Stop();
        audioSource.clip = sound_ButtonCancel;
        audioSource.Play();
    }

    private void UISoundTapEventCallback(Void _item)
    {
        audioSource.Stop();
        audioSource.clip = sound_Tap;
        audioSource.Play();
    }

    private void SetSoundsVolumeEventCallback(float _newVolume)
    {
        audioSource.volume = _newVolume;
    }

    private void TriggerCatSoundEventCallback(CatMeowSounds _catSound)
    {
        audioSource.PlayOneShot(GetCatSound(_catSound));
    }

    private void TriggerShipSoundEventCallback(ShipSounds _shipSound)
    {
        audioSource.PlayOneShot(GetShipSound(_shipSound));
    }
    #endregion

    private AudioClip GetCatSound(CatMeowSounds _catSound)
    {
        switch (_catSound)
        {
            case CatMeowSounds.SELECTED_CAT1:
                return sound_CatMeow1;
            case CatMeowSounds.SELECTED_CAT2:
                return sound_CatMeow2;
            case CatMeowSounds.SELECTED_CAT3:
                return sound_CatMeow3;
            default:
                return sound_CatMeow1;
        }
    }

    private AudioClip GetShipSound(ShipSounds _shipSound)
    {
        switch (_shipSound)
        {
            case ShipSounds.SHIP_SELECTED:
                return sound_ShipSelected;
            default:
                return sound_ShipSelected;
        }
    }

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
