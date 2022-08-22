using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: Update with final game scenes name
public enum GameScenes
{
    Combat,
    MainMenu
}

public class SceneLoaderManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pnl_loading;
    [SerializeField]
    private GameScenes sceneToLoad;

    public VoidEvent LoadCombatSceneEvent { get; set; }
    public VoidEvent LoadMainMenuSceneEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        if(LoadCombatSceneEvent)
            _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadCombatSceneEvent, LoadCombatSceneEventCallback));
        if(LoadMainMenuSceneEvent)
            _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadMainMenuSceneEvent, LoadMainMenuSceneEventCallback));
    }

    #region Event callbacks
    private void LoadCombatSceneEventCallback(Void _item)
    {
        pnl_loading.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneToLoad.ToString()));
    }

    private void LoadMainMenuSceneEventCallback(Void _item)
    {
        pnl_loading.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneToLoad.ToString()));
    }
    #endregion

    IEnumerator LoadSceneAsync(string _sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
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
