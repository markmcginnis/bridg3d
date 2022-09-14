using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
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
        currentHealth -= damage * damageModifier;
        if(currentHealth <= 0){
            Die();
        }
    }

    public void Heal(float amount){
        currentHealth = Mathf.Clamp(currentHealth + amount, currentHealth, maxHealth);
    }

    virtual public void Die(){
        Debug.Log("DEAD");
    }
}
