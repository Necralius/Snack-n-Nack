using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NekraliusDevelopmentStudio
{
    public class GameManager : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //GameManager - (0.1)
        //State: Functional - (Needs Refactoring, Needs Coments)
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

        [Header("Game State")]
        public bool gameStarted = false;
        public bool gameFinished = false;
        #endregion

        #region - Game Mode Manegment -
        [Header("Game Mode")]
        public GameType gameType = GameType.CasualMode;
        public enum GameType {CasualMode, InfinityMode};
        #endregion

        #region - Score Value -
        [Header("Score")]
        public int scoreValue;
        #endregion

        #region - Game Finish Data -
        [Header("Game Finish System")]
        public GameObject finishScreen;
        public TextMeshProUGUI finalTime;
        public TextMeshProUGUI finalScore;

        #endregion

        //----------- Methods -----------//

        #region - Build In Methods -
        private void Update()
        {
            CounterManegment();

            if (gameTime >= maxGameTime || gridGenerator.gameGridContent.Count <= 0) FinishGame();
        }
        private void OnEnable()
        {
            finishScreen.SetActive(false);
        }
        #endregion

        #region - Score Calculation -
        public void CalculateScore()
        {
            scoreValue += 200;
            scoreText.text = scoreValue.ToString();
        }
        #endregion

        #region - Timer Counter Manegment -
        private void CounterManegment()
        {
            if (gameFinished) return;
            else if (!gameFinished) finishScreen.SetActive(false);

            if (gameType.Equals(GameType.CasualMode))
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
            else if (gameType.Equals(GameType.InfinityMode))
            {

            }
        }
        #endregion

        #region - Game Finish System -
        private void FinishGame()
        {
            gameFinished = true;
            
            finalTime.text = string.Format("Your Time: {0:00.00}", gameTime);
            finalScore.text = string.Format("Your Score: {0}", scoreValue);

            finishScreen.SetActive(true);
        }
        #endregion
    }
}