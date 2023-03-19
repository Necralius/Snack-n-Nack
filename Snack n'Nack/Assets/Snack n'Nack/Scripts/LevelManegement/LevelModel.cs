using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    [CreateAssetMenu(fileName = "New Level Model", menuName = "Snack n'Nack/Level System/New Level")]
    public class LevelModel : ScriptableObject
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //CompleteCodeName - (Code Version)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        public bool levelUnlocked = true;

        #region - Data Declaration -
        [Header("Level Data")]
        public int levelGridFactor = 4;
        public string levelName;
        public int levelRate = 3;
        #endregion

        private bool VerifiesIfUnlocked()
        {
            //Search Json saves
            return false;
        }
    }
}