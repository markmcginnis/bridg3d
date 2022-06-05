using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveDisplay : MonoBehaviour
{
    public void setText(int waveNumber)
    {
        GetComponent<Text>().text = "Wave: " + (waveNumber + 1);
    }
}
