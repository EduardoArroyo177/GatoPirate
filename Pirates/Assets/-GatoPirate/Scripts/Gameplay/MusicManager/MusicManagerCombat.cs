using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityAtoms;
using UnityEngine;

public class MusicManagerCombat : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] combatMusicList;
    //[SerializeField]
    //private AudioClip[] combatNightMusicList;

    public VoidEvent TriggerCombatMusicEvent { get; set; }
    public FloatEvent SetMusicVolumeEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    private AudioSource audioSource;

    public void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerCombatMusicEvent, TriggerCombatMusicEventCallback));
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(SetMusicVolumeEvent, SetMusicVolumeEventCallback));
    }

    #region Event callbacks
    private void TriggerCombatMusicEventCallback(Void _item)
    {
        audioSource.Stop();
        audioSource.clip = combatMusicList[Random.Range(0, combatMusicList.Length)];
        audioSource.Play();
    }

    private void SetMusicVolumeEventCallback(float _newVolume)
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
