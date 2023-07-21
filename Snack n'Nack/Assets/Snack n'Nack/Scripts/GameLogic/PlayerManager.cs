using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NekraliusDevelopmentStudio
{
    public class PlayerManager : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //PlayerManager - (0.1)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        #region - Singleton Pattern -
        public static PlayerManager Instance;
        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        #region - Game Mode Manegment -
        [Header("Game Mode")]
        public GameType gameType = GameType.CasualMode;
        #endregion

        #region - Main Data Declaration -
        public PlayerData playerData;
        public LevelModel currentLevel;
        public GameSate gameState;

        public List<LevelModel> allLevels = new List<LevelModel>();

        public LevelModel infinityLevelModel;
        #endregion

        #region - Game Level Management -
        
        #endregion

        public void UnlockNextLevel()
        {
            int currentLevelIndex = 0;
            for (int i = 0; i < allLevels.Count; i++)
            {
                if (allLevels[i].Equals(currentLevel))
                {
                    currentLevelIndex = i;
                    break;
                }
            }

            if (allLevels[currentLevelIndex + 1] && (currentLevelIndex + 1) <= allLevels.Count)
            {
                if (!allLevels[currentLevelIndex + 1].levelUnlocked) allLevels[currentLevelIndex + 1].levelUnlocked = true;
            }
        }
        public void ResetAllLevels()
        {
            foreach (LevelModel level in allLevels)
            {
                level.levelUnlocked = false;
                level.levelPlayed = false;
            }
        }

        public void SelectCurrentLevel(LevelView level) => currentLevel = level.currentLevel;

        private void Update()
        {
            if (gameState.Equals(GameSate.InGame))
            {
                if (SceneManager.GetActiveScene().buildIndex == 1 && !GridGenerator.Instance.generatedSeted)
                {
                    GridGenerator.Instance.GenerateGridInGameAction(currentLevel.levelGridFactor);
                }
                if (SceneManager.GetActiveScene().buildIndex == 2 && !GridGenerator.Instance.generatedSeted)
                {
                    GridGenerator.Instance.GenerateGridInGameAction(infinityLevelModel.levelGridFactor);
                }
            }
        }
    }
}