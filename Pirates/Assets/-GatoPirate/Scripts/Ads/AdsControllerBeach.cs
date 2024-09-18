using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityAtoms;
using UnityEngine;

public class AdsControllerBeach : MonoBehaviour
{
    public VoidEvent FreeCoinsRewardSuccessEvent { get; set; }
    public VoidEvent LoadFreeCoinsAdRecruitmentEvent { get; set; }
    public VoidEvent LoadFreeCoinsAdStoreEvent { get; set; }
    public CurrencyTypeIntEvent ShowRewardedCurrencyEvent { get; set; }
    public VoidEvent UnloadEventsEvent { get; set; }

    public virtual void Initialize() { }
    public virtual void PlayAdFreeCoinsRecruitment(Void _item) { }
    public virtual void PlayAdFreeCoinsStore(Void _item) { }
    public virtual void FreeCoinsSuccess(Void _item) { }
}
