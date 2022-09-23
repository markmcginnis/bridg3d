using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class DefendController : MonoBehaviour
{
    public Animator shieldAnimator;
    [Range(0f,1f)]
    public float shieldStrength = 1f;
    public float shieldCooldown = 2.5f;

    float shieldTime;

    HealthController healthController;

    void Start() {
        healthController = GetComponent<HealthController>();
        shieldTime = 0f;
    }

    void Update() {
        shieldTime = Mathf.Clamp(shieldTime - Time.deltaTime, -0.1f, shieldCooldown);
    }

    public void ShieldUp(){
        if(shieldTime > 0)
            return;
        shieldAnimator.SetBool("Defend", true);
        if(healthController != null){
            healthController.damageModifier = 1f - shieldStrength;
        }
    }

    public void ShieldDown(){
        if(shieldAnimator.GetBool("Defend"))
            shieldTime = shieldCooldown;
        shieldAnimator.SetBool("Defend", false);
        if(healthController != null){
            healthController.damageModifier = 1f;
        }
    }

    public bool IsShieldUp(){
        return shieldAnimator.GetBool("Defend");
    }
}
