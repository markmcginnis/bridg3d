using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletController : MonoBehaviour
{
    [SerializeField]
    float balance = 0;

    public void IncreaseBalance(float value){
        balance += value;
    }

    public void DecreaseBalance(float value){
        balance -= value;
    }

    public float GetBalance(){
        return balance;
    }

    public bool CanAfford(float value){
        return value <= balance;
    }
}
