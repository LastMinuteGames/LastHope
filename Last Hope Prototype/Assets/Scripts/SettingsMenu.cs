using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Slider qualitySlider;
    public Slider volumeSlider;
    public Text qualityValue;

    // Use this for initialization
    void Start () {
        qualityValue.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
        qualitySlider.value = QualitySettings.GetQualityLevel();
    }

    public void Quality_Changed(float newValue)
    {
        qualityValue.text = QualitySettings.names[(int)newValue];
        QualitySettings.SetQualityLevel((int)newValue);
    }

    public void Volume_Changed(float newValue)
    {
        Debug.Log(newValue);
        //sound.volume = newValue;
        AudioListener.volume = newValue;
        
    }

}
