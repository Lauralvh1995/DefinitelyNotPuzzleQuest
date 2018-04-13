using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Color
{
    Red,
    Blue,
    Green,
    Yellow
}

public class Player : MonoBehaviour, IComparable<Player> {
    public BarController bar;
    public bool defeated = false;
    public Material defeatedMat;
    [SerializeField] int score;

    int currentHP;
    int currentRed;
    int currentBlue;
    int currentGreen;
    int currentYellow;

    [SerializeField] Color playerColor;

    public Canvas canvas;
    public Text scoreBoard;

    private void Update()
    {
        currentHP = bar.GetHP();
        currentRed = bar.GetRed();
        currentBlue = bar.GetBlue();
        currentGreen = bar.GetGreen();
        currentYellow = bar.GetYellow();

        if (currentHP <= 0 && FindObjectOfType<TurnManager>().initializing == false)
        {
            defeated = true;
            GetComponentInChildren<MeshRenderer>().material = defeatedMat;
        }
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
        if(currentRed > 4 && !defeated)
        {
            currentRed -= 5;
            bar.SetRed(currentRed);
            int otherHP = FindPlayer(Color.Red).currentHP -= 5;
            FindPlayer(Color.Red).bar.SetHP(otherHP);
            FindObjectOfType<TurnManager>().ResetPass();
            FindObjectOfType<TurnManager>().NextTurn();
        }
        else
        {
            FindObjectOfType<TurnManager>().Pass();
        }
    }
    public void AttackBlue()
    {
        if (currentBlue > 4 && !defeated)
        {
            currentBlue -= 5;
            bar.SetBlue(currentBlue);
            int otherHP = FindPlayer(Color.Blue).currentHP -= 5;
            FindPlayer(Color.Blue).bar.SetHP(otherHP);
            FindObjectOfType<TurnManager>().ResetPass();
            FindObjectOfType<TurnManager>().NextTurn();
        }
        else
        {
            FindObjectOfType<TurnManager>().Pass();
        }
    }
    public void AttackGreen()
    {
        if (currentGreen > 4 && !defeated)
        {
            currentGreen -= 5;
            bar.SetGreen(currentGreen);
            int otherHP = FindPlayer(Color.Green).currentHP -= 5;
            FindPlayer(Color.Green).bar.SetHP(otherHP);
            FindObjectOfType<TurnManager>().ResetPass();
            FindObjectOfType<TurnManager>().NextTurn();
        }
        else
        {
            FindObjectOfType<TurnManager>().Pass();
        }
    }
    public void AttackYellow()
    {
        if (currentYellow > 4 && !defeated)
        {
            currentYellow -= 5;
            bar.SetYellow(currentYellow);
            int otherHP = FindPlayer(Color.Yellow).currentHP -= 5;
            FindPlayer(Color.Yellow).bar.SetHP(otherHP);
            FindObjectOfType<TurnManager>().ResetPass();
            FindObjectOfType<TurnManager>().NextTurn();

        }
        else
        {
            FindObjectOfType<TurnManager>().Pass();
        }
    }
    public void MatchAttack(int damage)
    {
        int otherHP;
        switch (playerColor)
        {
            case Color.Red:
                {
                    otherHP = FindPlayer(Color.Green).currentHP -= damage;
                    FindPlayer(Color.Green).bar.SetHP(otherHP);
                    break;
                }
            case Color.Blue:
                {
                    otherHP = FindPlayer(Color.Yellow).currentHP -= damage;
                    FindPlayer(Color.Yellow).bar.SetHP(otherHP);
                    break;
                }
            case Color.Green:
                {
                    otherHP = FindPlayer(Color.Red).currentHP -= damage;
                    FindPlayer(Color.Red).bar.SetHP(otherHP);
                    break;
                }
            case Color.Yellow:
                {
                    otherHP = FindPlayer(Color.Blue).currentHP -= damage;
                    FindPlayer(Color.Blue).bar.SetHP(otherHP);
                    break;
                }
        }
    }
    public void ButtonHeal()
    {
        if(defeated)
        {
            FindObjectOfType<TurnManager>().Pass();
        }
        int otherHP;
        switch (playerColor)
        {
            case Color.Red:
                {
                    if (currentRed > 4)
                    {
                        currentRed -= 5;
                        bar.SetRed(currentRed);
                        otherHP = FindPlayer(Color.Red).currentHP += 5;
                        FindPlayer(Color.Red).bar.SetHP(otherHP);
                        FindObjectOfType<TurnManager>().ResetPass();
                        FindObjectOfType<TurnManager>().NextTurn();
                    }
                    break;
                }
            case Color.Blue:
                {
                    if (currentBlue > 4)
                    {
                        currentBlue -= 5;
                        bar.SetBlue(currentBlue);
                        otherHP = FindPlayer(Color.Blue).currentHP += 5;
                        FindPlayer(Color.Blue).bar.SetHP(otherHP);
                        FindObjectOfType<TurnManager>().ResetPass();
                        FindObjectOfType<TurnManager>().NextTurn();
                    }
                    break;
                }
            case Color.Green:
                {
                    if (currentGreen > 4)
                    {
                        currentGreen -= 5;
                        bar.SetGreen(currentGreen);
                        otherHP = FindPlayer(Color.Green).currentHP += 5;
                        FindPlayer(Color.Green).bar.SetHP(otherHP);
                        FindObjectOfType<TurnManager>().ResetPass();
                        FindObjectOfType<TurnManager>().NextTurn();
                    }
                    break;
                }
            case Color.Yellow:
                {
                    if (currentYellow > 4)
                    {
                        currentYellow -= 5;
                        bar.SetYellow(currentYellow);
                        otherHP = FindPlayer(Color.Yellow).currentHP += 5;
                        FindPlayer(Color.Yellow).bar.SetHP(otherHP);
                        FindObjectOfType<TurnManager>().ResetPass();
                        FindObjectOfType<TurnManager>().NextTurn();
                    }
                    break;
                }
        }
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
        scoreBoard.text = score.ToString();
    }
    public int GetScore()
    {
        return score;
    }

    public void AddRed(int i)
    {
        currentRed += i;
        bar.SetRed(currentRed);
    }
    public void AddBlue(int i)
    {
        currentBlue += i;
        bar.SetBlue(currentBlue);
    }
    public void AddGreen(int i)
    {
        currentGreen += i;
        bar.SetGreen(currentGreen);
    }
    public void AddYellow(int i)
    {
        currentYellow += i;
        bar.SetYellow(currentYellow);
    }
    public void AddHP(int i)
    {
        currentHP += i;
        bar.SetHP(currentHP);
    }
}
