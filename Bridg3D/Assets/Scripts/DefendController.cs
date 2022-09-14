using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class DefendController : MonoBehaviour
{
    public Animator shieldAnimator;
    [Range(0f,1f)]
    public float shieldStrength = 1f;

    HealthController healthController;

    void Start() {
        healthController = GetComponent<HealthController>();
    }

    public void ShieldUp(){
        Debug.Log("shield up");
            shieldAnimator.SetBool("Defend", true);
            if(healthController != null){
                healthController.damageModifier = 1f - shieldStrength;
            }
    }

    public void ShieldDown(){
        Debug.Log("shield down");
            shieldAnimator.SetBool("Defend", false);
            if(healthController != null){
                healthController.damageModifier = 1f;
            }
    }
}
