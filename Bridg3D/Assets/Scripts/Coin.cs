using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float value = 1;

    void OnTriggerEnter(Collider other)
    {
        //only go to player
        WalletController walletController = other.GetComponent<WalletController>();
        if(!walletController)
            return;
        walletController.IncreaseBalance(value);
        GameObject.Destroy(gameObject);
    }
}
