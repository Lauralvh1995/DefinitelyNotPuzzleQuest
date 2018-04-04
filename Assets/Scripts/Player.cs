using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Color
{
    Red,
    Blue,
    Green,
    Yellow
}

public class Player : MonoBehaviour {
    public BarController bar;
    int currentHP;
    int currentRed;
    int currentBlue;
    int currentGreen;
    int currentYellow;

    [SerializeField] Color playerColor;

    private void Update()
    {
        currentHP = bar.GetHP();
        currentRed = bar.GetRed();
        currentBlue = bar.GetBlue();
        currentGreen = bar.GetGreen();
        currentYellow = bar.GetYellow();
    }
    public void AttackLeft()
    {
        switch (playerColor)
        {
            case Color.Red:
                {

                    break;
                }
            case Color.Blue:
                {
                    break;
                }
            case Color.Green:
                {
                    break;
                }
            case Color.Yellow:
                {
                    break;
                }
        }
    }
    public void AttackAcross()
    {
        switch (playerColor)
        {
            case Color.Red:
                {
                    break;
                }
            case Color.Blue:
                {
                    break;
                }
            case Color.Green:
                {
                    break;
                }
            case Color.Yellow:
                {
                    break;
                }
        }
    }
    public void AttackRight()
    {
        switch (playerColor)
        {
            case Color.Red:
                {
                    break;
                }
            case Color.Blue:
                {
                    break;
                }
            case Color.Green:
                {
                    break;
                }
            case Color.Yellow:
                {
                    break;
                }
        }
    }
    public void Attack()
    {
        switch (playerColor)
        {
            case Color.Red:
                {
                    break;
                }
            case Color.Blue:
                {
                    break;
                }
            case Color.Green:
                {
                    break;
                }
            case Color.Yellow:
                {
                    break;
                }
        }
    }
    public void Heal()
    {
        switch (playerColor)
        {
            case Color.Red:
                {
                    break;
                }
            case Color.Blue:
                {
                    break;
                }
            case Color.Green:
                {
                    break;
                }
            case Color.Yellow:
                {
                    break;
                }
        }
    }
}
