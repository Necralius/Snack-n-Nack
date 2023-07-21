using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Security.Cryptography;
using Unity.VisualScripting;
using NekraliusDevelopmentStudio;
using UnityEngine.SocialPlatforms.Impl;

namespace NekraliusDevelopmentStudio
{
    public enum SpawnType {Casual, Infinity };
    
    public class GridGenerator : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //GridGenerator - (0.5)
        //State: Functional - (Needs Refactoring, Needs Coments)
        //This code represents (Code functionality ou code meaning)

        #region - Singleton Pattern -
        public static GridGenerator Instance;
        private void Awake() => Instance = this;
        #endregion

        #region - Grid Data -
        [Header("Grid Data")]
        [Range(2,10)] public int MatrixSize = 3;
        private int height = 10;
        private int width = 10;
        [SerializeField] private float gridSpaceSize = 5f;
        [SerializeField] private float timeBetwenCellSpawn = 0.5f;
        private GameObject[,] gameGrid;
        #endregion

        #region - General World Grid Data
        public CustomDictionary gridDictionary;
        public List<GameObject> gameGridContent;
        public List<GameObject> generatedPairs;

        #endregion

        #region - Generation State -
        public bool generatedSeted = false;
        public bool generationFinished = false;
        #endregion

        #region - Camera Adjustment Data-

        Vector3 centerCoordinate;
        [Header("Camera Adjustment Data")]
        public GameObject centerTarget;
        public float cameraTravelSpeed = 3f;
        public float Y_Pos = 2.3f;
        #endregion

        #region - Diferent Cubes Types Spawn -
        [Header("Cell Puzzles Types")]
        public List<GameObject> actualGamePossibilities;
        public List<GameObject> allTypePossibilities;
        public GameObject selectedObject;

        [Header("Special Card Spawn")]
        public GameObject specialCardPrefab;
        public float specialSpawnTime = 15;
        [SerializeField] private float specialTime;
        public bool needsSpecial;

        #endregion

        #region - Audio System -

        #endregion

        #region - Infinity Mode -
        public SpawnType spawnType;

        [Header("Infinity Mode")]
        public int spwanFromQuantity = 2;

        public List<GridAspects> blocksMissing;

        public List<GameObject> singlePair;

        public bool NeedsGeneration = false;
        #endregion

        //----------- Methods -----------//

        #region - BuildIn Methods -
        private void Start()
        {
            centerCoordinate = centerTarget.transform.position;
            generatedSeted = false;        
        }
        private void Update()
        {
            if (blocksMissing.Count >= spwanFromQuantity) NeedsGeneration = true;
            else if (blocksMissing.Count == 0) NeedsGeneration = false;

            centerTarget.transform.localPosition = Vector3.Lerp(centerTarget.transform.localPosition, CalculateGridCenter(), cameraTravelSpeed * Time.deltaTime);
            
            if (NeedsGeneration && PlayerManager.Instance.gameType == GameType.InfinityMode) StartCoroutine(GenerateMissingBlocks());
            RandomObjectPossibilityAddBehavior();
            SpecialCardBehavior();
        }
        #endregion

        #region - Grid Generation - 

        #region - Infinity GameType Generation -
        private void SpawnBlock(GameObject blockToSpawn, GridAspects gridAspect)
        {      
            if (!gridDictionary.ValueExists(new Vector2Int(gridAspect.posX, gridAspect.posZ)))
            {
                GameObject instatiatedObj = Instantiate(blockToSpawn, gridAspect.gridCellAbsolutePosition, Quaternion.identity, transform);

                gridDictionary.Add(new Vector2Int(gridAspect.posX, gridAspect.posZ), instatiatedObj);

                GridAspects currentGrid = gridAspect;
                GridCell currentCell = gridDictionary.GetValue(new Vector2Int(currentGrid.posX, currentGrid.posZ)).GetComponent<GridCell>();

                currentCell.SetPosition(currentGrid.posX, currentGrid.posZ);
                currentCell.gameObject.name = string.Format("Grid Item [{0},{1}]", currentGrid.posX, currentGrid.posZ);
                currentCell.literalPosition = new Vector3(transform.position.x + currentGrid.posX * gridSpaceSize, transform.position.y, transform.position.z + currentGrid.posZ * gridSpaceSize);

                gameGridContent.Add(instatiatedObj);

                AudioManager.Instance.audioDatabase.GetAndPlayEffectClip("Cell Generated");               
            }
        }
        public IEnumerator GenerateMissingBlocks()
        {
            //if (!NeedsGeneration) yield return null;

            yield return new WaitForSeconds(2f);

            //if (!NeedsGeneration) yield return null;

            if (blocksMissing.Count >= 2)
            {
                GenerateSinglePair();
                for (int i = 0; i < singlePair.Count; i++)
                {
                    if (singlePair[i] != null || blocksMissing[i] != null)
                    {
                        GridAspects selectedGridAspect = blocksMissing[0];

                        SpawnBlock(singlePair[i], selectedGridAspect);
                        blocksMissing.Remove(blocksMissing[0]);

                        yield return new WaitForSeconds(timeBetwenCellSpawn);
                    }
                }
            }
            if (blocksMissing.Count == 0) StopAllCoroutines();
        }
        #endregion

        #region - Casual GameType Generation - 
        public void GenerateGridInGameAction(int matrixFactor) => StartCoroutine(GenerateGridInGame(matrixFactor));
        public IEnumerator GenerateGridInGame(int newMatrixFactor)
        {
            if (PlayerManager.Instance.gameType.Equals(GameType.InfinityMode)) AddRandomObjectType(4);
            else AddRandomObjectType(4);

            generatedSeted = true;
            generationFinished = false;

            height = newMatrixFactor;
            width = newMatrixFactor;
            MatrixSize = newMatrixFactor;

            if (height != width)
            {
                Debug.LogWarning("The height and width numbers must be equals!");
                yield return null;
            }

            if (!(gameGridContent.Equals(null))) DestroyPreviusGrid();

            gameGrid = new GameObject[height, width];
            gameGridContent = new List<GameObject>(height * width);

            generatedPairs = BlockSelectionGeneration(newMatrixFactor * newMatrixFactor);

            int index = 0;

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    selectedObject = generatedPairs[index];
                    GameObject instatiatedObj = Instantiate(selectedObject, new Vector3(transform.position.x + x * gridSpaceSize, transform.position.y, transform.position.z + z * gridSpaceSize), Quaternion.identity, transform);

                    gridDictionary.Add(new Vector2Int(x, z), instatiatedObj);

                    gridDictionary.GetValue(new Vector2Int(x, z)).GetComponent<GridCell>().SetPosition(x, z);
                    gridDictionary.GetValue(new Vector2Int(x, z)).GetComponent<GridCell>().gameObject.name = string.Format("Grid Item [{0},{1}]", x, z);
                    gridDictionary.GetValue(new Vector2Int(x, z)).GetComponent<GridCell>().literalPosition = new Vector3(transform.position.x + x * gridSpaceSize, transform.position.y, transform.position.z + z * gridSpaceSize);

                    gameGridContent.Add(instatiatedObj);

                    instatiatedObj = null;
                    index++;

                    AudioManager.Instance.audioDatabase.GetAndPlayEffectClip("Cell Generated");

                    yield return new WaitForSeconds(timeBetwenCellSpawn);
                }
            }
            generationFinished = true;
        }
        #endregion

        #endregion

        #region - Cell Animation Management -
        //Only used in casual mode
        public void ShowAllCells()//This method get all the game cells content and call his show animation
        {
            foreach(GameObject obj in gameGridContent) obj.GetComponent<GridCell>().ShowObject();
        }
        public void HideAllCells()//this method get all the game cells content and call his hide animation
        {
            foreach (GameObject obj in gameGridContent) obj.GetComponent<GridCell>().HideObject();
        }
        #endregion

        #region - Pair Random Generation -
        private List<GameObject> BlockSelectionGeneration(int blocksNeeded)
        {
            List<GameObject> generatedBlocks = new List<GameObject>(blocksNeeded);

            for (int i = 0; i < blocksNeeded / 2; i++)
            {
                GenerateSinglePair();
                generatedBlocks.AddRange(singlePair);
            }
            return generatedBlocks;
        }
        private void SpecialCardBehavior()
        {
            if (specialTime >= specialSpawnTime && !needsSpecial)
            {
                specialTime = 0;
                needsSpecial = true;
            }
            else specialTime += Time.deltaTime;
        }
        private void GenerateSinglePair()
        {
            /* This method generate a single block pair of equal blocks,
             * this method has the intent of simplify a complex procedural generation process.
             */
            singlePair.Clear();

            List<GameObject> NewSinglePair = new List<GameObject>(2);

            if (needsSpecial)
            {
                NewSinglePair.Add(specialCardPrefab);
                NewSinglePair.Add(specialCardPrefab);
                needsSpecial = false;
                specialTime = 0;
            }
            else
            {
                int selectedPair = Random.Range(0, actualGamePossibilities.Count);

                for (int i = 0; i < 2; i++) NewSinglePair.Add(actualGamePossibilities[selectedPair]);
            }
            singlePair = NewSinglePair;
        }
        #endregion

        #region - Game Pair Possibilities Manegement -

        public void RandomObjectPossibilityAddBehavior()
        {
            int score = GameManager.Instance.currentScoreValue;
            if (score == 200) AddRandomObjectType(1);
            else if (score == 600) AddRandomObjectType(1);
            else if (score == 1100) AddRandomObjectType(1);
            else if (score == 1700) AddRandomObjectType(1);         
        }

        public void AddRandomObjectType(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                while (true)
                {
                    GameObject selectedType = allTypePossibilities[Random.Range(0, allTypePossibilities.Count)];
                    if (actualGamePossibilities.Any(element => element.Equals(selectedType)))
                    {
                        if (actualGamePossibilities.Count >= allTypePossibilities.Count) break; //Reached all game types possibilities
                        continue;
                    }
                    else
                    {
                        actualGamePossibilities.Add(selectedType);
                        break;
                    }
                }
            }
        }

        #endregion

        #region - Game Content Cells Manegement -
        public void ManageCellContent() => gameGridContent.RemoveAll(s => s == null);//This method remove all objects that is on the game content list an is equals to null
        #endregion

        #region - Grid Center Calculation -
        private Vector3 CalculateGridCenter()
        {
            float newY_Pos = Y_Pos;
            Vector3 newCenterCoordinate = Vector3.zero;

            if ((height % 2) > 0)//Odd number verification
            {
                float oddIndex = height / 2;

                oddIndex = Mathf.RoundToInt(oddIndex);

                if (height > 4) newY_Pos += oddIndex * gridSpaceSize;

                newCenterCoordinate = new Vector3(oddIndex * gridSpaceSize, newY_Pos, oddIndex * gridSpaceSize);
            }
            else if ((height % 2) <= 0)//Pair Number verification
            {
                float pairIndex = (height / 2) - 0.5f;

                if (height > 4) newY_Pos += pairIndex * gridSpaceSize;

                newCenterCoordinate = new Vector3(pairIndex * gridSpaceSize, newY_Pos, pairIndex * gridSpaceSize);
            }
            return newCenterCoordinate;
        }
        #endregion

        #region - Grid Destroy Action -
        private void DestroyPreviusGrid()
        {
            gridDictionary.Clear();
            foreach(GameObject obj in gameGridContent) DestroyImmediate(obj);
            gameGridContent.Clear();
        }
        public void SpecialActionDestroyGrid() => StartCoroutine(SpecialActionDestroy());
        public IEnumerator SpecialActionDestroy()
        {
            gridDictionary.Clear();
            int scoreMultiplicator = gameGridContent.Count / 2;

            foreach(GameObject obj in gameGridContent)
            {
                if (obj != null)
                {
                    GridCell gridAspectObj = obj.GetComponent<GridCell>();
                    blocksMissing.Add(new GridAspects(gridAspectObj));
                    gridAspectObj.outlineEffector.enabled = true;
                    Destroy(obj, 0.1f);
                    yield return new WaitForSeconds(0.1f);
                }
                else continue;
            }

            gameGridContent.Clear();
            GameManager.Instance.CalculateScore(scoreMultiplicator * 30);
            yield return null;
        }
        #endregion
    }
}