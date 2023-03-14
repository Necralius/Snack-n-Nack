using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
    //CompleteCodeName - (Code Version)
    //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
    //This code represents (Code functionality ou code meaning)

    #region - Singleton Pattern -
    public static GameManager Instance;
    private void Awake() => Instance = this;
    #endregion

    #region - UI Update -
    [Header("UI Items")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    #endregion

    public void LoadSceneAsync(int SceneIndex) => SceneManager.LoadSceneAsync(SceneIndex);//This method loads an scene using an scene index as argument
    public void LoadSceneAsync(string SceneName) => SceneManager.LoadSceneAsync(SceneName);//This method loads an scene using the scene name as argument
    public void ReloadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//This method reload the current scene
}