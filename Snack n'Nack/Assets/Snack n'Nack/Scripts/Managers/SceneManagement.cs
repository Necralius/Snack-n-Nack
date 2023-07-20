using UnityEngine;
using UnityEngine.SceneManagement;

namespace NekraliusDevelopmentStudio
{
    public class SceneManagement : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //SceneManagement - (Code Version)
        //State: Functional - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        #region - Singleton Pattern
        public static SceneManagement Instance;
        private void Awake() => Instance = this;
        #endregion

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        #region - Scene Managment -
        public void LoadSceneAsync(int SceneIndex) => SceneManager.LoadSceneAsync(SceneIndex);//This method loads an scene using an scene index as argument
        public void LoadSceneAsync(string SceneName) => SceneManager.LoadSceneAsync(SceneName);//This method loads an scene using the scene name as argument
        public void LoadInGameSceneAsync(int SceneIndex)
        {
            SceneManager.LoadSceneAsync(SceneIndex);//This method loads an scene using an scene index as argument
            PlayerManager.Instance.gameType = SceneIndex == 1 ? GameType.CasualMode : GameType.InfinityMode;
            PlayerManager.Instance.gameState = GameSate.InGame;
        }
        public void LoadMenuSceneAsync()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(0);//This method loads an scene using an scene index as argument
            if (operation.isDone) return;
            PlayerManager.Instance.gameState = GameSate.InMenu;
        }
        public void ReloadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//This method reload the current scene
        public void ReloadGameScene() => LoadInGameSceneAsync(SceneManager.GetActiveScene().buildIndex);
        #endregion
    }
}