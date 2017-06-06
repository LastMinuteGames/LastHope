using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public Text qualityText;
    public Text volumeText;
    public Slider qualitySlider;
    public Slider volumeSlider;
    public Text qualityValue;
    private int focus = 0;
    public Color unselectedTextColor;
    public Color selectedTextColor;

    private bool leftInUse = false;
    private bool rightInUse = false;

    // Use this for initialization
    void Start()
    {
        qualityValue.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
        qualitySlider.value = QualitySettings.GetQualityLevel();

        unselectedTextColor = new Color(0F, 0F, 0F, 1);
        selectedTextColor = new Color(1F, 1F, 1F, 1);

        qualityText.color = selectedTextColor;
        qualityValue.color = selectedTextColor;
        volumeText.color = unselectedTextColor;
    }

    void Update()
    {
        if (InputManager.LeftJoystickUp() || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (focus == 1)
            {
                focus--;
                qualityText.color = selectedTextColor;
                qualityValue.color = selectedTextColor;
                volumeText.color = unselectedTextColor;
            }
        }
        if (InputManager.LeftJoystickDown() || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (focus == 0)
            {
                focus++;
                qualityText.color = unselectedTextColor;
                qualityValue.color = unselectedTextColor;
                volumeText.color = selectedTextColor;
            }
        }

        switch (focus)
        {
            case 0:
                if (InputManager.LeftJoystickLeft() || Input.GetKeyDown("left"))
                {
                    if (!leftInUse)
                    {
                        qualitySlider.value--;
                        leftInUse = true;
                    }
                }
                else
                {
                    leftInUse = false;
                }

                if (InputManager.LeftJoystickRight() || Input.GetKeyDown("right"))
                {
                    if (!rightInUse)
                    {
                        qualitySlider.value++;
                        rightInUse = true;
                    }
                }
                else
                {
                    rightInUse = false;
                }
                break;
            case 1:
                if (InputManager.LeftJoystickLeft() || Input.GetKey(KeyCode.LeftArrow))
                {
                    volumeSlider.value -= 0.01f;
                }
                if (InputManager.LeftJoystickRight() || Input.GetKey(KeyCode.RightArrow))
                {
                    volumeSlider.value += 0.01f;
                }
                break;
        }


        if (InputManager.LightAttack() || Input.GetKeyDown(KeyCode.Return))
        {

        }
        if (InputManager.Dodge() || Input.GetKeyDown(KeyCode.Escape))
        {

        }
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
