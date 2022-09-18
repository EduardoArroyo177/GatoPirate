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

    public void Initialize()
    {
        settingsView.SettingsController = this;
        LoadSavedSettings();
    }

    private void LoadSavedSettings()
    {
        settingsView.SliderMusic.value = SettingsDataSaveManager.Instance.GetMusicVolume();
        SetMusicVolumeEvent.Raise(settingsView.SliderMusic.value);

        settingsView.SliderSounds.value = SettingsDataSaveManager.Instance.GetSoundsVolume();
        SetSoundsVolumeEvent.Raise(settingsView.SliderSounds.value);

        settingsView.ToggleVibration.onValueChanged.AddListener(OnToggleChange);
        settingsView.ToggleVibration.isOn = SettingsDataSaveManager.Instance.GetVibrationOn();
        VibrationController.Instance.SetVibrationOn(settingsView.ToggleVibration.isOn);

        settingsView.SliderMusic.onValueChanged.AddListener(MusicVolumeChange);
        settingsView.SliderSounds.onValueChanged.AddListener(SoundsVolumeChange);
    }

    private void MusicVolumeChange(float _newVolume)
    {
        SetMusicVolumeEvent.Raise(settingsView.SliderMusic.value);
    }

    private void SoundsVolumeChange(float _newVolume)
    {
        SetSoundsVolumeEvent.Raise(settingsView.SliderSounds.value);
    }

    private void OnToggleChange(bool _value)
    {
        if (_value)
            VibrationController.Instance.TriggerVibrationOn();
    }

    public void SaveSettings()
    {
        SettingsDataSaveManager.Instance.UpdateSettings(settingsView.SliderMusic.value,
            settingsView.SliderSounds.value,
            settingsView.ToggleVibration.isOn);

        VibrationController.Instance.SetVibrationOn(settingsView.ToggleVibration.isOn);
    }

    public void Add1000Coins()
    {
        CurrencyDataSaveManager.Instance.UpdateCurrency(CurrencyType.GOLDEN_COINS, 1000);
    }
}
