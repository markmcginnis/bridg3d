using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void Quit()
    {
        UnityEngine.Debug.Log("quit");
        Application.Quit();
    }
}
