using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : HealthController
{
    public GameObject damageIndicator;

    public override void Die()
    {
        //disable components to see outcome
        base.Die();
        currentHealth = 0;
        GetComponent<AttackController>().enabled = false;
        GetComponent<HealthController>().enabled = false;
        GetComponent<DefendController>().enabled = false;
        GetComponent<FPSMovement>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponentInChildren<MouseLook>().enabled = false;
        //may need to add more like quick timer than go to lose menu or whatever else
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if(damageModifier == 0f)
            return;
        StartCoroutine(InvincibilityTimer(0.2f,damageModifier));
    }

    IEnumerator InvincibilityTimer(float time, float originalDamagerModifier){
        damageModifier = 0f;
        damageIndicator.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        damageIndicator.SetActive(false);
        yield return new WaitForSeconds(time);
        damageModifier = originalDamagerModifier;
        yield return null;
    }
}
