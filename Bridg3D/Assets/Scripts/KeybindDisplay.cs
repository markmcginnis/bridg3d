using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeybindDisplay : MonoBehaviour
{
    [System.Serializable]
    public struct UIelement{
        public TMP_Text textUI;
        public string name;
    }

    InputManager inputManager;
    public UIelement[] UIElements;
    Dictionary<string,TMP_Text> elements;

    // Start is called before the first frame update
    void Start()
    {
        elements = new Dictionary<string, TMP_Text>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        foreach(UIelement e in UIElements){
            elements[e.name] = e.textUI;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Dictionary<string, KeyCode> keybinds = inputManager.GetKeybindsDictionary();
        foreach(var keybind in keybinds){
            elements[keybind.Key].text = keybind.Key + " - " + keybind.Value.ToString();
        }
    }
}
