using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void setHealth(float health){
        slider.value = health;
    }

    public void setMaxHealth(float maxHealth){
        slider.maxValue = maxHealth;
        setHealth(maxHealth);
    }

    public void Die(){
        this.transform.Find("Heart").gameObject.SetActive(false);
        this.transform.Find("Skull").gameObject.SetActive(true);
    }
}
