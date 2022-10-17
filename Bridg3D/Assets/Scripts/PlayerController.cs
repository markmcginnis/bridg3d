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

    public bool acceptCombatInput = true;
    public bool acceptOtherInput = true;

    void Start(){
        input = GameObject.FindObjectOfType<InputManager>();
        attackController = GetComponent<AttackController>();
        defendController = GetComponent<DefendController>();
        healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(acceptCombatInput){
            if(input.GetButtonDown("Defend")){
                defendController.ShieldUp();
            }
            if(input.GetButtonUp("Defend")){
                defendController.ShieldDown();
            }
            if(input.GetButtonDown("Attack")){
                attackController.Attack();
            }
        }
        if(acceptOtherInput){
            if(input.GetButtonDown("Buy Health")){
            marketController.BuyHealth();
        }
        if((input.GetButtonDown("Cancel") || input.GetButtonDown("Open Upgrade Menu")) && marketController.upgradeMenuOpen){
            marketController.CloseUpgradeMenu();
        }
        else if(input.GetButtonDown("Open Upgrade Menu") && !marketController.upgradeMenuOpen && !input.keybindMenuOpen){
            marketController.OpenUpgradeMenu();
        }
        }
        if((input.GetButtonDown("Cancel") || input.GetButtonDown("Open Keybind Menu")) && input.keybindMenuOpen){
            input.CloseKeybindMenu();
        }
        else if(input.GetButtonDown("Open Keybind Menu") && !input.keybindMenuOpen && !marketController.upgradeMenuOpen){
            input.OpenKeybindMenu();
        }
    }
}
