using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NekraliusDevelopmentStudio
{
    public class LevelView : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //LevelView - (0.3)
        //State: Functional - (Needs Refactoring, Needs Coments)
        //This code represents (Code functionality or code meaning)

        public LevelModel currentLevel;

        [HideInInspector] public CanvasGroup canvasGroup;

        #region - Level UI Data -
        [Header("Level UI")]
        public List<Image> rateImages;
        [HideInInspector] public TextMeshProUGUI levelName;
        public Image unlockedState;
        public Button levelBtn => GetComponent<Button>();
        #endregion

        private void Start() => levelName = GetComponentInChildren<TextMeshProUGUI>();

        private void LevelUiUpdate()
        {
            levelName.text = currentLevel.levelName;

            int index = currentLevel.levelRate;

            unlockedState.enabled = !currentLevel.levelUnlocked;
            levelBtn.enabled = currentLevel.levelUnlocked;

            foreach (Image img in rateImages)
            {
                if (!currentLevel.levelUnlocked)
                {
                    img.enabled = false;
                    continue;
                }

                if (index <= 0) img.enabled = false;
                else img.enabled = true;
                index--;
            }

            canvasGroup.alpha = currentLevel.levelUnlocked ? 1 : 0.65f;
        }
        public void StartLevel()
        {
            SceneManagement.Instance.LoadInGameSceneAsync(1);
            PlayerManager.Instance.SelectCurrentLevel(this);
        }
        //private void OnEnable()
        //{
        //    levelBtn.onClick.AddListener(StartLevel);
        //}
        private void OnValidate() => LevelUiUpdate();
        private void FixedUpdate() => LevelUiUpdate();
    }
}