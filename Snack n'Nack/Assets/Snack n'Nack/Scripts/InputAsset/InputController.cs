using NekraliusDevelopmentStudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NekraliusDevelopmentStudio
{
    public class InputController : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //InputController - (Code Version)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        #region - Singleton Pattern -
        public static InputController Instance;
        void Awake() => Instance = this;
        #endregion

        [Header("Input Detection")]
        public LayerMask objectMask;
        public Camera mainCam => Camera.main;

        //[Header("Guessing System")]
        public enum GuessType { First, Second};
        public bool firstGuessTaked = false;
        public bool secondGuessTaked = false;

        public Guess firstGuess;
        public Guess secondGuess;

        #region - BuildIn Methods -
        private void Update()
        {
            GetMousePosition();
        }
        #endregion

        #region - Mouse Position Gathering -
        private void GetMousePosition()
        {
            if (GameManager.Instance.gameStarted)
            {
                Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, objectMask) && hit.transform.GetComponent<GridCell>())
                {
                    if (Input.GetMouseButtonDown(0) && hit.transform.GetComponent<GridCell>()) TakeGuess(hit.transform.GetComponent<GridCell>());
                }
            }
        }
        #endregion

        #region - Guessing System -
        private void TakeGuess(GridCell gridCellitem)
        {
            if (!firstGuessTaked)
            {
                firstGuessTaked = true;
                firstGuess = new Guess(gridCellitem.guessID, gridCellitem);
                gridCellitem.outlineEffector.enabled = true;
                gridCellitem.isSelected = true;
            }
            else
            {
                if (!secondGuessTaked)
                {
                    secondGuessTaked = true;
                    secondGuess = new Guess(gridCellitem.guessID, gridCellitem);
                    gridCellitem.outlineEffector.enabled = true;
                    gridCellitem.isSelected = true;
                    StartCoroutine(CheckGuesses());
                }
            } 
        }
        private IEnumerator CheckGuesses()
        {
            if (firstGuess.CheckGuesses(secondGuess))
            {
                GameManager.Instance.CalculateScore();
                Destroy(firstGuess.gridCell.gameObject, 0.2f);
                Destroy(secondGuess.gridCell.gameObject, 0.2f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                firstGuess.gridCell.outlineEffector.enabled = false;
                secondGuess.gridCell.outlineEffector.enabled = false;
            }

            firstGuessTaked = false;
            secondGuessTaked = false;

            firstGuess = null;
            secondGuess = null;
        }
        #endregion
    }
}

#region - Guess Model -
public class Guess
{
    private int GuessValue;
    public GridCell gridCell;
    public Guess(int guessValue, GridCell cellItem)
    {
        GuessValue = guessValue;
        gridCell = cellItem;
    }
    public bool CheckGuesses(Guess guessToCheck) => guessToCheck.GuessValue == this.GuessValue;
}
#endregion