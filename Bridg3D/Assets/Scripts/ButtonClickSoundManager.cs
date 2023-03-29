using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonClickSoundManager : MonoBehaviour
{
    public AudioManager audioManager;

    void Awake() {
        audioManager = GetComponent<AudioManager>();
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Button[] buttons = Resources.FindObjectsOfTypeAll<Button>();
        foreach(Button b in buttons){
            b.onClick.AddListener(PlayButtonClick);
        }
        TMP_Dropdown[] dropdowns = Resources.FindObjectsOfTypeAll<TMP_Dropdown>();
        foreach(TMP_Dropdown d in dropdowns){
            d.onValueChanged.AddListener(PlayButtonClick);
        }
        Slider[] sliders = Resources.FindObjectsOfTypeAll<Slider>();
        foreach(Slider s in sliders){
            if(s.name.EndsWith("Slider"))
                s.onValueChanged.AddListener(PlayButtonClick);
        }
    }

    public void PlayButtonClick(){
        audioManager.Play("ButtonClick");
    }

    public void PlayButtonClick(int a){
        audioManager.Play("ButtonClick");
    }

    public void PlayButtonClick(float a){
        audioManager.Play("ButtonClick");
    }
}
