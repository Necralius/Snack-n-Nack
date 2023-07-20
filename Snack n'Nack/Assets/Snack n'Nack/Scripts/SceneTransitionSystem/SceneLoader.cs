using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NekraliusDevelopmentStudio
{ 
    public class SceneLoader : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //SceneLoader - (0.1)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        #region - Scene Loading Data -
        public bool canLoad = true;
        public int nextSceneIndex = 1;

        //private bool loadingScene = false;
        //public float loadTime = 6f;
        //public bool loadNextSceneWithTime = false;
        //private float currentTime;
        //public Slider sceneLoadTimer;
        #endregion

        //----------------------Methods----------------------\\

        #region - BuiltIn Methods -
        //private void Start()
        //{
        //    loadingScene = false;
        //    sceneLoadTimer.maxValue = loadTime;
        //    sceneLoadTimer.value = 0;
        //    currentTime = 0;
        //}
        //private void Update()
        //{
        //    LoadNextSceneWithTime();
        //}
        #endregion

        #region - Scene Loading System -
        public void LoadNextScene() => TransitionAsset.Instance.LoadScene(nextSceneIndex);
        //void LoadNextSceneWithTime()
        //{
        //    if (canLoad && loadNextSceneWithTime && !loadingScene)
        //    {                
        //        sceneLoadTimer.gameObject.SetActive(true);

        //        if (currentTime >= loadTime)
        //        {
        //            loadingScene = true;
        //            currentTime = 0;
        //            TransitionAsset.Instance.LoadScene(nextSceneIndex);
        //        }
        //        else
        //        {
        //            loadingScene = false;
        //            currentTime += Time.deltaTime;
        //        }

        //        sceneLoadTimer.value = currentTime;
        //    }
        //    else if (!loadNextSceneWithTime) sceneLoadTimer.gameObject.SetActive(false);       
        //}
        public void ReloadActualScene() => TransitionAsset.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
        #endregion
    }
}