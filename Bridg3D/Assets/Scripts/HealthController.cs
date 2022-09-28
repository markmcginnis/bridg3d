using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : CustomComponent
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float damageModifier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage){
        //adjust damage for potential shield then take it
        currentHealth -= damage * damageModifier;
        if(currentHealth <= 0){
            //can only die after taking damage
            Die();
        }
    }

    public void Heal(float amount){
        //heal the amount but don't go over the maxHealth
        currentHealth = Mathf.Clamp(currentHealth + amount, currentHealth, maxHealth);
    }

    virtual public void Die(){
        //may need to update with general dying stuff
        Debug.Log(gameObject.name + " DEAD");
    }
}
