using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NekraliusDevelopmentStudio
{
    public class SpecialCardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //SpecialCardView - (0.1)
        //State: Functional - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        #region - Singleton Pattern -
        public static SpecialCardView Instance;
        private void Awake() => Instance = this;
        #endregion

        [Header("Card Visual Feedback")]
        public bool objectSelected = false;
        public float selectedSize = 1.2f;
        public float sizeTransitionSpeed = 4f;

        private Vector2 defaultSize;
        private Vector2 targetSize;

        [Header("Card UI")]
        public TextMeshProUGUI cardQuantityDisplay;

        [Header("Card Data")]
        public int cardQuantity = 0;

        [Header("On Click Events"), Space]
        public UnityEvent onClickEvent;

        private void Start()
        {
            defaultSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        }

        private void Update()
        {
            targetSize = objectSelected ? defaultSize * selectedSize : defaultSize;

            gameObject.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(gameObject.GetComponent<RectTransform>().sizeDelta, targetSize, sizeTransitionSpeed * Time.deltaTime);
        }
        public void AddCard()
        {
            cardQuantity++;
            cardQuantityDisplay.text = string.Format("{0}x", cardQuantity);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            objectSelected = true;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            objectSelected = false;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (cardQuantity > 0)
            {
                cardQuantity--;
                onClickEvent.Invoke();
            }
        }
    }
}