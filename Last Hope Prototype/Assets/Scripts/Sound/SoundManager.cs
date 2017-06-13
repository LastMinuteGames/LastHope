using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace LastHope.SoundManager
{
    public class SoundManager : MonoBehaviour
    {
        //We make these 3 private because we will use modified properties so we add behaviour when any of these volumes is changed
        private static float globalVolume = 1f;
        private static float musicVolume = 1f;
        private static float soundVolume = 1f;
        private static float targetSoundVolume = 1.0f;
        private static float targetMusicVolume = 1.0f;

        private static bool initialized = false;
        private static GameObject soundManagerGO;
        private static SoundManager instance;

        private static int persistTag = 0;

        private static List<AudioSourceLoop> loopingMusicList = new List<AudioSourceLoop>();
        private static List<AudioSourceLoop> loopingSoundsList = new List<AudioSourceLoop>();
        private static HashSet<AudioSourceLoop> persistedMusic = new HashSet<AudioSourceLoop>();
        private static Dictionary<AudioClip, List<float>> soundsList = new Dictionary<AudioClip, List<float>>();

        public static int ParalelAudioClipsCap = 4;


        //Properties...
        public static float GlobalVolume
        {
            get { return globalVolume; }
            set
            {
                if (value != globalVolume)
                {
                    globalVolume = value;

                    //We just want the setters to update the volumes
                    musicVolume = targetMusicVolume * globalVolume;
                    soundVolume = targetSoundVolume * globalVolume;
                    UpdateLoopingMusic();
                    UpdateLoopingSounds();
                }
            }
        }
        public static float MusicVolume
        {
            get { return targetMusicVolume; }
            set
            {
                if (value != targetMusicVolume)
                {
                    targetMusicVolume = value;
                    musicVolume = targetMusicVolume * globalVolume;
                    UpdateLoopingMusic();
                }
            }
        }
        public static float SoundVolume
        {
            get { return targetSoundVolume; }
            set
            {
                if (value != targetSoundVolume)
                {
                    targetSoundVolume = value;
                    soundVolume = targetSoundVolume * globalVolume;
                    UpdateLoopingSounds();
                }
            }
        }


        private static void UpdateLoopingSounds()
        {
            foreach (AudioSourceLoop asl in loopingSoundsList)
            {
                asl.currentTargetVolume = asl.originalTargetVolume * soundVolume;
            }
        }
        private static void UpdateLoopingMusic()
        {
            foreach (AudioSourceLoop asl in loopingMusicList)
            {
                asl.currentTargetVolume = asl.originalTargetVolume * musicVolume;
            }
        }


        private static void AutoInstantiateItself()
        {
            initialized = true;
            soundManagerGO = new GameObject();
            soundManagerGO.name = "SoundManager";
            soundManagerGO.hideFlags = HideFlags.DontSave;
            instance = soundManagerGO.AddComponent<SoundManager>();
            GameObject.DontDestroyOnLoad(soundManagerGO);
        }


        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoad;
        }
        private void OnSceneLoad(Scene s, LoadSceneMode m)
        {
            persistTag++;
            StopLoopingSounds();
            StopLoopingMusics();
            soundsList.Clear();
            DeletePersistentMusic();

        }
        private void StopLoopingSounds()
        {
            for (int i = loopingSoundsList.Count-1; i >= 0; i--)
            {
                if (!loopingSoundsList[i].isPersist || !loopingSoundsList[i].audioSource.isPlaying)
                {
                    loopingSoundsList.RemoveAt(i);
                }
            }
        }
        private void StopLoopingMusics()
        {
            for (int i = loopingMusicList.Count-1; i >= 0; i--)
            {
                if (!loopingMusicList[i].isPersist || !loopingMusicList[i].audioSource.isPlaying)
                {
                    loopingMusicList.RemoveAt(i);
                }
            }
        }
        private void DeletePersistentMusic()
        {
            foreach (AudioSourceLoop asl in persistedMusic)
            {
                if (!asl.audioSource.isPlaying)
                {
                    Destroy(asl.audioSource.gameObject);
                }
            }
            persistedMusic.Clear();
        }


        private void Update()
        {
            for (int i = loopingSoundsList.Count - 1; i >= 0; i--)
            {
                if (loopingSoundsList[i].Update())
                {
                    loopingSoundsList.RemoveAt(i);
                }
            }
            for (int i = loopingMusicList.Count - 1; i >= 0; i--)
            {
                if (loopingMusicList[i].Update())
                {
                    if (loopingMusicList[i].Tag != persistTag)
                    {
                        Destroy(loopingMusicList[i].audioSource.gameObject);
                    }
                    loopingMusicList.RemoveAt(i);
                }
            }
        }


        //TODO:: add method to play looping sounds, not just looping music
        public static void PlayLoopingMusic(AudioSource audioSource, float volume = 1f, float fadeSeconds = 1f, bool persist = false)
        {
            if (!initialized)
            {
                AutoInstantiateItself();
            }

            for (int i = loopingMusicList.Count - 1; i >= 0; i--)
            {
                AudioSourceLoop asl = loopingMusicList[i];
                if (asl.audioSource == audioSource)
                {
                    loopingMusicList.RemoveAt(i);
                }
            }

            audioSource.gameObject.SetActive(true);
            AudioSourceLoop newAsl = new AudioSourceLoop(audioSource, fadeSeconds, fadeSeconds, persist);
            newAsl.Play(volume);
            newAsl.Tag = persistTag;
            loopingMusicList.Add(newAsl);

            //TODO::in theory only music could be persistant
            if (persist)
            {
                audioSource.gameObject.transform.parent = null;
                DontDestroyOnLoad(audioSource.gameObject);
                persistedMusic.Add(newAsl);
            }
        }

        //TODO:: add method to stop particular loopingSound. And another for stopping everything that loops
        public static void StopLoopingMusic(AudioSource audioSource)
        {
            foreach (AudioSourceLoop asl in loopingMusicList)
            {
                if (asl.audioSource == audioSource)
                {
                    asl.Stop();
                    audioSource = null;
                    break;
                }
            }
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }

        public static void PlaySound(AudioSource audioSource, AudioClip audioClip, float volume = 1.0f)
        {
            if (!initialized)
            {
                AutoInstantiateItself();
            }

            List<float> volumes;
            if (!soundsList.TryGetValue(audioClip, out volumes))
            {
                volumes = new List<float>();
                soundsList[audioClip] = volumes;
            }
            else if (volumes.Count == ParalelAudioClipsCap)
            {
                return;
            }

            //simple way of managic volume when we have duplicates of the same audioclip
            //not the best algorithm - could be improved
            float targetVolume = volume * soundVolume;
            if (volumes.Count > 2)
            {
                targetVolume = targetVolume / volumes.Count;
            }
            volumes.Add(targetVolume);
            audioSource.PlayOneShot(audioClip, targetVolume);
            instance.StartCoroutine(CleanVolumeFromClip(audioClip, targetVolume));
        }
        private static IEnumerator CleanVolumeFromClip(AudioClip audioClip, float volume)
        {
            yield return new WaitForSeconds(audioClip.length);

            List<float> volumes;
            if (soundsList.TryGetValue(audioClip, out volumes))
            {
                volumes.Remove(volume);
            }
        }




        //If you unselect or select the game all sound should be paused/unpaused
        private void OnApplicationFocus(bool paused)
        {
        Debug.Log("qwoufhqwofuqwhfouqwh");
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public static void Pause()
        {
            foreach (AudioSourceLoop asl in loopingMusicList)
            {
                asl.Pause();
            }
            foreach (AudioSourceLoop asl in loopingSoundsList)
            {
                asl.Pause();
            }
        }

        public static void Resume()
        {
            foreach (AudioSourceLoop asl in loopingMusicList)
            {
                asl.Resume();
            }
            foreach (AudioSourceLoop asl in loopingSoundsList)
            {
                asl.Resume();
            }
        }

        public static void Stop()
        {
            foreach (AudioSourceLoop asl in loopingMusicList)
            {
                asl.Stop();
            }
            foreach (AudioSourceLoop asl in loopingSoundsList)
            {
                asl.Stop();
            }
        }

    }





    public static class AudioSourceExtensionMethods
    {
        //play fx
        public static void PlaySound(this AudioSource audioSource, AudioClip audioClip, float volume = 1)
        {
            SoundManager.PlaySound(audioSource, audioClip, volume);
        }

        //play looping music
        public static void PlayLoopingMusic(this AudioSource audioSource, float volume = 1, float fadeDuration = 1, bool persist = true)
        {
            SoundManager.PlayLoopingMusic(audioSource, volume, fadeDuration, persist);
        }

        //play looping sound: incoming feature...


        //stop looping music
        public static void StopLoopingMusic(this AudioSource audioSource)
        {
            SoundManager.StopLoopingMusic(audioSource);
        }

        //stop looping sound: incoming feature...
    }





    public class AudioSourceLoop
    {
        public AudioSource audioSource;

        //On each audiosourceloop update we will be targetting to currentTargetvolume = originalTVolume * (musicVolume || soundVolume)
        public float currentTargetVolume;
        public float originalTargetVolume;

        //Is stopping right now? If so in upate we wont actualize the volume since it will be stopping
        public bool isStopping;

        //Should this audioSource not be deleted between scenes?
        public bool isPersist;

        //Tag used to clean up looping music that isn't playing anymore.
        public int Tag;


        private float startVolume;

        //FadeSeconds represent how long will take this audiosource to disappear or appear
        private float startFadeSeconds;
        private float stopFadeSeconds;
        private float currentFadeSeconds;

        //How long has it been since it is playing? We are gonna compare this to fadeseconds to know the situation of transitions
        private float timeSincePlay;

        private bool isPaused;

        public AudioSourceLoop(AudioSource audioS, float startFadeSeconds, float stopFadeSeconds, bool persist)
        {
            audioSource = audioS;
            if (audioS != null)
            {
                audioSource.loop = true;
                audioSource.volume = 0f;
            }

            this.startFadeSeconds = currentFadeSeconds = startFadeSeconds;
            this.stopFadeSeconds = stopFadeSeconds;
            isPersist = persist;
        }


        public void Play(float targetVolume)
        {
            if (audioSource != null)
            {
                return;
            }

            audioSource.volume = startVolume = (audioSource.isPlaying ? audioSource.volume : 0.0f);
            audioSource.loop = true;
            currentTargetVolume = targetVolume;
            originalTargetVolume = targetVolume;
            currentFadeSeconds = startFadeSeconds;
            isStopping = false;
            timeSincePlay = 0.0f;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }


        public void Stop()
        {
            if (audioSource != null && audioSource.isPlaying && !isStopping)
            {
                currentTargetVolume = 0.0f;
                isStopping = true;
                startVolume = audioSource.volume;
                currentFadeSeconds = stopFadeSeconds;
                timeSincePlay = 0.0f;
            }
        }

        public void Pause()
        {
            if (audioSource != null && !isPaused && audioSource.isPlaying)
            {
                isPaused = true;
                audioSource.Pause();
            }
        }


        public void Resume()
        {
            if (audioSource != null && isPaused)
            {
                isPaused = false;
                audioSource.UnPause();
            }
        }

        //returns whether should be updated anymore or not
        public bool Update()
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                timeSincePlay += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, currentTargetVolume, timeSincePlay / currentFadeSeconds);
                if (audioSource.volume == 0f && isStopping)
                {
                    audioSource.Stop();
                    isStopping = false;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return isPaused;
            }
        }
    }
}