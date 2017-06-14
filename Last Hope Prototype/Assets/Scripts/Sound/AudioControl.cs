using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LastHope.SoundManager;


public class AudioControl : MonoBehaviour
{

    public AudioSource[] SoundAudioSources;
    public AudioSource[] MusicAudioSources;

    public void PlaySound(int index)
    {
        SoundAudioSources[index].PlaySound(SoundAudioSources[index].clip);
    }

    public void PLayMusic(int index)
    {
        MusicAudioSources[index].PlayLoopingMusic(1.0f, 1.0f, true);
    }


}
