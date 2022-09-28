using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
//inherent dynamic upgrading ability
public class DefendController : CustomComponent
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
        //monitor shield cooldown
        shieldTime = Mathf.Clamp(shieldTime - Time.deltaTime, -0.1f, shieldCooldown);
    }

    public void ShieldUp(){
        //make sure cooldown has cooled down
        if(shieldTime > 0)
            return;
        shieldAnimator.SetBool("Defend", true);
        //modify taking of damage to inverse of strength
        healthController.damageModifier = 1f - shieldStrength;
    }

    public void ShieldDown(){
        //if currently defending, reset cooldown
        if(shieldAnimator.GetBool("Defend"))
            shieldTime = shieldCooldown;
        //stop defending
        shieldAnimator.SetBool("Defend", false);
        //reset taking of damage
        healthController.damageModifier = 1f;
    }

    public bool IsShieldUp(){
        //simple thing for others to use
        return shieldAnimator.GetBool("Defend");
    }
}
