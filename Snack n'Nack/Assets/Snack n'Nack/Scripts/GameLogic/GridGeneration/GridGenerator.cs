using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Security.Cryptography;

namespace NekraliusDevelopmentStudio
{
    public class GridGenerator : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //GridGenerator - (0.1)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
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

        public bool generationFinished = false;

        #region - Camera Adjustment Data-
        Vector3 centerCoordinate;
        [Header("Camera Adjustment Data")]
        public GameObject centerTarget;
        public float cameraTravelSpeed = 3f;
        public int newMatrixFactor = 3;
        #endregion

        #region - Diferent Cubs Spawn -
        public GameObject[] cellTypesPrefabs;
        public GameObject selectedObject;
        public int spawnedTimes = 2;

        public List<GameObject> pairs;
        #endregion

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
        #endregion

        public void ShowAllCells()
        {
            foreach(GameObject obj in gameGridContent) obj.GetComponent<GridCell>().ShowObject();
        }
        public void HideAllCells()
        {
            foreach (GameObject obj in gameGridContent) obj.GetComponent<GridCell>().HideObject();
        }

        #region - Grid Generation -

        #region - Grid Generator In Game -
        public void GenerateGridInGameAction() => StartCoroutine(GenerateGridInGame());
        public IEnumerator GenerateGridInGame()
        {
            generationFinished = false;

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

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (GameManager.Instance.casualMode)
                    {
                        if (spawnedTimes == 2)
                        {
                            selectedObject = cellTypesPrefabs[Random.Range(0, cellTypesPrefabs.Length)];
                            spawnedTimes = 1;
                        }
                        else if (spawnedTimes < 2) spawnedTimes++;
                    }
                    else if (GameManager.Instance.infinityMode)
                    {
                        Debug.Log("Not implemented yet!");
                    }

                    GameObject instatiatedObj = Instantiate(selectedObject, new Vector3(transform.position.x + x * gridSpaceSize, transform.position.y, transform.position.z + z * gridSpaceSize), Quaternion.identity, transform);

                    gameGridContent.Add(instatiatedObj);

                    gameGrid[x, z] = instatiatedObj;
                    gameGrid[x, z].GetComponent<GridCell>().SetPosition(x, z);
                    gameGrid[x, z].gameObject.name = string.Format("Grid Item [{0},{1}]", x, z);

                    instatiatedObj = null;

                    yield return new WaitForSeconds(timeBetwenCellSpawn);
                }
            }
            generationFinished = true;
        }
        public void GeneratePairs()
        {
            int looper = height * height;
            int index = 0;

            for (int i = 0; i < looper; i++)
            {
                if (index == looper / 2) index = 0;
                pairs.Add(cellTypesPrefabs[index]);
                index++;
            }
            System.Random random = new System.Random();
            pairs = pairs.OrderBy(e => random.Next()).ToList();
            pairs = pairs.OrderBy(e => random.Next()).ToList();
        }
        public void RegenerateGridWithNewFactor(int factor) => StartCoroutine(GenerateGridInGame(factor));
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

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    GameObject selectedPrefab = cellTypesPrefabs[Random.Range(0, cellTypesPrefabs.Length)];

                    GameObject instatiatedObj = Instantiate(selectedPrefab, new Vector3(transform.position.x + x * gridSpaceSize, transform.position.y, transform.position.z + z * gridSpaceSize), Quaternion.identity, transform);

                    gameGridContent.Add(instatiatedObj);

                    gameGrid[x, z] = instatiatedObj;
                    gameGrid[x, z].GetComponent<GridCell>().SetPosition(x, z);
                    gameGrid[x, z].gameObject.name = string.Format("Grid Item [{0},{1}]", x, z);

                    instatiatedObj = null;

                    yield return new WaitForSeconds(timeBetwenCellSpawn);
                }
            }
            generationFinished = true;
        }
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

        #region - Grid Generator In Editor -
        public void GenerateGridInEditorAction()
        {
            if (height != width)
            {
                Debug.LogWarning("The height and width numbers must be equals!");
                return;
            }

            if (gridCellPrefab == null)
            {
                Debug.LogWarning("Please assing a valid grid cell prefab!");
                return;
            }

            if (!(gameGridContent.Equals(null))) DestroyPreviusGrid();

            gameGrid = new GameObject[height, width];
            gameGridContent = new List<GameObject>(height * width);

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    GameObject selectedPrefab = cellTypesPrefabs[Random.Range(0, cellTypesPrefabs.Length)];

                    GameObject instatiatedObj = Instantiate(selectedPrefab, new Vector3(transform.position.x + x * gridSpaceSize, transform.position.y,transform.position.z + z * gridSpaceSize), Quaternion.identity, transform);
                    gameGridContent.Add(instatiatedObj);

                    gameGrid[x, z] = instatiatedObj;
                    gameGrid[x, z].GetComponent<GridCell>().SetPosition(x, z);
                    gameGrid[x, z].gameObject.name = string.Format("Grid Item [{0},{1}]", x, z);

                    instatiatedObj = null;
                }
            }
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