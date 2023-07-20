using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    public class AudioInGameManager : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //AudioInGameManager - (Code Version)
        //State: Functional - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        #region - Singleton Pattern -
        public static AudioInGameManager Instance;
        private void Awake() => Instance = this;
        #endregion

        #region - Audio Sources -
        public AudioSource effectsSource;
        public AudioSource musicSource;
        #endregion

        #region - Audio Data -
        public AudioDatabaseModel audioDatabase;
        #endregion

        #region - Audio Manegement Methods -
        public void PlayAudioClipWithVariation(AudioClip clipToPlay)
        {
            effectsSource.volume = Random.Range(0.85f, 1f);
            effectsSource.pitch = Random.Range(0.9f, 1f);
            effectsSource.PlayOneShot(clipToPlay);
        }
        public void PlayMusic(AudioClip musicToPlay) => musicSource.PlayOneShot(musicToPlay);
        public void StopMusic() => musicSource.Stop();
        #endregion

        #region - BuildIn Methods -

        #endregion
    }
}