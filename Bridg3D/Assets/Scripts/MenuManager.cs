using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public PlayerController playerController;
    public AudioManager audioManager;

    void Start(){
        // Cursor.lockState = CursorLockMode.Confined;
        MouseAdjuster.SetState(CursorLockMode.Confined);
        playerController = FindObjectOfType<PlayerController>();
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
        // Cursor.lockState = CursorLockMode.None;
        MouseAdjuster.SetState(CursorLockMode.Confined);
        audioManager.Pause(audioManager.currentSongName);
        playerController.pauseMenuOpen = true;
    }

    public void Resume(){
        // Debug.Log("resume");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        // Cursor.lockState = CursorLockMode.Locked;
        MouseAdjuster.SetState(CursorLockMode.Locked);
        audioManager.Resume(audioManager.currentSongName);
        playerController.pauseMenuOpen = false;
    }
}
