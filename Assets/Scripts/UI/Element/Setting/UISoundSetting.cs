using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundSetting : MonoBehaviour
{
    private SoundManager soundManager;

    [SerializeField]
    private Slider allVolumeSlider;
    [SerializeField]
    private Slider bgmVolumeSlider;
    [SerializeField]
    private Slider sfxVolumeSlider;

    private void Start()
    {
        soundManager = SoundManager.Instance;

        allVolumeSlider.value = soundManager.AllMasterVolume;
        bgmVolumeSlider.value = soundManager.BGMMasterVolume;
        sfxVolumeSlider.value = soundManager.SFXMasterVolume;
    }

    public void ResetSetting()
    {
        UpdateAllVolume(0.5f);
        UpdateBGM(0.5f);
        UpdateSFX(0.5f);

        allVolumeSlider.value = soundManager.AllMasterVolume;
        bgmVolumeSlider.value = soundManager.BGMMasterVolume;
        sfxVolumeSlider.value = soundManager.SFXMasterVolume;
    }

    public void UpdateAllVolume(float value)
    {
        soundManager.ChangeAllVolume(value);
    }

    public void UpdateBGM(float value)
    {
        soundManager.ChangeBGMVolume(value);
    }

    public void UpdateSFX(float value)
    {
        soundManager.ChangeSFXVolume(value);
    }
}
