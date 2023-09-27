using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityAtoms;
using UnityEngine;

public class IAdsController : MonoBehaviour
{
    public VoidEvent FreeCoinsRewardSuccessEvent { get; set; }
    public VoidEvent LoadFreeCoinsAdRecruitmentEvent { get; set; }
    public VoidEvent LoadFreeCoinsAdStoreEvent { get; set; }
    public CurrencyTypeIntEvent ShowRewardedCurrencyEvent { get; set; }
    public virtual void Initialize() { }

}
