using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLocalization : MonoBehaviour
{
    [SerializeField]
    private bool forceEnglish;

    private void Awake()
    {
        if (Languages.localizationInitialized)
            return;

        if (forceEnglish)
            Languages.Init(SystemLanguage.English);
        else
        {
            // TODO: Load saved language

            // If there is no saved language, load system language
            Languages.Init(Application.systemLanguage);
        }
    }
}
