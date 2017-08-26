using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace LastHope.SoundManager2
{

    #region AudioSourceExtensions
    public static class AudioSourceExtensions
    {
        public static void PlaySound(this AudioSource audioSource, float volume = 1)
        {
            if (CheckAudioSourceAndClip(audioSource))
            {
                SoundManager2.PlaySound(audioSource, audioSource.clip, volume);
            }
        }
        public static void PlayLoopingSound(this AudioSource audioSource, float volume = 1)
        {
            if (CheckAudioSourceAndClip(audioSource))
            {
                SoundManager2.PlayLoopingSound(audioSource, audioSource.clip, volume);
            }
        }
        public static void PlayLoopingMusic(this AudioSource audioSource, float volume = 1)
        {
            if (audioSource != null)
            {
                SoundManager2.PlayLoopingMusic(audioSource, volume);
            }
        }
        private static bool CheckAudioSourceAndClip(AudioSource audioSource)
        {
            if (audioSource == null)
            {
                Debug.LogWarning("Null audioSource sent to PlaySound()");
                return false;
            }
            if (audioSource.clip == null)
            {
                Debug.LogWarning("No clip on audioSource sent to PlaySound()");
                return false;
            }
            return true;
        }
        public static void StopLoopingSound(this AudioSource audioSource)
        {
            if (CheckAudioSourceAndClip(audioSource))
            {
                SoundManager2.StopLoopingSound(audioSource);
            }
        }
    }
    #endregion


    public class SoundManager2 : MonoBehaviour
    {

        #region General volume management

        private static float masterVolume = 1f;
        private static float soundVolume = 1f;
        private static float musicVolume = 1f;
        private static float realSoundVolume = 1f;
        private static float realMusicVolume = 1f;

        public static float MasterVolume
        {
            get { return masterVolume; }
            set
            {
                if (value != masterVolume)
                {
                    masterVolume = value;
                    realSoundVolume = soundVolume * masterVolume;
                    realMusicVolume = musicVolume * masterVolume;
                    UpdateMusicsVolume();
                    UpdateAmbientVolume();
                }
            }
        }

        public static float SoundVolume
        {
            get { return soundVolume; }
            set
            {
                soundVolume = value;
                realSoundVolume = soundVolume * masterVolume;
                UpdateAmbientVolume();
            }
        }

        public static float MusicVolume
        {
            get { return musicVolume; }
            set
            {
                musicVolume = value;
                realMusicVolume = musicVolume * masterVolume;
                UpdateMusicsVolume();
            }
        }

        private static void UpdateMusicsVolume()
        {
            foreach (LoopingAudioSource loopingAudioSource in musics)
            {
                if (!loopingAudioSource.isStopping)
                {
                    loopingAudioSource.targetVolume = loopingAudioSource.initialTargetVolume * realMusicVolume;
                }
            }
        }

        private static void UpdateAmbientVolume()
        {
            foreach (LoopingAudioSource loopingAudioSource in ambientSounds)
            {
                loopingAudioSource.targetVolume = loopingAudioSource.initialTargetVolume * realSoundVolume;
            }
        }
        #endregion


        private static List<LoopingAudioSource> musics = new List<LoopingAudioSource>();
        private static List<LoopingAudioSource> ambientSounds = new List<LoopingAudioSource>();
        private static Dictionary<AudioClip, List<float>> soundsDictionary = new Dictionary<AudioClip, List<float>>();
        private static int maxDuplicateAudioClips = 5;


        #region Instantiation

        private static GameObject rootGo;
        private static SoundManager2 instance;
        private static bool instantiated = false;

        public static void Instantiate()
        {
            if (instance != null)
            {
                Debug.LogWarning("Trying to instantiate SounManager, 2nd time");
                return;
            }

            rootGo = new GameObject();
            rootGo.name = "Sound Manager";
            rootGo.hideFlags = HideFlags.DontSave;
            instance = rootGo.AddComponent<SoundManager2>();
            DontDestroyOnLoad(rootGo);
            instantiated = true;
        }
        #endregion



        #region Play Functions

        public static void PlaySound(AudioSource audioSource, AudioClip audioClip, float volume)
        {
            if (!instantiated)
            {
                Instantiate();
            }

            List<float> volumes;
            bool isClipRegistered = soundsDictionary.TryGetValue(audioClip, out volumes);

            if (!isClipRegistered)
            {
                volumes = new List<float>();
                soundsDictionary[audioClip] = volumes;
            }
            else if (volumes.Count >= maxDuplicateAudioClips)
            {
                return;
            }

            float minVolume = 2;
            float maxVolume = -2;
            foreach (float vol in volumes)
            {
                minVolume = Mathf.Min(minVolume, vol);
                maxVolume = Mathf.Max(maxVolume, vol);
            }

            float targetVolume = volume * realSoundVolume;
            if (maxVolume > 0.5f)
            {
                targetVolume = (minVolume + maxVolume) / (float)(volumes.Count + 2);
            }

            volumes.Add(targetVolume);
            audioSource.PlayOneShot(audioClip, targetVolume);
            instance.StartCoroutine(CleanUpVolumeOfClip(audioClip, targetVolume));
        }

        public static void PlayLoopingSound(AudioSource audioSource, AudioClip audioClip, float volume)
        {
            if (!instantiated)
            {
                Instantiate();
            }

            LoopingAudioSource loopingAudioSource = new LoopingAudioSource(audioSource, false);
            float targetVolume = volume * realSoundVolume;
            loopingAudioSource.Play(targetVolume);
            ambientSounds.Add(loopingAudioSource);

        }

        public static void StopLoopingSound (AudioSource audioSource)
        {
            for (int i = 0; i < ambientSounds.Count; i++)
            {
                LoopingAudioSource l = ambientSounds[i];
                if (l.audioSource == audioSource)
                {
                    l.Stop();
                    ambientSounds.RemoveAt(i);
                    return;
                }
            }
        }

        public static void PlayLoopingMusic(AudioSource audioSource, float volume)
        {
            if (!instantiated)
            {
                Instantiate();
            }

            //Check if we have this audiosource already added

            for (int i = musics.Count - 1; i >= 0; i--)
            {
                LoopingAudioSource loopingAS = musics[i];
                if (loopingAS.audioSource == audioSource)
                {
                    musics.RemoveAt(i);
                }
                loopingAS.Stop();
            }

            LoopingAudioSource loopingAudioSource = new LoopingAudioSource(audioSource);
            float targetVolume = volume * realMusicVolume;
            loopingAudioSource.Play(targetVolume);
            musics.Add(loopingAudioSource);
        }

        private static IEnumerator CleanUpVolumeOfClip(AudioClip clip, float volume)
        {
            yield return new WaitForSeconds(clip.length);

            List<float> volumes;
            if (soundsDictionary.TryGetValue(clip, out volumes))
            {
                volumes.Remove(volume);
            }

        }

        #endregion



        #region MonoBehaviour functions

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        private void OnSceneLoad(Scene s, LoadSceneMode m)
        {
            musics.Clear();
            ambientSounds.Clear();
            soundsDictionary.Clear();
        }

        private void Update()
        {
            //Debug.Log("Music count: " + musics.Count);
            //Debug.Log("Ambient sounds count: " + ambientSounds.Count);
            //Debug.Log("Solo sounds count: " + soundsDictionary.Count);

            for (int i = 0; i < musics.Count; i++)
            {
                musics[i].Update();
            }
            for (int i = 0; i < ambientSounds.Count; i++)
            {
                ambientSounds[i].Update();
            }
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                ResumeEverything();
            }
            else
            {
                PauseEverything();
            }
        }

        public static void PauseEverything()
        {
            foreach (LoopingAudioSource m in musics)
            {
                m.Pause();
            }
            foreach (LoopingAudioSource a in ambientSounds)
            {
                a.Pause();
            }
        }

        public static void ResumeEverything()
        {
            foreach (LoopingAudioSource m in musics)
            {
                m.Resume();
            }
            foreach (LoopingAudioSource a in ambientSounds)
            {
                a.Resume();
            }
        }

        #endregion
    }



    #region LoopingAudioSourceClass

    public class LoopingAudioSource
    {
        public AudioSource audioSource;
        private float startVolume;
        private float currentVolume;
        public float initialTargetVolume;
        public float targetVolume;
        private float fadeTime;
        private float timestamp;
        public bool isStopping;
        private bool isMusic;
        private bool persist;


        public LoopingAudioSource(AudioSource audioSource, bool isMusic = true, float fadeTime = 1)
        {
            if (audioSource == null)
            {
                Debug.LogWarning("trying to create a LoopingAudioSource but no audiosource was sent");
                Application.Quit();
            }

            this.audioSource = audioSource;
            startVolume = 0;
            currentVolume = 0;
            initialTargetVolume = 1;
            targetVolume = 1;
            this.fadeTime = fadeTime;
            timestamp = 0;
            isStopping = false;
            this.isMusic = isMusic;
            persist = isMusic;

            audioSource.loop = true;
            audioSource.volume = currentVolume;
        }

        public void Play(float targetVolume = 1)
        {
            currentVolume = startVolume = (audioSource.isPlaying ? audioSource.volume : 0);
            initialTargetVolume = targetVolume;
            this.targetVolume = targetVolume;

            audioSource.volume = currentVolume;
            audioSource.loop = true;
            if (!isMusic)
            {
                audioSource.volume = currentVolume = targetVolume;
                //audioSource.PlayOneShot()
            }

            audioSource.Play();
            timestamp = 0;
            isStopping = false;
        }

        public void Stop()
        {
            if (!isMusic)
            {
                audioSource.Stop();
            }

            startVolume = currentVolume = audioSource.volume;
            targetVolume = 0;
            isStopping = true;
            timestamp = 0;
        }

        public void Pause()
        {
            audioSource.Pause();
        }

        public void Resume()
        {
            audioSource.UnPause();
        }

        public void Update()
        {
            if (!audioSource.isPlaying)
            {
                return;
            }

            if (!isMusic)
            {
                audioSource.volume = currentVolume = targetVolume;
                return;
            }

            timestamp += Time.deltaTime;
            currentVolume = Mathf.Lerp(startVolume, targetVolume, timestamp / fadeTime);
            audioSource.volume = currentVolume;

            if (currentVolume == 0 && isStopping)
            {
                audioSource.Stop();
                isStopping = false;
            }
        }
    }
    #endregion

}