using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    public class GridGenerator : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //GridGenerator - (0.1)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality ou code meaning)

        [SerializeField, Range(2, 10)] private int height = 10;
        [SerializeField, Range(2,10)] private int width = 10;
        [SerializeField] private float gridSpaceSize = 5f;

        [SerializeField] private GameObject gridCellPrefab;
        public GameObject[,] gameGrid;
        public List<GameObject> gameGridContent;

        void Start()
        {
            GenerateGrid();
        }
        public void GenerateGrid()
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

            if (!(gameGridContent.Equals(null)))
            {
                DestroyPreviusGrid();
            }

            gameGrid = new GameObject[height, width];
            gameGridContent = new List<GameObject>(height * width);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    GameObject instatiatedObj = Instantiate(gridCellPrefab, new Vector3(transform.position.x + x * gridSpaceSize, transform.position.y, transform.position.z + y * gridSpaceSize), Quaternion.identity);
                    gameGridContent.Add(instatiatedObj);

                    gameGrid[x, y] = instatiatedObj;
                    gameGrid[x, y].transform.parent = gameObject.transform;
                    gameGrid[x, y].gameObject.name = string.Format("Grid Item [{0},{1}]", x, y);
                    instatiatedObj = null;
                }
            }
        }
        private void DestroyPreviusGrid()
        {
            foreach(GameObject obj in gameGridContent) DestroyImmediate(obj);
            gameGridContent.Clear();
        }
    }
}