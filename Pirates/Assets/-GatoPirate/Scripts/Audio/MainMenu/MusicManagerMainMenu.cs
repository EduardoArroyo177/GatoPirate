using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class MusicManagerMainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] islandMusicList;
    //[SerializeField]
    //private AudioClip[] nightMusicList;
    [SerializeField]
    private AudioClip storeMusic;

    public VoidEvent TriggerIslandMusicEvent { get; set; }
    public VoidEvent TriggerStoreMusicEvent { get; set; }
    public FloatEvent SetMusicVolumeEvent { get; set; }
    public VoidEvent UnloadEventsEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    private AudioSource audioSource;

    public void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerIslandMusicEvent, TriggerIslandMusicEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerStoreMusicEvent, TriggerStoreMusicEventCallback));
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(SetMusicVolumeEvent, SetMusicVolumeEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));
    }

    #region Event callbacks
    private void TriggerIslandMusicEventCallback(Void _item)
    {
        audioSource.Stop();
        audioSource.clip = islandMusicList[Random.Range(0, islandMusicList.Length)];
        audioSource.Play();
    }

    private void TriggerStoreMusicEventCallback(Void _item)
    {
        audioSource.Stop();
        audioSource.clip = storeMusic;
        audioSource.Play();
    }

    private void SetMusicVolumeEventCallback(float _newVolume)
    {
        audioSource.volume = _newVolume;
    }
    #endregion

    #region OnDestroy
    private void UnloadEventsEventCallback(Void _item)
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }

    //private void OnDestroy()
    //{
    //    foreach (var item in _eventHandlers)
    //    {
    //        item.UnregisterListener();
    //    }
    //    _eventHandlers.Clear();
    //}
    #endregion
}
