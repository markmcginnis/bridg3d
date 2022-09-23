using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    void OnTriggerEnter(Collider other)
    {
        WalletController walletController = other.GetComponent<WalletController>();
        if(!walletController)
            return;
        walletController.IncreaseBalance(value);
        GameObject.Destroy(gameObject);
    }
}
