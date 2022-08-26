using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsDataSaveManager : SceneSingleton<SettingsDataSaveManager>
{
    [Header("Settings")]
    [SerializeField]
    private DataSaveSettingsStructure settings;

    public const string SETTINGS_SAVING_DATA_KEY = "SETTINGS_SAVED";

    public DataSaveSettingsStructure Settings { get => settings; set => settings = value; }

    public void LoadSettingsSavedData()
    {
        string dataSave = PlayerPrefs.GetString(SETTINGS_SAVING_DATA_KEY);
        if (string.IsNullOrEmpty(dataSave))
        {
            Settings = new DataSaveSettingsStructure();
            SaveSettingsData();
        }
        else 
        {
            Settings = JsonUtility.FromJson<DataSaveSettingsStructure>(dataSave);
        }
    }

    public void UpdateSettings(float _musicVolume, float _soundsVolume, bool _vibrationOn)
    {
        Settings.MusicVolume = _musicVolume;
        Settings.SoundsVolume = _soundsVolume;
        Settings.VibrationOn = _vibrationOn;
        SaveSettingsData();
    }

    public float GetMusicVolume()
    {
        return Settings.MusicVolume;
    }

    public float GetSoundsVolume()
    {
        return Settings.SoundsVolume;
    }

    public bool GetVibrationOn()
    {
        return Settings.VibrationOn;
    }

    private void SaveSettingsData()
    {
        PlayerPrefs.SetString(SETTINGS_SAVING_DATA_KEY, JsonUtility.ToJson(Settings));
    }
}
