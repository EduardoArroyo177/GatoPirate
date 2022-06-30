using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScenes
{
    TestCombatScene,
    TestMainMenuScene
}

public class SceneLoaderManager : SceneSingleton<SceneLoaderManager>
{
    [SerializeField]
    private GameObject pnl_loading;

    public void LoadScene(GameScenes _sceneToLoad)
    {
        pnl_loading.SetActive(true);
        StartCoroutine(LoadSceneAsync(_sceneToLoad.ToString()));
    }

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
}
