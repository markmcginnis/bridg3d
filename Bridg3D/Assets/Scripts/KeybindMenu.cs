using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KeybindMenu : MonoBehaviour
{
    InputManager inputManager;
    public GameObject keybindItem;
    public GameObject rebindPrompt;
    string rebindAction = null;
    Dictionary<string, TMP_Text> labelNames;

    void Start(){
        labelNames = new Dictionary<string, TMP_Text>();
        inputManager = GameObject.FindObjectOfType<InputManager>();

        string[] axisNames = inputManager.GetAxisNames();
        for(int i = 0; i < axisNames.Length; i++){
            GameObject keybindArea = (GameObject)Instantiate(keybindItem);
            keybindArea.transform.SetParent(this.transform);
            keybindArea.transform.localScale = Vector3.one;

            TMP_Text buttonNameText = keybindArea.transform.Find("ButtonName").GetComponent<TMP_Text>();
            buttonNameText.text = axisNames[i].Substring(0,axisNames[i].Length-1) + ((axisNames[i].EndsWith('+')) ? " Positive" : " Negative");

            TMP_Text boundButtonText = keybindArea.transform.Find("BindButton/BoundButton").GetComponent<TMP_Text>();
            boundButtonText.text = inputManager.GetAxisKeyName(axisNames[i]);
            labelNames[axisNames[i]] = boundButtonText;

            Button bindButton = keybindArea.transform.Find("BindButton").GetComponent<Button>();
            string name = axisNames[i];
            bindButton.onClick.AddListener(() => { StartRebindFor(name); });
        }

        string[] buttonNames = inputManager.GetButtonNames();
        for(int i = 0; i < buttonNames.Length; i++){
            GameObject keybindArea = (GameObject)Instantiate(keybindItem);
            keybindArea.transform.SetParent(this.transform);
            keybindArea.transform.localScale = Vector3.one;

            TMP_Text buttonNameText = keybindArea.transform.Find("ButtonName").GetComponent<TMP_Text>();
            buttonNameText.text = buttonNames[i];

            TMP_Text boundButtonText = keybindArea.transform.Find("BindButton/BoundButton").GetComponent<TMP_Text>();
            boundButtonText.text = inputManager.GetKeyName(buttonNames[i]);
            labelNames[buttonNames[i]] = boundButtonText;

            Button bindButton = keybindArea.transform.Find("BindButton").GetComponent<Button>();
            string name = buttonNames[i];
            bindButton.onClick.AddListener(() => { StartRebindFor(name); });
        }

        
    }

    void StartRebindFor(string actionName){
        rebindAction = actionName;
    }

    void Update(){
        if(rebindAction == null){
            rebindPrompt.SetActive(false);
            return;
        }
        else{
            rebindPrompt.SetActive(true);
        }
        KeyCode[] allKeycodes = (KeyCode[])Enum.GetValues(typeof(KeyCode));
        foreach(KeyCode keyCode in allKeycodes){
            if(Input.GetKeyDown(keyCode)){
                Debug.Log(rebindAction);
                if(rebindAction.EndsWith('+') || rebindAction.EndsWith('-'))
                    inputManager.RebindAxis(rebindAction, keyCode);
                else
                    inputManager.RebindKey(rebindAction, keyCode);
                labelNames[rebindAction].text = keyCode.ToString();
                rebindAction = null;
                break;
            }
        }
    }
}
