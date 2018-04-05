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
        }
    }
    public void AttackBlue()
    {
        if (currentBlue > 4)
        {
            bar.ChangeBlue(-5);
            FindPlayer(Color.Blue).bar.ChangeHP(-5);
            FindObjectOfType<TurnManager>().ResetPass();
        }
    }
    public void AttackGreen()
    {
        if (currentGreen > 4)
        {
            bar.ChangeGreen(-5);
            FindPlayer(Color.Green).bar.ChangeHP(-5);
            FindObjectOfType<TurnManager>().ResetPass();
        }
    }
    public void AttackYellow()
    {
        if (currentYellow > 4)
        {
            bar.ChangeYellow(-5);
            FindPlayer(Color.Yellow).bar.ChangeHP(-5);
            FindObjectOfType<TurnManager>().ResetPass();
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
    }
}
