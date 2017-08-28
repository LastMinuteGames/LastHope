using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LastHope.SoundManager2;

public class AudioSources : MonoBehaviour
{
    public static AudioSources instance = null;

    [SerializeField] private AudioSource[] SoundAudioSources;
    [SerializeField] private AudioSource[] MusicAudioSources;

    public void PlaySound(int index, float volume = 1)
    {
        SoundAudioSources[index].PlaySound(volume);
    }
    public void Play3DSound(int index, Vector3 position, float volume = 1)
    {
        SoundAudioSources[index].PlaySound(position, volume);
    }

    public void PlayAmbientSound(int index, float volume = 1)
    {
        SoundAudioSources[index].PlayLoopingSound(volume);
    }
    public void Play3DAmbientSound(int index, Vector3 position, float volume = 1)
    {
        SoundAudioSources[index].PlayLoopingSound(position, volume);
    }
    public void StopAmbientsound(int index)
    {
        SoundAudioSources[index].StopLoopingSound();
    }

    public void PlayMusic(int index, float volume = 1)
    {
        MusicAudioSources[index].PlayLoopingMusic(volume);
    }

    public void SetSoundVolume(float volume)
    {
        SoundManager2.SoundVolume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        SoundManager2.MusicVolume = volume;
    }
    public void SetMasterVolume(float volume)
    {
        SoundManager2.MasterVolume = volume;
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
    #region Menu
    Menu_SwapSelection,
    Menu_ApplySelection,
    Menu_StartGame,
    Menu_Pause,
    Menu_Unpause,
    Menu_BackToMenu,
    Menu_QuitGame,
    Menu_CantDoAction,
    #endregion

    #region Player
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
    Player_HUD_CantDoAction,

    Player_Combat_LightAttack,
    Player_Combat_HeavyAttack,
    Player_Combat_ShieldAttack,
    Player_Combat_LightAttackHit,
    Player_Combat_HeavyAttackHit,
    Player_Combat_ShieldAttackHit,
    Player_Combat_BlockPosition,
    Player_Combat_BlockAttack,
    Player_Combat_SpecialAttackRed,
    Player_Combat_SpecialAttackBlue,
    Player_Combat_ChargeBlueAttack,
    Player_Combat_ReleaseBlueAttack,
    Player_Combat_ReceiveAttack,
    Player_Combat_Die,
    Player_Combat_Respawn,
    #endregion

    #region Enemy1
    Enemy_FSMinfo_TargetDettected,
    Enemy_FSMinfo_TargetMissing,
    Enemy_FSMinfo_Spawn,

    Enemy_Move_Idle,
    Enemy_Move_Walking,

    Enemy_Combat_Attack,
    Enemy_Combat_AttackHit,
    Enemy_Combat_ReceiveAttack,
    Enemy_Combat_Die,
    #endregion

    #region Enviroment
    Environment_Generator_GeneratorNoise,
    Environment_Generator_GeneratorSpawn,

    Environment_Bridge_Bridge,

    Environment_PlayerToWorld_MessageReceived,
    Environment_PlayerToWorld_PressButton,
    Environment_PlayerToWorld_Interact,

    Environment_Unclassified_EngineShutdown,
    Environment_Unclassified_PowerUp,
    Environment_Unclassified_Core,

    Environment_PickUps_HP,
    Environment_PickUps_Energy,

    Environment_Artillery_Movement,
    Environment_Artillery_Shot,

    Environment_BreakEnvironment_BreakBarricade,
    Environment_BreakEnvironment_BreakBench,
    Environment_BreakEnvironment_BreakTrafficLight
    #endregion
}

