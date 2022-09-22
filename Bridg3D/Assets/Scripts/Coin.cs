using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    void OnTriggerEnter(Collider other)
    {
        if(!other.GetComponent<WalletController>())
            return;
        other.GetComponent<WalletController>().IncreaseBalance(value);
        GameObject.Destroy(gameObject);
    }
}
