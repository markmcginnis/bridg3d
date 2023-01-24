using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static Settings settings;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        settings = gameObject.AddComponent<Settings>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(scene.name == "MainMenu")
            return;
        Debug.Log("setting menu values");
        GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        
        TMP_Dropdown difficultySelector = Array.Find(Resources.FindObjectsOfTypeAll<TMP_Dropdown>(), dropdown => dropdown.name == "DifficultySelector");
        if(difficultySelector){
            difficultySelector.value = (int)settings.difficulty;
            difficultySelector.onValueChanged.AddListener(delegate {ChangeDifficulty(difficultySelector.value);});
            Debug.Log("set difficulty");
        }
        Slider volumeSlider = Array.Find(Resources.FindObjectsOfTypeAll<Slider>(), slider => slider.name == "VolumeSlider");
        if(volumeSlider){
            volumeSlider.value = settings.audioVolume;
            volumeSlider.onValueChanged.AddListener(delegate {ChangeVolume(volumeSlider.value);});
            Debug.Log("set volume");
        }
        Slider mouseSensSlider = Array.Find(Resources.FindObjectsOfTypeAll<Slider>(), slider => slider.name == "MouseSensSlider");
        if(mouseSensSlider){
            mouseSensSlider.value = settings.mouseSens;
            mouseSensSlider.onValueChanged.AddListener(delegate {ChangeMouseSens(mouseSensSlider.value);});
            Debug.Log("set mouse sens");
        }
    }

    public void ChangeDifficulty(int newDifficulty){
        settings.difficulty = (Settings.Difficulty)newDifficulty;
    }

    public void ChangeVolume(float newVolume){
        settings.audioVolume = newVolume;
        AudioManager[] audioManagers = FindObjectsOfType<AudioManager>();
        foreach(AudioManager audioManager in audioManagers){
            audioManager.ChangeVolume(settings.audioVolume);
        }
    }

    public void ChangeMouseSens(float newSens){
        settings.mouseSens = newSens;
    }
}