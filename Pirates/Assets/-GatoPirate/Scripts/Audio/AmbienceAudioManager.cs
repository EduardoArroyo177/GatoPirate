using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class AmbienceAudioManager : MonoBehaviour
{
    [SerializeField]
    private float volumeDefaultValue;
    public FloatEvent SetSoundsVolumeEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    private AudioSource audioSource;

    public void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(SetSoundsVolumeEvent, SetSoundsVolumeEventCallback));
    }

    #region Event callbacks
    private void SetSoundsVolumeEventCallback(float _volume)
    {
        // Ambience level 1 volume is 0.3
        audioSource.volume = _volume * volumeDefaultValue;
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
