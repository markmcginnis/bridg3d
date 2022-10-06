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

    public bool acceptCombatInput = true;

    void Start(){
        attackController = GetComponent<AttackController>();
        defendController = GetComponent<DefendController>();
        healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(acceptCombatInput){
            if(Input.GetButtonDown("Fire2")){
                defendController.ShieldUp();
            }
            if(Input.GetButtonUp("Fire2")){
                defendController.ShieldDown();
            }
            if(Input.GetButtonDown("Fire1")){
                attackController.Attack();
            }
        }
        if(Input.GetButtonDown("BuyHealth")){
            marketController.BuyHealth();
        }
        if((Input.GetButtonDown("Cancel") || Input.GetButtonDown("BuyUpgrade")) && marketController.upgradeMenuOpen){
            marketController.CloseUpgradeMenu();
        }
        else if(Input.GetButtonDown("BuyUpgrade") && !marketController.upgradeMenuOpen){
            marketController.OpenUpgradeMenu();
        }
    }
}
