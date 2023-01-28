using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public AudioManager audioManager;

    void Start(){
        Cursor.lockState = CursorLockMode.Confined;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if(!playerController)
            return;
        audioManager = playerController.GetComponent<AudioManager>();        
    }

    public void Play(){
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadEndScene(bool isVictory){
        SceneManager.LoadScene((isVictory) ? "WinMenu" : "LossMenu");
    }

    public void Quit(){
        UnityEngine.Debug.Log("quit");
        Application.Quit();
    }

    public void Pause(){
        // Debug.Log("pause");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        audioManager.Pause(audioManager.currentSongName);
    }

    public void Resume(){
        // Debug.Log("resume");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        audioManager.Play(audioManager.currentSongName);
    }
}
