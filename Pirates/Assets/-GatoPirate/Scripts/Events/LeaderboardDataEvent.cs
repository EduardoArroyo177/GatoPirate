using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtoms
{
    [CreateAssetMenu(menuName = "Gato Pirate/Events/Leaderboard Data Event", fileName = "LeaderboardDataEvent", order = 1)]

    public class LeaderboardDataEvent : AtomEvent<LeaderboardScoreData> { }
}
