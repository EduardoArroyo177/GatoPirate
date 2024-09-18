using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityAtoms;
using UnityEngine;

public class AdsControllerCombat : MonoBehaviour
{
    public VoidEvent LoadReviveAdEvent { get; set; }
    public VoidEvent LoadDoubleRewardAdEvent { get; set; }
    public VoidEvent LoadCombatFinishedAdEvent { get; set; }
    public VoidEvent CombatRewardAdSuccessEvent { get; set; }
    public VoidEvent ReviveSuccessEvent { get; set; }
    public VoidEvent DoubleRewardSuccessEvent { get; set; }
    public VoidEvent UnloadEventsEvent { get; set; }

    public virtual void Initialize() { }
    public virtual void PlayAdRevive(Void _item) { }
    public virtual void PlayAdDoubleReward(Void _item) { }
}
