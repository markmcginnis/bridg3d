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
        if(scene.name == "MainMenu"){
            GameObject.Find("TitleSong").GetComponent<AudioSource>().volume = settings.musicVolume;
            return;
        }
        if(scene.name == "WinMenu"){
            GameObject.Find("WinSong").GetComponent<AudioSource>().volume = settings.musicVolume;
            return;
        }
        if(scene.name == "LossMenu"){
            GameObject.Find("LossSong").GetComponent<AudioSource>().volume = settings.musicVolume;
            return;
        }
        Debug.Log("setting menu values");
        GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        
        TMP_Dropdown difficultySelector = Array.Find(Resources.FindObjectsOfTypeAll<TMP_Dropdown>(), dropdown => dropdown.name == "DifficultySelector");
        if(difficultySelector){
            difficultySelector.value = (int)settings.difficulty;
            difficultySelector.onValueChanged.AddListener(delegate {ChangeDifficulty(difficultySelector.value);});
            Debug.Log("set difficulty");
        }
        Slider sfxVolumeSlider = Array.Find(Resources.FindObjectsOfTypeAll<Slider>(), slider => slider.name == "SFXVolumeSlider");
        if(sfxVolumeSlider){
            sfxVolumeSlider.value = settings.sfxVolume;
            sfxVolumeSlider.onValueChanged.AddListener(delegate {ChangeSFXVolume(sfxVolumeSlider.value);});
            Debug.Log("set volume");
        }
        Slider musicVolumeSlider = Array.Find(Resources.FindObjectsOfTypeAll<Slider>(), slider => slider.name == "MusicVolumeSlider");
        if(musicVolumeSlider){
            musicVolumeSlider.value = settings.musicVolume;
            musicVolumeSlider.onValueChanged.AddListener(delegate {ChangeMusicVolume(musicVolumeSlider.value);});
            Debug.Log("set volume");
        }
        Slider mouseSensSlider = Array.Find(Resources.FindObjectsOfTypeAll<Slider>(), slider => slider.name == "MouseSensSlider");
        if(mouseSensSlider){
            mouseSensSlider.value = settings.mouseSens;
            mouseSensSlider.onValueChanged.AddListener(delegate {ChangeMouseSens(mouseSensSlider.value);});
            Debug.Log("set mouse sens");
        }
        Time.timeScale = 1f;
    }

    public void ChangeDifficulty(int newDifficulty){
        settings.difficulty = (Settings.Difficulty)newDifficulty;
    }

    public void ChangeSFXVolume(float newVolume){
        settings.sfxVolume = newVolume;
        AudioManager[] audioManagers = FindObjectsOfType<AudioManager>();
        foreach(AudioManager audioManager in audioManagers){
            audioManager.ChangeVolume(settings.sfxVolume,true);
        }
    }

    public void ChangeMusicVolume(float newVolume){
        settings.musicVolume = newVolume;
        AudioManager[] audioManagers = FindObjectsOfType<AudioManager>();
        foreach(AudioManager audioManager in audioManagers){
            audioManager.ChangeVolume(settings.musicVolume,false);
        }
    }

    public void ChangeTitleSongVolume(float newVolume){
        GameObject.Find("TitleSong").GetComponent<AudioSource>().volume = newVolume;
    }

    public void ChangeMouseSens(float newSens){
        settings.mouseSens = newSens;
    }
}