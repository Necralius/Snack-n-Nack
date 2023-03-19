using System.Collections;
using System.Collections.Generic;
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

        #region - Scene Managment -
        public void LoadSceneAsync(int SceneIndex) => SceneManager.LoadSceneAsync(SceneIndex);//This method loads an scene using an scene index as argument
        public void LoadSceneAsync(string SceneName) => SceneManager.LoadSceneAsync(SceneName);//This method loads an scene using the scene name as argument
        public void ReloadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//This method reload the current scene
        #endregion
    }
}