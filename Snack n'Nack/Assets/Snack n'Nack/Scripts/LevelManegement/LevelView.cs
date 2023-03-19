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
        //LevelView - (Code Version)
        //State - (Needs Refactoring, Needs Coments)
        //This code represents (Code functionality or code meaning)

        public LevelModel currentLevel;
        
        private CanvasGroup canvasGroup;

        #region - Level UI Data -
        [Header("Level UI")]
        public List<Image> rateImages;
        public TextMeshProUGUI levelName;
        #endregion

        private void LevelUiUpdate()
        {
            levelName.text = currentLevel.levelName;

            int index = currentLevel.levelRate;
            foreach (Image img in rateImages)
            {
                if (index <= 0) img.enabled = false;
                else img.enabled = true;
                index--;
            }
        }
        private void Awake() => canvasGroup = GetComponent<CanvasGroup>();
        private void OnValidate()
        {
            LevelUiUpdate();
            if (GetComponent<CanvasGroup>()) return;
            else gameObject.AddComponent<CanvasGroup>();

            canvasGroup = GetComponent<CanvasGroup>();

            canvasGroup.alpha = currentLevel.levelUnlocked ? 1 : 0.65f;
        }
        private void FixedUpdate()
        {
            LevelUiUpdate();
        }
    }
}