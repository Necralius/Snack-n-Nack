using NekraliusDevelopmentStudio;
using System.Collections;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
    //InputController - (0.1)
    //State: Functional - (Needs Refactoring, Needs Coments)
    //This code represents (Code functionality or code meaning)

    #region - Singleton Pattern -
    public static InputController Instance;
    void Awake() => Instance = this;
    #endregion

    #region - Main Data Declaration -
    [Header("Input Detection")]
    public LayerMask objectMask;
    public Camera mainCam => Camera.main;

    public enum GuessType { First, Second};
    [Header("Guessing System")]
    public bool firstGuessTaked = false;
    public bool secondGuessTaked = false;

    public Guess firstGuess;
    public Guess secondGuess;
    #endregion

    //----------- Methods -----------//

    #region - BuildIn Methods -
    private void Update()
    {
        GetMousePosition();
    }
    #endregion

    #region - Mouse Position Gathering -
    private void GetMousePosition()
    {
        if (GameManager.Instance.gameStarted && !GameManager.Instance.gameFinished)
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

            if (PlayerManager.Instance.gameType.Equals(GameType.CasualMode)) firstGuess.gridCell.ShowObject();            

            gridCellitem.outlineEffector.enabled = true;
            gridCellitem.isSelected = true;
        }
        else
        {
            if (!secondGuessTaked)
            {
                secondGuessTaked = true;
                secondGuess = new Guess(gridCellitem.guessID, gridCellitem);
                if (PlayerManager.Instance.gameType.Equals(GameType.CasualMode)) secondGuess.gridCell.ShowObject();

                gridCellitem.outlineEffector.enabled = true;
                gridCellitem.isSelected = true;

                StartCoroutine(CheckGuesses());
            }
        } 
    }

    #region - Guessing Check -
    private IEnumerator CheckGuesses()
    {
        if (firstGuess.CheckGuesses(secondGuess))
        {
            if (firstGuess.gridCell.isSpecial) SpecialCardView.Instance.AddCard();
            else GameManager.Instance.CalculateScore(30);
            GameManager.Instance.CalculateScore(30);



            Destroy(firstGuess.gridCell.gameObject, 1f);
            Destroy(secondGuess.gridCell.gameObject, 1f);

            RemoveItemFromGrid(firstGuess);
            RemoveItemFromGrid(secondGuess);             
        }
        else
        {
            yield return new WaitForSeconds(1f);
            firstGuess.gridCell.outlineEffector.enabled = false;
            secondGuess.gridCell.outlineEffector.enabled = false;

            if (PlayerManager.Instance.gameType.Equals(GameType.CasualMode)) firstGuess.gridCell.HideObject();
            if (PlayerManager.Instance.gameType.Equals(GameType.CasualMode)) secondGuess.gridCell.HideObject();
            GameManager.Instance.wrongGuesses++;
        }

        firstGuessTaked = false;
        secondGuessTaked = false;

        firstGuess = null;
        secondGuess = null;
    }

    private void RemoveItemFromGrid(Guess guess)
    {
        guess.gridCell.gameObject.GetComponentInChildren<Animator>().SetTrigger("Destruct");
        GridGenerator.Instance.gridDictionary.Remove(new Vector2Int(guess.guessGridAspect.posX, guess.guessGridAspect.posZ));
        GridGenerator.Instance.blocksMissing.Add(guess.guessGridAspect);
        Destroy(guess.gridCell.gameObject, 1f);
    }
    #endregion

    #endregion
}