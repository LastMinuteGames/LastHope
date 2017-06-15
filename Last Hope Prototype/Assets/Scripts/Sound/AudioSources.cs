using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LastHope.SoundManager;

public class AudioSources : MonoBehaviour
{
    public static AudioSources instance = null;

    [SerializeField] private AudioSource[] SoundAudioSources;
    [SerializeField] private AudioSource[] MusicAudioSources;


    public void PlaySound(int index)
    {
        SoundAudioSources[index].PlaySound(SoundAudioSources[index].clip);
    }
    public void PlayMusic(int index)
    {
        MusicAudioSources[index].PlayLoopingMusic(1.0f, 1.0f, true);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


}


public enum AudiosMusic
{
    MainTheme,
    CityTheme,
    CombatTheme
}


public enum AudiosSoundFX
{
    Menu_SwapSelection,
    Menu_ApplySelection,
    Menu_StartGame,
    Menu_Pause,
    Menu_Unpause,
    Menu_BackToMenu,
    Menu_QuitGame,
    Menu_CantDoAction,

    Player_Movement_Idle,
    Player_Movement_Walk,
    Player_Movement_Dash,

    Player_HUD_HpUp,
    Player_HUD_HpDown,
    Player_HUD_MaxHpUp,
    Player_HUD_EnergyUp,
    Player_HUD_EnergyDown,
    Player_HUD_MaxEnergyUp,
    Player_HUD_SelectStanceBlue,
    Player_HUD_SelectStanceRed,
    Player_HUD_CantDoAction
}

