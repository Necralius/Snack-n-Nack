using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    public class UtilityModels : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //UtilityModels - (0.1)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents a library of custum classes or utility structures and enumerators used in the general project.
    }

    #region - Guess Model -
    [Serializable]
    public class Guess
    {
        private int GuessValue;
        public GridCell gridCell;
        public GridAspects guessGridAspect;

        public Guess(int guessValue, GridCell cellItem)
        {
            GuessValue = guessValue;
            gridCell = cellItem;
            guessGridAspect = new GridAspects(gridCell);
        }
        public bool CheckGuesses(Guess guessToCheck) => guessToCheck.GuessValue == this.GuessValue;
    }
    #endregion

    #region - GridAspects -
    [Serializable]
    public class GridAspects
    {
        public Vector3 gridCellAbsolutePosition;
        public int posX;
        public int posZ;

        public GridAspects(int posX, int posZ, Vector3 absolutePos)
        {
            this.posX = posX;
            this.posZ = posZ;
            this.gridCellAbsolutePosition = absolutePos;
        }
        public GridAspects(GridCell cell)
        {
            posX = cell.posX;
            posZ = cell.posZ;
            this.gridCellAbsolutePosition = cell.literalPosition;
        }
    }
    #endregion

    #region - Custom Dictionary -
    [Serializable]
    public class CustomDictionary
    {
        public List<Value> Values;

        #region - Own Methods -
        public void Remove(Vector2Int key) => Values.RemoveAll(e => e.T_Key.Equals(key));
        public void Add(Vector2Int Key, GameObject Value)
        {
            if (ValueExists(Key)) Debug.Log("Value cannot be added, Already exists an value related to this key in the dictionary.");
            else Values.Add(new Value(Key, Value));
        }
        public bool ValueExists(Vector2Int Key) => Values.Any(e => e.T_Key.Equals(Key));
        public bool ValueExists(GameObject Value) => Values.Any(e => e.T_Value.Equals(Value));
        public void DictionaryUpdate() => Values.ForEach(e => { if (e.T_Value.Equals(null)) this.Remove(e.T_Key); });
        public GameObject GetValue(Vector2Int keyValue)
        {
            if (ValueExists(keyValue)) return Values.Find(e => e.T_Key.Equals(keyValue)).T_Value;
            else
            {
                Debug.Log("There is no value related to this key.");
                return null;
            }
        }
        public void Clear() => Values.Clear();
        #endregion

        #region - Value Model -
        [Serializable]
        public class Value
        {
            public Vector2Int T_Key;
            public GameObject T_Value;
            public Value(Vector2Int Key, GameObject Value)
            {
                T_Key = Key;
                T_Value = Value;
            }
        }
        #endregion
    }

    #endregion

    public enum GameSate { InMenu, InGame };
    public enum GameType { CasualMode, InfinityMode };
}

/* General Objects Types ID
 * 0. Model
 * 1. Brick_Medieval
 * 2. Damaged_Concrete
 * 3. FabricSc-Fi
 * 4. Hot_Steel
 * 5. Jute_Batting
 * 6. Lath_Planks
 * 7. Medieval_Metal
 * 8. Metal_Armor
 * 9.
 * 10.
 * 11.
 */

/* Special Objects Types ID
 * 0. DestroyAll
 * 
 */