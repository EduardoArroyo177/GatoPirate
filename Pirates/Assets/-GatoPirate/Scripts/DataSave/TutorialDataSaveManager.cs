using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDataSaveManager : SceneSingleton<TutorialDataSaveManager>
{
    [SerializeField]
    private DataSaveTutorialListStructure tutorialCompleted;

    public const string TUTORIAL_SAVING_DATA_KEY = "TUTORIAL_COMPLETED";

    public DataSaveTutorialListStructure TutorialCompleted { get => tutorialCompleted; set => tutorialCompleted = value; }

    #region Initialization
    public void LoadTutorialSavedData()
    {
        string dataSave = PlayerPrefs.GetString(TUTORIAL_SAVING_DATA_KEY);
        if (string.IsNullOrEmpty(dataSave))
        {
            TutorialCompleted = new DataSaveTutorialListStructure();
            // Combat tutorial
            DataSaveTutorialStructure initialTutorialStructure = new DataSaveTutorialStructure();
            initialTutorialStructure.TutorialType = TutorialType.COMBAT.ToString();
            initialTutorialStructure.IsCompleted = false;
            TutorialCompleted.TutorialList.Add(initialTutorialStructure);
            // Weak spot tutorial
            initialTutorialStructure = new DataSaveTutorialStructure();
            initialTutorialStructure.TutorialType = TutorialType.COMBAT_WEAK_SPOT.ToString();
            initialTutorialStructure.IsCompleted = false;
            TutorialCompleted.TutorialList.Add(initialTutorialStructure);
            // Resources box tutorial
            initialTutorialStructure = new DataSaveTutorialStructure();
            initialTutorialStructure.TutorialType = TutorialType.COMBAT_RESOURCES_BOX.ToString();
            initialTutorialStructure.IsCompleted = false;
            TutorialCompleted.TutorialList.Add(initialTutorialStructure);
            // Meta game tutorial
            initialTutorialStructure = new DataSaveTutorialStructure();
            initialTutorialStructure.TutorialType = TutorialType.META_GAME.ToString();
            initialTutorialStructure.IsCompleted = false;
            TutorialCompleted.TutorialList.Add(initialTutorialStructure);

            SaveTutorialData();
        }
        else
        {
            // Load saved data
            TutorialCompleted = JsonUtility.FromJson <DataSaveTutorialListStructure>(dataSave);
        }
    }
    #endregion

    #region Update data
    public bool GetTutorialCompletedStatus(TutorialType _tutorialType)
    {
        int index = TutorialCompleted.TutorialList.FindIndex(x => x.TutorialType.Equals(_tutorialType.ToString()));

        if (index < 0)
        {
            Debug.LogError($"An error occured while retrieving tutorial data: {_tutorialType}");
            return true;
        }

        return TutorialCompleted.TutorialList[index].IsCompleted;
    }

    public void UpdateTutorialCompleted(TutorialType _tutorialType)
    {
        int index = TutorialCompleted.TutorialList.FindIndex(x => x.TutorialType.Equals(_tutorialType.ToString()));

        if (index < 0)
        {
            Debug.LogError($"An error occured while retrieving tutorial data: {_tutorialType}");
            return;
        }

        TutorialCompleted.TutorialList[index].IsCompleted = true;
        SaveTutorialData();
    }
    #endregion

    #region Save data
    private void SaveTutorialData()
    {
        PlayerPrefs.SetString(TUTORIAL_SAVING_DATA_KEY, JsonUtility.ToJson(TutorialCompleted));
    }
    #endregion
}
