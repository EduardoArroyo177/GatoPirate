using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMockCrewSelectionController : MonoBehaviour
{
    [SerializeField]
    private CombatData combatData;
    [SerializeField]
    private CatCrewController[] oneCrew;
    [SerializeField]
    private CatCrewController[] twoCrew;
    [SerializeField]
    private CatCrewController[] threeCrew;
    [SerializeField]
    private CatCrewController[] fourCrew;

    public void StartWithOneCannon()
    {
        combatData.CatCrewControllerList = oneCrew;
        LoadCombatScene();
    }

    public void StartWithTwoCannons()
    {
        combatData.CatCrewControllerList = twoCrew;
        LoadCombatScene();
    }

    public void StartWithThreeCannons()
    {
        combatData.CatCrewControllerList = threeCrew;
        LoadCombatScene();
    }

    public void StartWithFourCannons()
    {
        combatData.CatCrewControllerList = fourCrew;
        LoadCombatScene();
    }

    private void LoadCombatScene()
    {
        SceneLoaderManager.Instance.LoadScene(GameScenes.TestCombatScene);
    }
}
