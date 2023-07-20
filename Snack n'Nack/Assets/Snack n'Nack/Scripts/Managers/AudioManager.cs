using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
    //AudioManager - (Code Version)
    //State: Functional - (Needs Refactoring, Needs Coments)
    //This code represents (Code functionality or code meaning)

    #region - Audio Manager -
    public AudioSource effectsSource;
    public AudioSource musicSource;

    public bool audioActive;
    public Toggle audioToggle;

    public Image audioCheckActive;
    public Image audioCheckDeactive;
    #endregion

    public void SetAudio()
    {
        audioActive = audioToggle.isOn;

        if (audioActive)
        {
            audioCheckActive.enabled = true;
            audioCheckDeactive.enabled = false;
        }
        else
        {
            audioCheckActive.enabled = false;
            audioCheckDeactive.enabled = true;
        }
    }
}