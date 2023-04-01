using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoundary : MonoBehaviour
{

    IEnumerator Kill(Collider other){
        GameObject go = other.gameObject;
        HealthController hc = go.GetComponent<HealthController>();
        if(hc){
            hc.Die();
        }
        if(go.tag == "DeadEnemy" || go.tag == "Enemy"){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<WalletController>().IncreaseBalance(5f);
            player.GetComponent<AudioManager>().Play("Coin_Pickup");
            yield return new WaitForSeconds(5f);
            Destroy(go);
        }
        yield return new WaitForSeconds(5f);
        // Destroy(other.gameObject);
    }
    
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Kill(other));
    }
}
