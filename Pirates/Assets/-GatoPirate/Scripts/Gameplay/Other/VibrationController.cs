using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class VibrationController : SceneSingleton<VibrationController>
{
    private bool isVibrationOn = false;

    public void Initialize()
    {
        isVibrationOn = SettingsDataSaveManager.Instance.GetVibrationOn();
    }

    public void SetVibrationOn(bool _vibrationOn)
    {
        isVibrationOn = _vibrationOn;
    }

    public void TriggerVibrationOn()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
    }

    public void TriggerBasicAttackVibration()
    {
        if (!isVibrationOn)
            return;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
    }

    public void TriggerNormalAttackVibration()
    {
        if (!isVibrationOn)
            return;
        //HapticController.fallbackPreset = HapticPatterns.PresetType.LightImpact;
        //HapticPatterns.PlayEmphasis(0.05f, 0.05f);
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
    }

    public void TriggerReceiveNormalAttackVibration()
    {
        if (!isVibrationOn)
            return;
    }

    public void TriggerSpecialAttackVibration()
    {
        if (!isVibrationOn)
            return;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
    }

    public void TriggerReceiveSpecialAttackVibration(float _duration)
    {
        if (!isVibrationOn)
            return;

        HapticController.Stop();
        HapticController.fallbackPreset = HapticPatterns.PresetType.HeavyImpact;
        HapticPatterns.PlayConstant(0.85f, 0.5f, _duration);
    }
}
