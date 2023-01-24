using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
public enum Difficulty {EASY, MEDIUM, HARD};

    public Difficulty difficulty = Difficulty.MEDIUM;

    public float audioVolume = 1f;

    public float mouseSens = 100f;
}
