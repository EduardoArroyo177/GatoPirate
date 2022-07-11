using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMockCrewSelectionController : MonoBehaviour
{
    [SerializeField]
    private CombatData combatData;
    [SerializeField]
    private CatData[] oneCrew;
    [SerializeField]
    private CatData[] twoCrew;
    [SerializeField]
    private CatData[] threeCrew;
    [SerializeField]
    private CatData[] fourCrew;

    public void StartWithOneCannon()
    {
        combatData.CatCrewDataList = oneCrew;
        LoadCombatScene();
    }

    public void StartWithTwoCannons()
    {
        combatData.CatCrewDataList = twoCrew;
        LoadCombatScene();
    }

    public void StartWithThreeCannons()
    {
        combatData.CatCrewDataList = threeCrew;
        LoadCombatScene();
    }

    public void StartWithFourCannons()
    {
        combatData.CatCrewDataList = fourCrew;
        LoadCombatScene();
    }

    private void LoadCombatScene()
    {
        SceneLoaderManager.Instance.LoadScene(GameScenes.TestCombatScene);
    }
}
