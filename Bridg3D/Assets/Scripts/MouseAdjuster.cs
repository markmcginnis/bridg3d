using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAdjuster : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.Log("MOUSE ADJUSTED");
        Cursor.lockState = CursorLockMode.None;
    }
}
