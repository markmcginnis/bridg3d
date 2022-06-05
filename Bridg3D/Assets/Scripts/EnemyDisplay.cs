using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDisplay : MonoBehaviour
{
    public void setText(int enemyNumber)
    {
        GetComponent<Text>().text = "Enemies Left: " + (enemyNumber);
    }
}
