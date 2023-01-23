using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public enum Difficulty {EASY, MEDIUM, HARD};

    public Difficulty difficulty = Difficulty.MEDIUM;

    public float audioVolume = 1f;

    public float mouseSens = 100f;

    public bool settingsMenuOpen = false;
    public GameObject settingsMenuContainer;

    void Awake(){
        DontDestroyOnLoad(gameObject);
    }

    public void OpenSettingsMenu(){
        settingsMenuOpen = true;
        settingsMenuContainer.SetActive(true);
    }

    public void CloseSettingsMenu(){
        settingsMenuOpen = false;
        settingsMenuContainer.SetActive(false);
    }

    public void ChangeDifficulty(int newDifficulty){
        difficulty = (Difficulty)newDifficulty;
    }

    public void ChangeVolume(float newVolume){
        audioVolume = newVolume;
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if(audioManager){
            audioManager.ChangeVolume(audioVolume);
        }
    }

    public void ChangeMouseSens(float newSens){
        mouseSens = newSens;
    }
}
