using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class CombatCatFacesController : MonoBehaviour
{
    [Header("Player cat faces")]
    [SerializeField]
    private CatFaceData catFaceDataSurprised;
    [SerializeField]
    private CatFaceData catFaceDataBrave;
    [SerializeField]
    private CatFaceData catFaceDataWorried;
    [SerializeField]
    private CatFaceData catFaceDataHappy;
    [SerializeField]
    private CatFaceData catFaceDataSad;
    [Header("Enemy cat faces")]
    [SerializeField]
    private CatFaceData catFaceDataDead;

    [Header("Body builder references")]
    [SerializeField]
    private CatBodyBuilder[] catBodyBuilderList;

    // Cinematic faces (player only)
    public VoidEvent UpdateToSurprisedFaceEvent { get; set; }
    public VoidEvent UpdateToBraveFaceEvent { get; set; }
    // Combat faces (player only)
    public VoidEvent UpdateToHappyFaceEvent { get; set; }
    // Faces for both enemies and player
    public VoidEvent UpdateToWorriedFaceEvent { get; set; }
    public VoidEvent UpdateToSadFaceEvent { get; set; }
    // Faces for enemy
    public VoidEvent UpdateToDeadFaceEvent { get; set; }
    public VoidEvent UnloadEventsEvent { get; set; }


    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        if(UpdateToSurprisedFaceEvent)
            _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UpdateToSurprisedFaceEvent, UpdateToSurprisedFaceEventCallback));
        if(UpdateToBraveFaceEvent)
            _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UpdateToBraveFaceEvent, UpdateToBraveFaceEventCallback));
        if (UpdateToHappyFaceEvent)
            _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UpdateToHappyFaceEvent, UpdateToHappyFaceEventCallback));
        if (UpdateToWorriedFaceEvent)
            _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UpdateToWorriedFaceEvent, UpdateToWorriedFaceEventCallback));
        if (UpdateToSadFaceEvent)
            _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UpdateToSadFaceEvent, UpdateToSadFaceEventCallback));
        if (UpdateToDeadFaceEvent)
            _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UpdateToDeadFaceEvent, UpdateToDeadFaceEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));
    }

    #region Event callbacks
    private void UpdateToSurprisedFaceEventCallback(Void _item)
    {
        for (int index = 0; index < catBodyBuilderList.Length; index++)
        {
            catBodyBuilderList[index].
                UpdateFaceSprite(catFaceDataSurprised.CatFaceSpriteList[Random.Range(0, catFaceDataSurprised.CatFaceSpriteList.Count)]);
        }
    }

    private void UpdateToBraveFaceEventCallback(Void _item)
    {
        for (int index = 0; index < catBodyBuilderList.Length; index++)
        {
            catBodyBuilderList[index].
                UpdateFaceSprite(catFaceDataBrave.CatFaceSpriteList[Random.Range(0, catFaceDataBrave.CatFaceSpriteList.Count)]);
        }
    }

    private void UpdateToHappyFaceEventCallback(Void _item)
    {
        for (int index = 0; index < catBodyBuilderList.Length; index++)
        {
            catBodyBuilderList[index].
                UpdateFaceSprite(catFaceDataHappy.CatFaceSpriteList[Random.Range(0, catFaceDataHappy.CatFaceSpriteList.Count)]);
        }
    }

    private void UpdateToWorriedFaceEventCallback(Void _item)
    {
        for (int index = 0; index < catBodyBuilderList.Length; index++)
        {
            catBodyBuilderList[index].
                UpdateFaceSprite(catFaceDataWorried.CatFaceSpriteList[Random.Range(0, catFaceDataWorried.CatFaceSpriteList.Count)]);
        }
    }

    private void UpdateToSadFaceEventCallback(Void _item)
    {
        for (int index = 0; index < catBodyBuilderList.Length; index++)
        {
            catBodyBuilderList[index].
                UpdateFaceSprite(catFaceDataSad.CatFaceSpriteList[Random.Range(0, catFaceDataSad.CatFaceSpriteList.Count)]);
        }
    }

    private void UpdateToDeadFaceEventCallback(Void _item)
    {
        for (int index = 0; index < catBodyBuilderList.Length; index++)
        {
            catBodyBuilderList[index].
                UpdateFaceSprite(catFaceDataDead.CatFaceSpriteList[Random.Range(0, catFaceDataDead.CatFaceSpriteList.Count)]);
        }
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
    #endregion
}
