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
    public bool LoggedIn { get; set; }
    public bool JosInit { get; set; }
    public BoolEvent LoginSuccessfulEvent { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
#if UNITY_EDITOR
        return;
#endif
        HMSAccountKitManager.Instance.OnSignInSuccess = OnLoginSuccess;
        HMSAccountKitManager.Instance.OnSignInFailed = OnLoginFailure;
    }

    public void Login()
    {
        HMSAccountKitManager.Instance.SignIn();
    }

    public void OnLoginSuccess(AuthAccount authHuaweiId)
    {
        Debug.Log($"Login succesful {authHuaweiId.DisplayName}");
        LoggedIn = true;
        LoginSuccessfulEvent.Raise(true);
    }

    public void OnLoginFailure(HMSException error)
    {
        Debug.LogError($"An error occurred {error.WrappedExceptionMessage}");
        LoginSuccessfulEvent.Raise(false);
    }
}
