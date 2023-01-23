using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour
{
    [System.Serializable]
    public class Keybind{
        public string actionName;
        public KeyCode button;
    }

    [System.Serializable]
    public class Axis{
        public string axisName;
        public KeyCode positiveButton;
        public KeyCode negativeButton;
    }

    Dictionary<string, KeyCode> keybinds;
    Dictionary<string, Axis> axes;


    public List<Keybind> setKeybinds;
    public List<Axis> setAxes;

    GameObject player;
    public bool keybindMenuOpen = false;
    public GameObject keybindMenuContainer;

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable(){
        player = GameObject.FindGameObjectWithTag("Player");
        keybinds = new Dictionary<string, KeyCode>();
        axes = new Dictionary<string, Axis>();
        foreach(Keybind keybind in setKeybinds){
            keybinds[keybind.actionName] = keybind.button;
        }
        foreach(Axis axis in setAxes){
            axes[axis.axisName] = axis;
        }
    }

    public string[] GetButtonNames(){
        return keybinds.Keys.ToArray();
    }

    public string[] GetAxisNames(){
        string[] rawAxes = axes.Keys.ToArray();
        List<string> axisNames = new List<string>();
        foreach(string axis in rawAxes){
            axisNames.Add(axis+"+");
            axisNames.Add(axis+"-");
        }
        return axisNames.ToArray();
    }

    public string GetAxisKeyName(string axisName){
        if(!axes.ContainsKey(axisName.Substring(0,axisName.Length-1))){
            Debug.LogError("Axis " + axisName + " not found!");
            return "ERROR";
        }
        return (axisName.EndsWith('+')) ? axes[axisName.Substring(0,axisName.Length-1)].positiveButton.ToString() : axes[axisName.Substring(0,axisName.Length-1)].negativeButton.ToString();
    }

    public string GetKeyName(string actionName){
        if(!keybinds.ContainsKey(actionName)){
            Debug.LogError("Action " + actionName + " not found!");
            return "ERROR";
        }
        return keybinds[actionName].ToString();
    }

    public bool GetButtonDown(string actionName){
        if(!keybinds.ContainsKey(actionName)){
            Debug.LogError("Action " + actionName + " not found!");
            return false;
        }
        return Input.GetKeyDown(keybinds[actionName]);
    }

    public bool GetButtonUp(string actionName){
        if(!keybinds.ContainsKey(actionName)){
            Debug.LogError("Action " + actionName + " not found!");
            return false;
        }
        return Input.GetKeyUp(keybinds[actionName]);
    }

    public float GetAxis(string axisName){
        if(!axes.ContainsKey(axisName)){
            Debug.LogError("Action " + axisName + " not found!");
            return 0f;
        }
        Axis axis = axes[axisName];
        float positive = (Input.GetKey(axis.positiveButton)) ? 1f : 0f;
        float negative = (Input.GetKey(axis.negativeButton)) ? -1f : 0f;
        return positive + negative;
    }

    public void RebindKey(string actionName, KeyCode keyCode){
        keybinds[actionName] = keyCode;
    }

    public void RebindAxis(string axisName, KeyCode keyCode){
        if(axisName.EndsWith('+'))
            axes[axisName.Substring(0,axisName.Length-1)].positiveButton = keyCode;
        else
            axes[axisName.Substring(0,axisName.Length-1)].negativeButton = keyCode;
    }

    public void OpenKeybindMenu(){
        keybindMenuOpen = true;
        keybindMenuContainer.SetActive(true);
        if(player){
            player.GetComponentInChildren<MouseLook>().enabled = false;
            player.GetComponent<FPSMovement>().enabled = false;
        }
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseKeybindMenu(){
        keybindMenuOpen = false;
        keybindMenuContainer.SetActive(false);
        if(player){
            player.GetComponentInChildren<MouseLook>().enabled = true;
            player.GetComponent<FPSMovement>().enabled = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
}
