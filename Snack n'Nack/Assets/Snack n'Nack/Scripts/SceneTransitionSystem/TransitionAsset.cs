using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionAsset : MonoBehaviour
{
    //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
    //TransitionAsset - (0.1)
    //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
    //This code represents (Code functionality or code meaning)

    private Animator anim => GetComponent<Animator>();

    #region - Singleton Pattern - 
    public static TransitionAsset Instance;
    private void Awake() => Instance = this;
    #endregion

    public float transitionOutTime = 1f;

    public void LoadScene(int sceneIndex) => StartCoroutine(LoadSceneWithTransition(sceneIndex));
    private IEnumerator LoadSceneWithTransition(int sceneIndex)
    {
        anim.SetTrigger("Transition_OUT");
        yield return new WaitForSeconds(transitionOutTime);

        AsyncOperation opr = SceneManager.LoadSceneAsync(sceneIndex);
        if (opr.isDone) anim.SetTrigger("Transition_IN");
    }

}