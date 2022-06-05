using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletDisplay : MonoBehaviour
{
    public void setText(int wallet)
    {
        GetComponent<Text>().text = "Coins: " + wallet;
    }
}
