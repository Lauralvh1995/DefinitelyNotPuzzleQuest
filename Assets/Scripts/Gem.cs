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
    // Update is called once per frame
    public GemType type;
    public int score;
    public int damage;
	void Update ()
    {
		
	}
}
