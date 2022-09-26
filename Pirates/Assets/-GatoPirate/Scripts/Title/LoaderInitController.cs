using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        TutorialDataSaveManager.Instance.LoadTutorialSavedData();
    }

    public void LoadGame()
    {
        if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.FIRST_COMBAT))
        {
            // Fill combat data with tutorial data
            combatData.EnemyShipData = enemyTutorialShipData;
            combatData.PlayerShipData = playerTutorialShipData;
            // Load combat scene
            StartCoroutine(LoadSceneAsync(GameScenes.Combat.ToString()));
        }
        else
        {
            // Load island scene
            StartCoroutine(LoadSceneAsync(GameScenes.MainMenu.ToString()));
        }
    }

    IEnumerator LoadSceneAsync(string _sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }
}
