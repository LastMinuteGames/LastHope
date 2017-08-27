using UnityEngine;
using UnityEngine.UI;
using LastHope.SoundManager2;



public class SoundManagerDemo : MonoBehaviour
{
    public Slider SoundSlider;
    public Slider MusicSlider;
    public Slider MasterSlider;

    public AudioSource[] SoundAudioSources;
    public AudioSource[] MusicAudioSources;
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaySound(0);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlaySound(0, new Vector3(10,0,0));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaySound(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayMusic(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayMusic(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayMusic(2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SoundManagement", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayLoopingSound(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            StopLoopingSound(0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayLoopingSound(1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayLoopingSound(1, new Vector3(10, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StopLoopingSound(1);
        }
    }



    private void PlaySound(int index)
    {
        SoundAudioSources[index].PlaySound();
    }

    private void PlaySound(int index, Vector3 position)
    {
        SoundAudioSources[index].PlaySound(position);
    }

    private void PlayLoopingSound(int index)
    {
        SoundAudioSources[index].PlayLoopingSound();
    }
    private void PlayLoopingSound(int index, Vector3 position)
    {
        SoundAudioSources[index].PlayLoopingSound(position);
    }

    private void StopLoopingSound(int index)
    {
        SoundAudioSources[index].StopLoopingSound();
    }

    private void PlayMusic(int index)
    {
        MusicAudioSources[index].PlayLoopingMusic();
    }

    private void PauseGame()
    {
        //SoundManager2.Pau
    }

    private void Resume()
    {

    }

    private void Stop()
    {

    }

    public void SoundVolumeChanged()
    {
        SoundManager2.SoundVolume = SoundSlider.value;
    }

    public void MusicVolumeChanged()
    {
        SoundManager2.MusicVolume = MusicSlider.value;
    }

    public void GlobalMusicChanged()
    {
        SoundManager2.MasterVolume = MasterSlider.value;
    }
}
