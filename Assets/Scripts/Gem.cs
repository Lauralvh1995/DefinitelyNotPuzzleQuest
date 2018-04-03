using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GemType
{
    Red,
    Green,
    Blue,
    Yellow,
    Purple,
    Orange,
    White
}
public class Gem : MonoBehaviour {
    public GemType type;
    public int score;
    public int damage;
}
