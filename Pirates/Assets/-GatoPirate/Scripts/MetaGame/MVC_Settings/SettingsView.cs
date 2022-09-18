using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField]
    private Slider sliderMusic;
    [SerializeField]
    private Slider sliderSounds;
    [SerializeField]
    private Toggle toggleVibration;

    public SettingsController SettingsController { get; set; }
    public Slider SliderMusic { get => sliderMusic; set => sliderMusic = value; }
    public Slider SliderSounds { get => sliderSounds; set => sliderSounds = value; }
    public Toggle ToggleVibration { get => toggleVibration; set => toggleVibration = value; }

    public void AcceptAndClose()
    {
        SettingsController.SaveSettings();
        gameObject.SetActive(false);
    }

    // TODO: For testing only, remove later
    public void AddCoins()
    {
        SettingsController.Add1000Coins();
    }
}
