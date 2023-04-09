using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
public enum Difficulty {EASY, MEDIUM, HARD};

    public Difficulty difficulty = Difficulty.MEDIUM;

    public float sfxVolume = 1f;

    public float musicVolume = 0.2f;

    public float mouseSens = 500f;
}
