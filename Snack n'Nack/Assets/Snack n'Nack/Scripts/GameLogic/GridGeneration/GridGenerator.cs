using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Security.Cryptography;
using Unity.VisualScripting;

namespace NekraliusDevelopmentStudio
{
    public class GridGenerator : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //GridGenerator - (0.1)
        //State: Functional - (Needs Refactoring, Needs Coments)
        //This code represents (Code functionality ou code meaning)

        #region - Singleton Pattern -
        public static GridGenerator Instance;
        private void Awake() => Instance = this;
        #endregion

        #region - Grid Data -
        [Header("Grid Data")]
        [SerializeField, Range(2, 10)] private int height = 10;
        [SerializeField, Range(2,10)] private int width = 10;
        [SerializeField] private float gridSpaceSize = 5f;
        [SerializeField] private float timeBetwenCellSpawn = 0.5f;
        [SerializeField] private GameObject gridCellPrefab;
        private GameObject[,] gameGrid;
        public List<GameObject> gameGridContent;
        #endregion

        #region - Generation State -
        public bool generationFinished = false;
        #endregion

        #region - Camera Adjustment Data-
        Vector3 centerCoordinate;
        [Header("Camera Adjustment Data")]
        public GameObject centerTarget;
        public float cameraTravelSpeed = 3f;
        public int newMatrixFactor = 3;
        #endregion

        #region - Diferent Cubs Spawn -
        [Header("Cell Puzzles Types")]
        public GameObject[] cellTypesPrefabs;
        public GameObject selectedObject;

        private List<GameObject> pairs;
        [Header("All Puzzles and Positions")]
        public SerializableDictionary<Vector2, GameObject> gridDictionary;
        #endregion

        //----------- Methods -----------//

        #region - BuildIn Methods -
        private void Start()
        {
            centerCoordinate = centerTarget.transform.position;
            GenerateGridInGameAction();
        }
        private void Update()
        {
            centerTarget.transform.localPosition = Vector3.Lerp(centerTarget.transform.localPosition, CalculateGridCenter(), cameraTravelSpeed * Time.deltaTime);
        }
        private void FixedUpdate() => ManageCellContent();
        #endregion

        #region - Cell Animation Management -
        public void ShowAllCells()//This method get all the game cells content and call his show animation
        {
            foreach(GameObject obj in gameGridContent) obj.GetComponent<GridCell>().ShowObject();
        }
        public void HideAllCells()//this method get all the game cells content and call his hide animation
        {
            foreach (GameObject obj in gameGridContent) obj.GetComponent<GridCell>().HideObject();
        }
        #endregion

        #region - Grid Generation -

        #region - Grid Generator In Game -
        public void GenerateGridInGameAction() => StartCoroutine(GenerateGridInGame());
        public IEnumerator GenerateGridInGame()
        {
            //This method generates an bidimentional array to produce the game grid

            generationFinished = false;

            //The below statements verified some data to make sure that the system cannot break within the game execution
            if (height != width)
            {
                Debug.LogWarning("The height and width numbers must be equals!");
                yield return null;
            }

            if (gridCellPrefab == null)
            {
                Debug.LogWarning("Please assing a valid grid cell prefab!");
                yield return null;
            }

            //The below statement verifies if exists an previous grid data, and if there is, the statement destroy all the previus grid data
            if (!(gameGridContent.Equals(null))) DestroyPreviusGrid();

            gameGrid = new GameObject[height, width];
            gameGridContent = new List<GameObject>(height * width);

            pairs = GeneratePairs();
            int index = 0;

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    selectedObject = pairs[index];
                    GameObject instatiatedObj = Instantiate(selectedObject, new Vector3(transform.position.x + x * gridSpaceSize, transform.position.y, transform.position.z + z * gridSpaceSize), Quaternion.identity, transform);
                    
                    gridDictionary.Add(new Vector2(x, z), instatiatedObj);
                    gridDictionary[new Vector2(x,z)].GetComponent<GridCell>().SetPosition(x, z);
                    gridDictionary[new Vector2(x,z)].GetComponent<GridCell>().gameObject.name = string.Format("Grid Item [{0},{1}]", x, z);

                    gameGridContent.Add(instatiatedObj);

                    instatiatedObj = null;
                    index++;

                    yield return new WaitForSeconds(timeBetwenCellSpawn);
                }
            }
            generationFinished = true;
        }
        public List<GameObject> GeneratePairs()
        {
            /* This method get all the puzzles types and generates pairs possibilities based on the grid puzzle quantity need,
            later the method shufle the list two times and returns itself. */
            List<GameObject> generatedPairs = new List<GameObject>();

            int looper = height * height;
            int index = 0;

            for (int i = 0; i < looper; i++)
            {
                if (index == looper / 2) index = 0;
                generatedPairs.Add(cellTypesPrefabs[index]);
                index++;
            }

            System.Random random = new System.Random();//This statement makes the method use only the System Random library for this especific purpouse.

            generatedPairs = generatedPairs.OrderBy(e => random.Next()).ToList();
            generatedPairs = generatedPairs.OrderBy(e => random.Next()).ToList();
            return generatedPairs;
        }
        public void GenerateGridInGameAction(int matrixFactor) => StartCoroutine(GenerateGridInGame(matrixFactor));
        public IEnumerator GenerateGridInGame(int newMatrixFactor)
        {
            generationFinished = false;

            height = newMatrixFactor;
            width = newMatrixFactor;

            if (height != width)
            {
                Debug.LogWarning("The height and width numbers must be equals!");
                yield return null;
            }

            if (gridCellPrefab == null)
            {
                Debug.LogWarning("Please assing a valid grid cell prefab!");
                yield return null;
            }

            if (!(gameGridContent.Equals(null))) DestroyPreviusGrid();

            gameGrid = new GameObject[height, width];
            gameGridContent = new List<GameObject>(height * width);

            GeneratePairs();
            int index = 0;

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    selectedObject = pairs[index];
                    GameObject instatiatedObj = Instantiate(selectedObject, new Vector3(transform.position.x + x * gridSpaceSize, transform.position.y, transform.position.z + z * gridSpaceSize), Quaternion.identity, transform);

                    gridDictionary.Add(new Vector2(x, z), instatiatedObj);

                    gridDictionary[new Vector2(x, z)].GetComponent<GridCell>().SetPosition(x, z);
                    gridDictionary[new Vector2(x, z)].GetComponent<GridCell>().gameObject.name = string.Format("Grid Item [{0},{1}]", x, z);

                    gameGridContent.Add(instatiatedObj);

                    instatiatedObj = null;
                    index++;

                    yield return new WaitForSeconds(timeBetwenCellSpawn);
                }
            }
            generationFinished = true;
        }
        #endregion

        #region - Game Content Cells Manegment -
        public void ManageCellContent() => gameGridContent.RemoveAll(s => s == null);//This method remove all objects that is on the game content list an is equals to null
        #endregion

        #region - Grid Center Calculation -
        private Vector3 CalculateGridCenter()
        {
            float Y_Pos = 2.3f;
            Vector3 newCenterCoordinate = Vector3.zero;

            if ((height % 2) > 0)//Odd number verification
            {
                float oddIndex = height / 2;

                oddIndex = Mathf.RoundToInt(oddIndex);

                if (height > 4) Y_Pos += oddIndex * gridSpaceSize;

                newCenterCoordinate = new Vector3(oddIndex * gridSpaceSize, Y_Pos, oddIndex * gridSpaceSize);
            }
            else if ((height % 2) <= 0)//Pair Number verification
            {
                float pairIndex = (height / 2) - 0.5f;

                if (height > 4) Y_Pos += pairIndex * gridSpaceSize;

                newCenterCoordinate = new Vector3(pairIndex * gridSpaceSize, Y_Pos, pairIndex * gridSpaceSize);
            }
            return newCenterCoordinate;
        }
        #endregion

        #region - Grid Destroy Action -
        private void DestroyPreviusGrid()
        {
            foreach(GameObject obj in gameGridContent) DestroyImmediate(obj);
            gameGridContent.Clear();
        }
        #endregion

        #endregion
    }
}