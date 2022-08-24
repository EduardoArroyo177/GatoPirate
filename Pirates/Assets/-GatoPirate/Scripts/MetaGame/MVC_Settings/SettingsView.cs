using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField]
    private Scrollbar scrollbarMusic;
    [SerializeField]
    private Scrollbar scrollbarSounds;
    [SerializeField]
    private Toggle toggleVibration;

    public SettingsController SettingsController { get; set; }
    public Scrollbar ScrollbarMusic { get => scrollbarMusic; set => scrollbarMusic = value; }
    public Scrollbar ScrollbarSounds { get => scrollbarSounds; set => scrollbarSounds = value; }
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
