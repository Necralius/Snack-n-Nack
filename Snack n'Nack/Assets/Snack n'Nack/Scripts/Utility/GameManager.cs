using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NekraliusDevelopmentStudio
{
    public class GameManager : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //GameManager - (Code Version)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality ou code meaning)

        #region - Singleton Pattern -
        public static GameManager Instance;
        private void Awake() => Instance = this;
        #endregion

        private GridGenerator gridGenerator => GridGenerator.Instance;

        #region - UI Update -
        [Header("UI Items")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI timerText;
        #endregion

        #region - Counter System -
        [Header("Counter System")]
        public float gameTime;
        public float maxGameTime = 20f;
        public float timeToStartGame = 3f;
        public bool gameStarted = false;
        public bool gameFinished = false;
        #endregion

        #region - Game Mode Manegment -
        public bool casualMode = false;
        public bool infinityMode = true;
        #endregion

        #region - Scene Managment -
        public void LoadSceneAsync(int SceneIndex) => SceneManager.LoadSceneAsync(SceneIndex);//This method loads an scene using an scene index as argument
        public void LoadSceneAsync(string SceneName) => SceneManager.LoadSceneAsync(SceneName);//This method loads an scene using the scene name as argument
        public void ReloadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//This method reload the current scene
        #endregion

        #region - Score Value -
        public int scoreValue;
        #endregion

        private void Update()
        {
            CounterManegment();
        }
        public void CalculateScore()
        {
            scoreValue += 200;
            scoreText.text = scoreValue.ToString();
        }
        private void CounterManegment()
        {
            if (casualMode)
            {
                if (!GridGenerator.Instance.generationFinished)
                {
                    gameStarted = false;
                    gameFinished = false;
                    gameTime = 0;
                }
                if (!gameStarted && GridGenerator.Instance.generationFinished)
                {
                    gridGenerator.ShowAllCells();

                    gameTime += Time.deltaTime;
                    if (gameTime >= timeToStartGame)
                    {
                        gridGenerator.HideAllCells();

                        gameStarted = true;
                        gameTime = 0;
                    }
                    timerText.text = string.Format("{0:00.00}/{1:00.00}", gameTime, timeToStartGame);
                }
                else if (gameStarted)
                {
                    if (gameTime < maxGameTime) gameTime += Time.deltaTime;
                    else if (gameTime >= maxGameTime)
                    {
                        gameTime = maxGameTime;
                        gameFinished = true;
                    }
                    timerText.text = string.Format("{0:00.00}/{1:00.00}", gameTime, maxGameTime);
                }
            }
            
        }
    }
}