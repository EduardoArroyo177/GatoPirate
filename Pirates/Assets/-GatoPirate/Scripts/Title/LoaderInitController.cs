using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class LoaderInitController : MonoBehaviour
{
    [Header("Tutorial data")]
    [SerializeField]
    private CombatData combatData;
    [SerializeField]
    private EnemyShipData enemyTutorialShipData;
    [SerializeField]
    private PlayerShipData playerTutorialShipData;

    private void Awake()
    {
        //UnityEngine.Screen.SetResolution(1920, 1080, true);
        TutorialDataSaveManager.Instance.LoadTutorialSavedData();
    }

    // Called from title animation 
    public void LoadGame()
    {
        Debug.Log("Loading game");
        if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.FIRST_COMBAT))
        {
            // Fill combat data with tutorial data
            combatData.EnemyShipData = enemyTutorialShipData;
            combatData.PlayerShipData = playerTutorialShipData;
            // Load combat scene
            StartCoroutine(LoadSceneAsync((int)GameScenes.Combat));
        }
        else
        {
            // Load island scene
            StartCoroutine(LoadSceneAsync((int)GameScenes.MainMenu));
        }
        //GameAnalytics.Initialize();
    }

    IEnumerator LoadSceneAsync(int _scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }
}
