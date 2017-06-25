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
    Player_HUD_CantDoAction,

    Player_Combat_LightAttack,
    Player_Combat_HeavyAttack,
    Player_Combat_LightAttackHit,
    Player_Combat_HeavyAttackHit,
    Player_Combat_BlockPosition,
    Player_Combat_BlockAttack,
    Player_Combat_SpecialAttackRed,
    Player_Combat_SpecialAttackBlue,
    Player_Combat_ReceiveAttack,
    Player_Combat_Die,
    Player_Combat_Respawn,

    Enemy_FSMinfo_TargetDettected,
    Enemy_FSMinfo_TargetMissing,
    Enemy_FSMinfo_Spawn,

    Enemy_Move_Idle,
    Enemy_Move_Walking,

    Enemy_Combat_Attack,
    Enemy_Combat_AttackHit,
    Enemy_Combat_ReceiveAttack,
    Enemy_Combat_Die,

    Environment_Generator_GeneratorNoise,
    Environment_Generator_GeneratorSpawn,

    Environment_Bridge_Bridge,

    Environment_PlayerToWorld_MessageReceived,
    Environment_PlayerToWorld_Interact,

    Environment_PickUps_HP,
    Environment_PickUps_Energy,

    Environment_PowerUps,

    Environment_Core,

    Environment_Artillery_Movement,
    Environment_Artillery_Shot,

    Player_Combat_ShieldAttack

}

