using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationController : SceneSingleton<VibrationController>
{
    public void TriggerBasicAttackVibration()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
    }

    public void TriggerNormalAttackVibration()
    {
        //HapticController.fallbackPreset = HapticPatterns.PresetType.LightImpact;
        //HapticPatterns.PlayEmphasis(0.05f, 0.05f);
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
    }

    public void TriggerReceiveNormalAttackVibration()
    {

    }

    public void TriggerSpecialAttackVibration()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);

    }

    public void TriggerReceiveSpecialAttackVibration(float _duration)
    {
        HapticController.Stop();
        HapticController.fallbackPreset = HapticPatterns.PresetType.HeavyImpact;
        HapticPatterns.PlayConstant(0.85f, 0.5f, _duration);
    }
}
