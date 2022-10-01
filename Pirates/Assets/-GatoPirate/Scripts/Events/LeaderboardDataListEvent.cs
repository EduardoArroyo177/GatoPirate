using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UDP;


namespace UnityAtoms
{
    [CreateAssetMenu(menuName = "Gato Pirate/Events/Leaderboard Data List Event", fileName = "LeaderboardDataListEvent", order = 1)]

    public class LeaderboardDataListEvent : AtomEvent<List<LeaderboardScoreData>> { }
}