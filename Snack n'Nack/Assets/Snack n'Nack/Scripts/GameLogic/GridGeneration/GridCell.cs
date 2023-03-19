using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace NekraliusDevelopmentStudio
{
    public class GridCell : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //GridCell - (Code Version)
        //State: Functional - (Needs Refactoring, Needs Coments)
        //This code represents (Code functionality or code meaning)

        #region - Position Data -
        [SerializeField] private int posX;
        [SerializeField] private int posZ;
        #endregion

        #region - Visual Data -
        public bool isSelected = false;

        [Header("Animation Settings")]
        public float rotationSpeed;
        public bool slotShowed = false;

        [HideInInspector] public Outline outlineEffector;
        #endregion

        #region - Indentificator Data -
        public int guessID;
        #endregion

        //----------- Methods -----------//

        #region - Build In Methods -
        private void Start() => outlineEffector = GetComponent<Outline>();
        #endregion

        #region - Position Management -
        public void SetPosition(int posX, int posZ)
        {
            this.posX = posX;
            this.posZ = posZ;
        }
        #endregion

        #region - Object Animation Managment -
        public void ShowObject()
        {
            transform.DORotate(new Vector3(180, 0, 0), rotationSpeed * 0.5f).SetEase(Ease.Linear);
            slotShowed = true;
        }
        public void HideObject()
        {
            transform.DORotate(new Vector3(0, 0, 0), rotationSpeed * 0.5f).SetEase(Ease.Linear);
            slotShowed = false;
        }
        #endregion
    }
}