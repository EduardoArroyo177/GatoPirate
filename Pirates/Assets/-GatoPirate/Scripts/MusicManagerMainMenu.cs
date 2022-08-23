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

    private List<IAtomEventHandler> _eventHandlers = new();
    private AudioSource audioSource;

    public void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerIslandMusicEvent, TriggerIslandMusicEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerStoreMusicEvent, TriggerStoreMusicEventCallback));
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
