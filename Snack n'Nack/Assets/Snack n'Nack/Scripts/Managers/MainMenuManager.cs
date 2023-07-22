using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    public class MainMenuManager : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //MainMenuManager - (0.1)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        LevelView currentView;

        public void InfinityMode() => ConfirmationDialogue.Instance.SetUpAction("Confirm New Game", "Really want to start the infinity mode?", StartInfinityMode);
        public void StartCasualMode(LevelView view)
        {
            currentView = view;

            ConfirmationDialogue.Instance.SetUpAction("Confirm Level", "Really want to start the level: " + view.currentLevel.levelName + "?", StartCasualMode);
        }

        private string StartInfinityMode(bool confirmed)
        {
            SceneManagement.Instance.LoadInGameSceneAsync(2);
            return "Not Implemented";
        }

        private string StartCasualMode(bool confirmed)
        {
            currentView.StartLevel();
            return "Not Implemented";
        }
    }
}