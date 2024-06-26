using UnityEngine;
using System.Collections;
using System;

public class Languages
{
	public static bool localizationInitialized;
	public static void Init(SystemLanguage Language)
	{
		// Set branding for localization, thus logo and URL.
		// Set branding for localization, thus logo and URL.
		// For now, the language and localization file are tied to the specified branding.
		// We might change this in a future build, that the language can be set independent of the branding.
		// Beware: if you want to use localization in the startup and loading scene
		// You need to make sure it's available in the resources at that moment!

		Debug.Log("[Languages] Init called.");

		string selectedLanguage = "English (US)"; // The default language.

		switch (Language)
		{
			case SystemLanguage.English: selectedLanguage = "English (US)"; break;
			case SystemLanguage.Spanish: selectedLanguage = "Espanol (MX)"; break;
			default:
				Debug.LogWarning("[Languages] Unknown branding! Using language: English (US).");
				selectedLanguage = "English (US)";
				break;
		}

		XLocalization.language = selectedLanguage;
		localizationInitialized = true;

		Debug.Log("[Languages] Language set to: " + XLocalization.language);
	}
}

