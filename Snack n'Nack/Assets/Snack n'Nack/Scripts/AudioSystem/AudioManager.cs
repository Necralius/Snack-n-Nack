using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    public class AudioManager : MonoBehaviour, IPersistentData
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //AudioManager - (0.2)
        //State: Functional - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        #region - Singleton Pattern -
        public static AudioManager Instance;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (Instance != null) Destroy(Instance);
            Instance = this;
        }
        #endregion

        #region - Audio Sources -
        public AudioSource mainSource => GetComponent<AudioSource>();
        #endregion

        #region - Audio Data -
        public AudioDatabaseModel audioDatabase;
        public bool audioActive = true;
        #endregion

        #region - Audio Manegement Methods -
        public void PlayAudioClipWithVariation(AudioClip clipToPlay)
        {
            mainSource.volume = Random.Range(0.85f, 1f);
            mainSource.pitch = Random.Range(0.9f, 1f);
            mainSource.PlayOneShot(clipToPlay);
        }
        public void PlayMusic(AudioClip musicToPlay) => mainSource.PlayOneShot(musicToPlay);
        public void StopMusic() => mainSource.Stop();

        public void LoadData(GameData data)
        {
            audioActive = data.audioActive;

            mainSource.enabled = audioActive;
            mainSource.enabled = audioActive;
        }
        #endregion
    }
}