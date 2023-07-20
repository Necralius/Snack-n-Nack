using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NekraliusDevelopmentStudio
{
    public class ConfirmationDialogue : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //ConfirmationDialogue - (Code Version)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        #region - Singleton Pattern -
        public static ConfirmationDialogue Instance;
        private void Awake() => Instance = this;
        #endregion

        private GameObject dialogObject => transform.GetChild(0).gameObject;

        public TextMeshProUGUI textLabel;
        public TextMeshProUGUI textContent;

        public Button yesButton;
        public Button noButton;

        public bool completeAction = false;

        Func<bool, string> methodToCall;

        public void SetUpAction(string dialogueLabel, string dialogueContent, Func<bool, string> method)
        {
            dialogObject.SetActive(true);
            yesButton.onClick.AddListener(delegate { YesDialogue(); });
            noButton.onClick.AddListener(delegate { NoDialogue(); });

            textLabel.text = dialogueLabel;
            textContent.text = dialogueContent;
            methodToCall = method;
        }

        private void YesDialogue()
        {
            methodToCall(true);
            dialogObject.SetActive(false);
        }
        private void NoDialogue()
        {
            dialogObject.SetActive(false);
        }
    }
}