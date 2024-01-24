using HmsPlugin;
using HuaweiMobileServices.Id;
using HuaweiMobileServices.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class HuaweiAccountLoginManager : SceneSingleton<HuaweiAccountLoginManager>
{
    public static BoolEvent LoginSuccessfulEvent { get; set; }

    public static void Initialize()
    {
#if UNITY_EDITOR
        return;
#endif
        HMSAccountKitManager.Instance.OnSignInSuccess = OnLoginSuccess;
        HMSAccountKitManager.Instance.OnSignInFailed = OnLoginFailure;
    }

    public static void Login()
    {
        HMSAccountKitManager.Instance.SignIn();
    }

    public static void OnLoginSuccess(AuthAccount authHuaweiId)
    {
        Debug.Log($"Login succesful {authHuaweiId.DisplayName}");
        LoginSuccessfulEvent.Raise(true);
    }

    public static void OnLoginFailure(HMSException error)
    {
        Debug.LogError($"An error occurred {error.WrappedExceptionMessage}");
        LoginSuccessfulEvent.Raise(false);
    }
}
