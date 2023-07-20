using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    [CreateAssetMenu(fileName = "New Level Model", menuName = "Snack n'Nack/Level System/New Level")]
    public class LevelModel : ScriptableObject
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //LevelModel - (0.3)
        //State: Functional - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents an scriptable level model,
        //OBS: The scriptable object has been choosed because of his persistent data aspect. 

        [Header("Level Aspects")]
        public bool levelUnlocked = true;
        public bool levelPlayed = false;

        public bool levelIsInfinity = false;

        #region - Data Declaration -
        [Header("Level Data")]
        [Range(2,8)] public int levelGridFactor = 4;
        public string levelName;
        public int levelRate = 3;
        #endregion
    }
}