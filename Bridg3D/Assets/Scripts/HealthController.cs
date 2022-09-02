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

    // Update is called once per frame
    void Update()
    {
        
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

    void Die(){
        Debug.Log("DEAD");
        gameObject.SetActive(false);
        //may need to move to be player/enemy specific
        //^^ maybe just have additional specific PlayerHealthController inherit from HealthController
    }
}
