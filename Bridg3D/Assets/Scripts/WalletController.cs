using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletController : MonoBehaviour
{
    int balance = 0;

    public void IncreaseBalance(int value){
        balance += value;
    }

    public void DecreaseBalance(int value){
        balance -= value;
    }

    public int GetBalance(){
        return balance;
    }

    public bool CanAfford(int value){
        return value >= balance;
    }
}
