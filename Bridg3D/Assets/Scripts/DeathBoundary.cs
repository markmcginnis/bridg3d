using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoundary : MonoBehaviour
{

    IEnumerator Kill(Collider other){
        HealthController hc = other.GetComponent<HealthController>();
        if(hc){
            hc.Die();
        }
        yield return new WaitForSeconds(5f);
        Destroy(other.gameObject);
    }
    
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Kill(other));
    }
}
