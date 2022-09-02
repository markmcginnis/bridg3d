using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class DefendController : MonoBehaviour
{
    public Animator shieldAnimator;
    [Range(0f,1f)]
    public float shieldStrength = 1f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2")){
            Debug.Log("shield up");
            shieldAnimator.SetBool("Defend", true);
            HealthController healthController = GetComponent<HealthController>();
            if(healthController != null){
                healthController.damageModifier = 1f - shieldStrength;
            }
        }
        if(Input.GetButtonUp("Fire2")){
            Debug.Log("shield down");
            shieldAnimator.SetBool("Defend", false);
            HealthController healthController = GetComponent<HealthController>();
            if(healthController != null){
                healthController.damageModifier = 1f;
            }
        }
    }
}
