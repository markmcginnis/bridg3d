using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    public float value = 5;

    void OnTriggerEnter(Collider other)
    {
        //only go to player
        PlayerHealthController healthController = other.GetComponent<PlayerHealthController>();
        if(!healthController)
            return;
        healthController.Heal(value);
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play("Health_Pickup");
        GameObject.Destroy(gameObject);
    }
}
