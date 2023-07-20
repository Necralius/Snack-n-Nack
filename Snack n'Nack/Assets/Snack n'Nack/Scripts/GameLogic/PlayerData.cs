using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Snack n'Nack/Player Data/New Player Data")]
    public class PlayerData : ScriptableObject
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //CompleteCodeName - (Code Version)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        public int totalScore;

        public void AddScore(int scoreToAdd) => totalScore += scoreToAdd;
        public void SaveScore() => PlayerPrefs.SetInt("TotalScore", totalScore);
        public void DeleteSave()
        {
            if (PlayerPrefs.HasKey("TotalScore")) PlayerPrefs.DeleteKey("TotalScore");
        }
        public string GetScore() => totalScore.ToString();

        public void ResetScore() => totalScore = 0;

        private void OnEnable()
        {
            if (PlayerPrefs.HasKey("TotalScore")) totalScore = PlayerPrefs.GetInt("TotalScore");
            else totalScore = 0;
        }
    }
}