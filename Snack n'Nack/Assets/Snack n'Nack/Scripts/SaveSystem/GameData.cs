using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    [Serializable]
    public class GameData
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //GameData - (0.1)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        public bool audioActive;

        public GameData(AudioManager audioManager)
        {
            audioActive = audioManager.audioActive;
        }
        public void SaveGame(AudioManager audioManager)
        {
            audioActive = audioManager.audioActive;
        }
    }
}