using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : CustomComponent
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float damageModifier = 1f;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth();
        currentHealth = maxHealth;
        audioManager = GetComponent<AudioManager>();
    }

    virtual public void SetMaxHealth(){}

    virtual public void TakeDamage(float damage){
        //adjust damage for potential shield then take it
        currentHealth -= damage * damageModifier;
        if(damageModifier == 0){
            audioManager.Play("ShieldHit");
        }
        else{
            audioManager.Play("Hurt");
        }
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
        audioManager.Play("Die");
    }
}
