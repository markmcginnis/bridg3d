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

    void Start(){
        attackController = GetComponent<AttackController>();
        defendController = GetComponent<DefendController>();
        healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2")){
            defendController.ShieldUp();
        }
        if(Input.GetButtonUp("Fire2")){
            defendController.ShieldDown();
        }
        if(Input.GetButtonDown("Fire1")){
            attackController.Attack();
        }
        if(Input.GetButtonDown("BuyHealth")){
            marketController.BuyHealth();
        }
        //only for testing purposes this should be removed later
        if(Input.GetButtonDown("BuyUpgrade")){
            marketController.BuyUpgrade("TestUpgrade");
            marketController.BuyUpgrade("None");
        }
    }
}
