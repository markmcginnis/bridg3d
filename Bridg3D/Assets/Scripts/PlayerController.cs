using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackController))]
[RequireComponent(typeof(DefendController))]
[RequireComponent(typeof(HealthController))]
public class PlayerController : MonoBehaviour
{
    AttackController attackController;
    DefendController defendController;
    HealthController healthController;
    [SerializeField]
    MarketController marketController;
    InputManager input;

    public enum GameState {COMBAT, UPGRADES, PAUSE, KEYBINDS}

    GameState currState = GameState.COMBAT;
    GameState returnState;

    public bool pauseMenuOpen = false;

    void Start(){
        input = GameObject.FindObjectOfType<InputManager>();
        attackController = GetComponent<AttackController>();
        defendController = GetComponent<DefendController>();
        healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input.keybindMenuOpen){
            currState = GameState.KEYBINDS;
        }
        else if(pauseMenuOpen){
            currState = GameState.PAUSE;
        }

        Debug.Log(currState);

        switch(currState){
            case GameState.PAUSE:
                //if game is paused, only actions are to undo the pausing
                Cursor.lockState = CursorLockMode.Confined;
                if(input.GetButtonDown("Cancel") || !pauseMenuOpen){
                    pauseMenuOpen = false;
                    currState = returnState;
                }
                break;
            case GameState.KEYBINDS:
                //if keybinds menu is open, only action is close keybind menu
                Cursor.lockState = CursorLockMode.Confined;
                if(input.GetButtonDown("Cancel")){
                    input.CloseKeybindMenu();
                    currState = GameState.PAUSE;
                }
                break;
            case GameState.UPGRADES:
                //if player is using market, only actions are to close the market or pause the game
                Cursor.lockState = CursorLockMode.Confined;
                if(input.GetButtonDown("Open Upgrade Menu") || !marketController.upgradeMenuOpen){
                    marketController.CloseUpgradeMenu();
                    currState = GameState.COMBAT;
                }
                if(input.GetButtonDown("Cancel")){
                    pauseMenuOpen = true;
                    returnState = currState;
                    currState = GameState.PAUSE;
                }
                break;
            case GameState.COMBAT:
            Cursor.lockState = CursorLockMode.Locked;
                //default state, accept all combat inputs or state changing inputs
                if(input.GetButtonDown("Defend")){
                    defendController.ShieldUp();
                }
                if(input.GetButtonUp("Defend")){
                    defendController.ShieldDown();
                }
                if(input.GetButtonDown("Attack")){
                    attackController.Attack();
                }
                if(input.GetButtonDown("Buy Health")){
                    marketController.BuyHealth();
                }
                if(input.GetButtonDown("Open Upgrade Menu") && !marketController.upgradeMenuOpen){
                    if(marketController.OpenUpgradeMenu()){
                        currState = GameState.UPGRADES;
                    }
                }
                if(input.GetButtonDown("Cancel")){
                    pauseMenuOpen = true;
                    returnState = currState;
                    currState = GameState.PAUSE;
                }
                break;
            default:
                Debug.LogError("ERROR: Default GameState case");
                break;
        }
    }
}
