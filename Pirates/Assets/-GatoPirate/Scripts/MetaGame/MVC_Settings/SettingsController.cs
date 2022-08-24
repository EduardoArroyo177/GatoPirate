using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private SettingsView settingsView;

    public FloatEvent SetMusicVolumeEvent { get; set; }
    public FloatEvent SetSoundsVolumeEvent { get; set; }
    public BoolEvent SetVibrationOnEvent { get; set; }

    private const string MUSIC_SETTINGS = "MUSIC_SETTINGS";
    private const string SOUNDS_SETTINGS = "SOUNDS_SETTINGS";
    private const string VIBRATION_SETTINGS = "VIBRATION_SETTINGS";

    public void Initialize()
    {
        settingsView.SettingsController = this;
        LoadSavedSettings();
    }

    private void LoadSavedSettings()
    {
        if (PlayerPrefs.HasKey(MUSIC_SETTINGS))
        {
            settingsView.ScrollbarMusic.value = PlayerPrefs.GetFloat(MUSIC_SETTINGS);
            SetMusicVolumeEvent.Raise(settingsView.ScrollbarMusic.value);
        }
        settingsView.ScrollbarMusic.onValueChanged.AddListener(MusicVolumeChange);

        if (PlayerPrefs.HasKey(SOUNDS_SETTINGS))
        {
            settingsView.ScrollbarSounds.value = PlayerPrefs.GetFloat(SOUNDS_SETTINGS);
            SetSoundsVolumeEvent.Raise(settingsView.ScrollbarSounds.value);
        }
        settingsView.ScrollbarSounds.onValueChanged.AddListener(SoundsVolumeChange);

        if (PlayerPrefs.HasKey(VIBRATION_SETTINGS))
        {
            settingsView.ToggleVibration.isOn = PlayerPrefs.GetInt(VIBRATION_SETTINGS) == 1;
            SetVibrationOnEvent.Raise(settingsView.ToggleVibration.isOn);
        }
    }

    private void MusicVolumeChange(float _newVolume)
    {
        SetMusicVolumeEvent.Raise(settingsView.ScrollbarMusic.value);
    }

    private void SoundsVolumeChange(float _newVolume)
    {
        SetSoundsVolumeEvent.Raise(settingsView.ScrollbarSounds.value);
    }

    public void SaveSettings()
    {
        
        PlayerPrefs.SetFloat(MUSIC_SETTINGS, settingsView.ScrollbarMusic.value);
        PlayerPrefs.SetFloat(SOUNDS_SETTINGS, settingsView.ScrollbarSounds.value);

        SetVibrationOnEvent.Raise(settingsView.ToggleVibration.isOn);
        PlayerPrefs.SetInt(VIBRATION_SETTINGS, settingsView.ToggleVibration.isOn ? 1 : 0);
    }

    public void Add1000Coins()
    {
        CurrencyDataSaveManager.Instance.UpdateCurrency(CurrencyType.GOLDEN_COINS, 1000);
    }
}
