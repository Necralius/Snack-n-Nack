using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

        #region - Dependencies -
        private GridGenerator gridGenerator => GridGenerator.Instance;
        #endregion

        #region - Pause System -
        [Header("Game Pause")]
        public GameObject pauseMenu;
        public bool isPaused;
        #endregion

        #region - Combo System -
        [Header("Combo System")]
        public int comboQuantity = 0;
        public float comboMultiplier = 1.0f;
        public TextMeshProUGUI comboText;
        private Animator comboAnimator => comboText.GetComponent<Animator>();

        [SerializeField] private float comboTime = 3;
        private float currentComboTimer = 0;
        #endregion

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
        private bool savedGame = false;
        #endregion

        #region - Score Value -
        [Header("Score")]
        public int currentScoreValue;
        public int targetScore;
        #endregion

        #region - Game Finish Data -
        [Header("Game Finish System")]
        public GameObject finishScreen;
        public GameObject objectsToHide;
        public TextMeshProUGUI finalTime;
        public TextMeshProUGUI finalScore;
        #endregion

        #region - Infinity Mode -
        [SerializeField] private TextMeshProUGUI wrongGuessesText;
        public int wrongGuesses = 0;
        public int maxWrongGuesses = 3;
        #endregion

        //----------- Methods -----------//

        #region - Build In Methods -  
        private void Start() => savedGame = false;
        private void OnEnable() { finishScreen.SetActive(false); objectsToHide.SetActive(true); }
        private void Update()
        {
            CounterManegment();
            ComboCounter();
            if (gameFinished) FinishGame();
            comboText.gameObject.SetActive(comboQuantity >= 2);

            currentScoreValue = Mathf.FloorToInt(Mathf.Lerp(currentScoreValue, targetScore, 10 * Time.deltaTime));
            scoreText.text = $"Score: {currentScoreValue}";
            if (PlayerManager.Instance.gameType.Equals(GameType.InfinityMode)) wrongGuessesText.text = $"Wrong Guesses: {wrongGuesses} from {maxWrongGuesses}";
        }
        #endregion

        #region - Score Calculation -
        public void CalculateScore(int value)
        {
            targetScore += (int)(value * comboMultiplier);
            gridGenerator.RandomObjectPossibilityAddBehavior();
        }
        #endregion

        #region - Timer Counter Manegment -
        private void CounterManegment()
        {
            if (gameFinished) return;
            else if (!gameFinished) finishScreen.SetActive(false);

            if (PlayerManager.Instance.gameType.Equals(GameType.CasualMode))
            {
                if (!gridGenerator.generationFinished)
                {
                    gameStarted = false;
                    gameFinished = false;
                    gameTime = 0;
                }
                if (!gameStarted && gridGenerator.generationFinished)
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
                if (gridGenerator.gameGridContent.Count <= 0)
                {
                    Debug.Log("Game finished!");
                    gameFinished = true;
                }
            }
            else if (PlayerManager.Instance.gameType.Equals(GameType.InfinityMode))
            {
                if (!gridGenerator.generationFinished)
                {
                    gameStarted = false;
                    gameFinished = false;
                    gameTime = 0;
                }
                else if (gridGenerator.generationFinished) gameStarted = true;
                if (gameStarted)
                {
                    if (!gameFinished) gameTime += Time.deltaTime;
                    if (wrongGuesses >= maxWrongGuesses) gameFinished = true;
                    timerText.text = string.Format("{0:00.00}", gameTime);
                }
            }
        }
        #endregion

        #region - Game Finish System -
        private void FinishGame()
        {
            gameFinished = true;
            Debug.Log("Game Finished!");
            
            finalTime.text = string.Format("{0:00.00}", gameTime);
            finalScore.text = $"{currentScoreValue}";

            if (!savedGame)
            {
                if (PlayerManager.Instance.gameType.Equals(GameType.CasualMode))
                {
                    PlayerManager.Instance.UnlockNextLevel();
                    PlayerManager.Instance.currentLevel.levelPlayed = true;
                }

                PlayerManager.Instance.playerData.AddScore(currentScoreValue);
                PlayerManager.Instance.playerData.SaveScore();
                savedGame = true;
            }

            finishScreen.SetActive(true);
            objectsToHide.SetActive(false);
        }
        #endregion

        #region - Pause Game -
        public void SettingInteraction()
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            isPaused = pauseMenu.activeInHierarchy;

            Time.timeScale = isPaused ? 0f : 1f;
        }
        #endregion

        #region - Retry Level Action -
        public void RetryLevelAction() => ConfirmationDialogue.Instance.SetUpAction("Confirm Retry", "Really want to repeat the level?", ReloadLevel);
        #endregion

        #region - Return To Menu Action -
        public void ReturnToMenu() => ConfirmationDialogue.Instance.SetUpAction("Confirm Return", "Really want to return to main menu?", ReturnToMenu);
        #endregion

        #region - Combo System -
        public void AddCombo()
        {
            comboQuantity++;
            
            if (comboQuantity < 2) return;
            
            comboMultiplier += 0.1f;
            currentComboTimer = comboTime;
            comboAnimator.SetTrigger("AddCombo");
            comboText.text = $"Combo {comboQuantity}X";
        }
        public void ResetCombo()
        {
            comboQuantity = 0;
            comboText.text = $"Combo {comboQuantity}X";
            comboText.gameObject.SetActive(false);
        }
        private void ComboCounter()
        {
            if (comboQuantity >= 2)
            {
                if (currentComboTimer <= 0) ResetCombo();
                else currentComboTimer -= Time.deltaTime;
            }
        }
        #endregion


        string ReloadLevel(bool confirmed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return "Not Implemented";
        }

        string ReturnToMenu(bool confirmed)
        {
            SceneManager.LoadScene(0);
            return "Not Implemented";
        }
    }
}