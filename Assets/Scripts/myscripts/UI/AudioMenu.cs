using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMenu : MonoBehaviour
{
    [System.Serializable]
    public class VolumeControl
    {
        public string parameterName;
        public Slider volumeSlider;
        public Button muteUnmuteButton;
        public Sprite muteIcon;
        public Sprite unmuteIcon;

        [HideInInspector]
        public bool isMuted;
        [HideInInspector]
        public float previousVolumeBeforeMute;  // This is to remember the volume level before muting
    }

    public AudioMixer audioMixer;
    public List<VolumeControl> volumeControls;

    private const float MinVolume = -80f;

    void Start()
    {
        foreach (var control in volumeControls)
        {
            float currentLinearVolume = GetCurrentVolumeLinear(control.parameterName);
            control.volumeSlider.value = currentLinearVolume;  // Set the slider to the current volume

            var currentControl = control;
            currentControl.muteUnmuteButton.onClick.AddListener(() => ToggleMuteUnmute(currentControl));
            currentControl.volumeSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, currentControl));
        }
    }

    public float GetCurrentVolumeLinear(string parameterName)
    {
        float currentDB;
        audioMixer.GetFloat(parameterName, out currentDB);
        return DecibelToLinear(currentDB);
    }

    public float LinearToDecibel(float linear)
    {
        if (linear != 0)
            return 20.0f * Mathf.Log10(linear);
        else
            return MinVolume;
    }

    public float DecibelToLinear(float dB)
    {
        return Mathf.Pow(10.0f, dB / 20.0f);
    }

    public void SetVolume(float linearVolume, string parameterName)
    {
        float dB = LinearToDecibel(linearVolume);
        audioMixer.SetFloat(parameterName, dB);
    }

    private bool isProgrammaticChange = false;

    public void ToggleMuteUnmute(VolumeControl control)
    {
        if (control.isMuted)
            UnmuteAudio(control);
        else
            MuteAudio(control);
    }

    private void MuteAudio(VolumeControl control)
    {
        control.previousVolumeBeforeMute = control.volumeSlider.value; // Remember the current volume

        isProgrammaticChange = true;  // Set the flag
        control.volumeSlider.value = 0;
        isProgrammaticChange = false; // Reset the flag

        audioMixer.SetFloat(control.parameterName, MinVolume);
        control.isMuted = true;
        control.muteUnmuteButton.GetComponent<Image>().sprite = control.muteIcon;
    }

   private void UnmuteAudio(VolumeControl control)
    {
        isProgrammaticChange = true;  // Set the flag
        control.volumeSlider.value = control.previousVolumeBeforeMute; // Restore the volume level
        isProgrammaticChange = false; // Reset the flag

        SetVolume(control.previousVolumeBeforeMute, control.parameterName);
        control.isMuted = false;
        control.muteUnmuteButton.GetComponent<Image>().sprite = control.unmuteIcon;
    }

    public void OnSliderValueChanged(float value, VolumeControl control)
    {
        if (isProgrammaticChange) // Check if the change was programmatic
            return;

        if (control.isMuted)
        {
            control.isMuted = false;
            control.muteUnmuteButton.GetComponent<Image>().sprite = control.unmuteIcon; 
        }
        SetVolume(value, control.parameterName);
    }
}
