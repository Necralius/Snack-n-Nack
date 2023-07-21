using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    [CreateAssetMenu(fileName = "New Audio Database File", menuName = "Snack n'Nack/Audio System/New Audio Database")]
    public class AudioDatabaseModel : ScriptableObject
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //AudioDatabaseModel - (Code Version)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        public List<AudioFile> AudioFileContents = new List<AudioFile>();

        public AudioClip GetClip(string ClipName) => AudioFileContents.First(clip => clip.FileName == ClipName).ClipContent;

        public void GetAndPlayEffectClip(string ClipName) => AudioManager.Instance.PlayAudioClipWithVariation(GetClip(ClipName));
        public void GetAndPlayMusicClip(string ClipName) => AudioManager.Instance.PlayMusic(GetClip(ClipName));

    }

    [Serializable]
    public struct AudioFile
    {
        public string FileName;
        public AudioClip ClipContent;
    }
}