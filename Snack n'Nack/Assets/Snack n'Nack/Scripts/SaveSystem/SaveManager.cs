using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NekraliusDevelopmentStudio
{
    public class SaveManager : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //SaveManager - (0.1 Version)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        #region - Singleton Pattern -
        public static SaveManager Instance;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else Destroy(gameObject);
        }
        #endregion

        public GameData gameData;
        private FileDataHandler fileDataHandler;

        public string fileName;

        public List<IPersistentData> loadableDatas = new List<IPersistentData>();

        private void Start()
        {
            fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

            LoadGameData();
        }
        private void Update()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private List<IPersistentData> GetAllLoadables()
        {
            IEnumerable<IPersistentData> dataLoadables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<IPersistentData>();
            return new List<IPersistentData>(dataLoadables);
        }

        public void LoadAllData()
        {
            if (gameData == null)
            {
                Debug.LogWarning("Data is null!");
                return;
            }

            loadableDatas = GetAllLoadables();
            foreach (var data in loadableDatas) data.LoadData(gameData);
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Scene loaded!");

            LoadAllData();
        }

        public GameData LoadGameData()
        {
            gameData = fileDataHandler.Load();
            return gameData;
        }

        public void FullGameDataSave()
        {
            gameData = GetGameSave();
            fileDataHandler.Save(gameData);
        }
        public GameData GetGameSave()
        {
            if (gameData == null)
            {
                GameData gameStateData = new GameData(AudioManager.Instance);
                return gameStateData;
            }
            gameData.SaveGame(AudioManager.Instance);
            return gameData;
        }
        private void OnApplicationQuit() => FullGameDataSave();
    }
}