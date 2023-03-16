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
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        [SerializeField] private int posX;
        [SerializeField] private int posZ;

        public bool isOccupied = false;

        [Header("Animation Settings")]
        public float rotationSpeed;
        public bool slotShowed = false;

        public void SetPosition(int posX, int posZ)
        {
            this.posX = posX;
            this.posZ = posZ;
        }
        public Vector2Int GetPosition() => new Vector2Int(posX, posZ);

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
    }
}