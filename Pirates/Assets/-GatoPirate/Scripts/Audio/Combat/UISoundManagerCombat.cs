using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class UISoundManagerCombat : MonoBehaviour
{
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

    // Events
    public VoidEvent UISoundScreenOpenEvent { get; set; }
    public VoidEvent UISoundScreenClosedEvent { get; set; }
    public VoidEvent UISoundButtonPressedEvent { get; set; }
    public VoidEvent UISoundButtonCancelEvent { get; set; }
    public VoidEvent UISoundTapEvent { get; set; }
    public FloatEvent SetSoundsVolumeEvent { get; set; }

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
