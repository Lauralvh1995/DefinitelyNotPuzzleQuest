using System;
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

public class Player : MonoBehaviour, IComparable<Player> {
    public BarController bar;
    [SerializeField] int score;

    int currentHP;
    int currentRed;
    int currentBlue;
    int currentGreen;
    int currentYellow;

    [SerializeField] Color playerColor;

    public Canvas canvas;

    private void Update()
    {
        currentHP = bar.GetHP();
        currentRed = bar.GetRed();
        currentBlue = bar.GetBlue();
        currentGreen = bar.GetGreen();
        currentYellow = bar.GetYellow();
    }
    Player FindPlayer(Color color)
    {
        foreach(Player p in FindObjectsOfType<Player>())
        {
            if(p.playerColor == color)
            {
                return p;
            }
        }
        throw new System.ArgumentNullException();
    }
    public void AttackRed()
    {
        if(currentRed >4)
        {
            bar.ChangeRed(-5);
            FindPlayer(Color.Red).bar.ChangeHP(-5);
            FindObjectOfType<TurnManager>().ResetPass();
            FindObjectOfType<TurnManager>().NextTurn();
        }
    }
    public void AttackBlue()
    {
        if (currentBlue > 4)
        {
            bar.ChangeBlue(-5);
            FindPlayer(Color.Blue).bar.ChangeHP(-5);
            FindObjectOfType<TurnManager>().ResetPass();
            FindObjectOfType<TurnManager>().NextTurn();
        }
    }
    public void AttackGreen()
    {
        if (currentGreen > 4)
        {
            bar.ChangeGreen(-5);
            FindPlayer(Color.Green).bar.ChangeHP(-5);
            FindObjectOfType<TurnManager>().ResetPass();
            FindObjectOfType<TurnManager>().NextTurn();
        }
    }
    public void AttackYellow()
    {
        if (currentYellow > 4)
        {
            bar.ChangeYellow(-5);
            FindPlayer(Color.Yellow).bar.ChangeHP(-5);
            FindObjectOfType<TurnManager>().ResetPass();
            FindObjectOfType<TurnManager>().NextTurn();
        }
    }
    public void MatchAttack()
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
        FindObjectOfType<TurnManager>().ResetPass();
        FindObjectOfType<TurnManager>().NextTurn();
    }
    public void ButtonHeal()
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
    public void MatchHeal()
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
        FindObjectOfType<TurnManager>().ResetPass();
        FindObjectOfType<TurnManager>().NextTurn();
    }

    public int CompareTo(Player y)
    {
        switch(playerColor)
        {
            case Color.Red:
                {
                    if (y.playerColor == Color.Blue || y.playerColor == Color.Green || y.playerColor == Color.Yellow)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            case Color.Blue:
                {
                    if (y.playerColor == Color.Green || y.playerColor == Color.Yellow)
                    {
                        return 1;
                    }
                    else if(y.playerColor == Color.Red)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            case Color.Green:
                {
                    if (y.playerColor == Color.Yellow)
                    {
                        return 1;
                    }
                    else if (y.playerColor == Color.Red || y.playerColor == Color.Blue)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            case Color.Yellow:
                {
                    if (y.playerColor == Color.Red || y.playerColor == Color.Blue)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
        }
        return 0;
    }

    public void AddScore(int s)
    {
        score += s;
    }
    public int GetScore()
    {
        return score;
    }
}
