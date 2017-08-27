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
            Vector3 position = Vector3.zero;
            if (CheckAudioSourceAndClip(audioSource))
            {
                SoundManager2.PlaySound(audioSource, audioSource.clip, volume, position, false);
            }
        }
        public static void PlaySound(this AudioSource audioSource, Vector3 position, float volume = 1)
        {
            if (CheckAudioSourceAndClip(audioSource))
            {
                SoundManager2.PlaySound(audioSource, audioSource.clip, volume, position, true);
            }
        }
        public static void PlayLoopingSound(this AudioSource audioSource, float volume = 1)
        {
            Vector3 position = Vector3.zero;
            if (CheckAudioSourceAndClip(audioSource))
            {
                SoundManager2.PlayLoopingSound(audioSource, audioSource.clip, volume, position, false);
            }
        }
        public static void PlayLoopingSound(this AudioSource audioSource, Vector3 position, float volume = 1)
        {
            if (CheckAudioSourceAndClip(audioSource))
            {
                SoundManager2.PlayLoopingSound(audioSource, audioSource.clip, volume, position, true);
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
            foreach (LoopingAudioSource m in musics)
            {
                m.UpdateVolume(realMusicVolume);
            }
        }

        private static void UpdateAmbientVolume()
        {
            foreach (LoopingAudioSource a in ambientSounds)
            {
                a.UpdateVolume(realSoundVolume);
            }
        }
        #endregion


        private static List<LoopingAudioSource> musics = new List<LoopingAudioSource>();
        private static List<LoopingAudioSource> ambientSounds = new List<LoopingAudioSource>();
        private static Dictionary<AudioClip, List<float>> soundsDictionary = new Dictionary<AudioClip, List<float>>();
        private static int maxDuplicateAudioClips = 5;
        private static int persistTag = 0;


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

        public static void PlaySound(AudioSource audioSource, AudioClip audioClip, float volume, Vector3 position, bool is3D)
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

            if (is3D)
            {
                AudioSource.PlayClipAtPoint(audioClip, position, targetVolume);
            }
            else
            {
                audioSource.PlayOneShot(audioClip, targetVolume);
            }

            instance.StartCoroutine(CleanUpVolumeOfClip(audioClip, targetVolume));
        }

        public static void PlayLoopingSound(AudioSource audioSource, AudioClip audioClip, float volume, Vector3 position, bool is3D)
        {
            if (!instantiated)
            {
                Instantiate();
            }

            //Debug.Log("Ambient sounds:" + ambientSounds.Count);

            for (int i = ambientSounds.Count - 1; i >= 0; i--)
            {
                LoopingAudioSource loopingAS = ambientSounds[i];
                if (loopingAS.audioSource == audioSource)
                {
                    ambientSounds.RemoveAt(i);
                }
                loopingAS.Stop();
            }


            LoopingAudioSource loopingAudioSource = new LoopingAudioSource(audioSource, false, 1.0f, is3D);
            float targetVolume = volume * realSoundVolume;

            if (is3D)
            {
                loopingAudioSource.audioSource.gameObject.transform.position = position;
                //enable 3D settings
                //Debug.Log("enable 3D settings!");
                loopingAudioSource.audioSource.spatialBlend = 1;
            }
            
            loopingAudioSource.Play(targetVolume);
            ambientSounds.Add(loopingAudioSource);

        }

        public static void StopLoopingSound(AudioSource audioSource)
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

            //Debug.Log("Music count:" + musics.Count);

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
            loopingAudioSource.persistTag = persistTag;
            loopingAudioSource.Play(targetVolume);

            audioSource.gameObject.transform.parent = null;
            GameObject.DontDestroyOnLoad(audioSource.gameObject);

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
            persistTag++;

            //musics.Clear();
            ClearMusicsOnSceneLoad();
            ambientSounds.Clear();
            soundsDictionary.Clear();
        }

        private void ClearMusicsOnSceneLoad()
        {
            for (int i = musics.Count-1; i >= 0; i--)
            {
                if (!musics[i].audioSource.isPlaying)
                {
                    GameObject.Destroy(musics[i].audioSource.gameObject);
                    musics.RemoveAt(i);
                }
            }
        }

        private void Update()
        {
            //Debug.Log("Music count: " + musics.Count);
            //Debug.Log("Ambient sounds count: " + ambientSounds.Count);
            //Debug.Log("Solo sounds count: " + soundsDictionary.Count);

            foreach (LoopingAudioSource l in musics)
            {
                l.Update();
            }
            foreach (LoopingAudioSource a in ambientSounds)
            {
                a.Update();
            }

            for (int i = musics.Count-1; i >= 0; i--)
            {
                if (musics[i].persistTag != persistTag)
                {
                    //Debug.Log(musics[i].persistTag);
                    //Debug.Log(persistTag);
                    if (!musics[i].audioSource.isPlaying)
                    {
                        GameObject.Destroy(musics[i].audioSource.gameObject);
                        musics.RemoveAt(i);
                    }
                }
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
        public int persistTag;

        private float startVolume;
        private float currentVolume;
        private float initialTargetVolume;
        private float targetVolume;
        private float fadeTime;
        private float timestamp;
        private bool isStopping;
        private bool isMusic;
        private bool persist;
        private bool is3D;


        public LoopingAudioSource(AudioSource audioSource, bool isMusic = true, float fadeTime = 1, bool is3D = false)
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
            this.is3D = is3D;

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
            //only ambient sounds can be 3D. Music is only bso
            if (!isMusic)
            {
                audioSource.Stop();
                if (is3D)
                {
                    audioSource.gameObject.transform.position = Vector3.zero;
                    //Debug.Log("Disable 3D settings");
                    audioSource.spatialBlend = 0;
                }
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

        public void UpdateVolume(float realVolume)
        {
            if (isMusic)
            {
                if (!isStopping)
                {
                    targetVolume = initialTargetVolume * realVolume;
                }
            }
            else
            {
                targetVolume = initialTargetVolume * realVolume;
            }
        }
    }
    #endregion

}