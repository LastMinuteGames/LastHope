using UnityEngine;
using UnityEngine.UI;

using LastHope.SoundManager;



public class soundmanagementTest : MonoBehaviour
{
    public Slider SoundSlider;
    public Slider MusicSlider;
    public Slider GlobalMusicSlider;

    public AudioSource[] SoundAudioSources;
    public AudioSource[] MusicAudioSources;

    void PlaySound(int index)
    {
        SoundAudioSources[index].PlaySound(SoundAudioSources[index].clip);
    }

    //TODO::play music when bug is fixed
    void PLayMusic(int index)
    {
        MusicAudioSources[index].PlayLoopingMusic(1.0f, 1.0f, true);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaySound(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaySound(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlaySound(2);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

    public void OnSoundVolumeChanged()
    {
        SoundManager.SoundVolume = SoundSlider.value;
    }

    public void OnMusicVolumeChanged()
    {
        SoundManager.MusicVolume = MusicSlider.value;
    }

    public void OnGlobalVolumeChanged()
    {
        SoundManager.GlobalVolume = GlobalMusicSlider.value;
    }

}
