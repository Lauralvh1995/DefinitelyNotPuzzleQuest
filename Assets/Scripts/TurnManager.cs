using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    public Transform camera;
    public bool moving;
    int turnCount = 0;
    int pass = 0;

    [SerializeField]
    Player[] players;

    [SerializeField]
    private Player activePlayer;

    public bool initializing = true;

    IEnumerator reset;
    private void Awake()
    {
        players = FindObjectsOfType<Player>();
        System.Array.Sort(players);
        System.Array.Reverse(players);

        reset = GetComponent<Board>().ResetBoard();
    }
    public void Initialize()
    {
        activePlayer = players[turnCount];
        foreach (Player p in players)
        {
            p.AddHP(20);
            if (p != activePlayer)
            {
                p.canvas.enabled = false;
            }
        }
        initializing = false;
    }

    public void Pass()
    {
        pass++;
        if(pass > 3)
        {
            ResetPass();
            StartCoroutine(reset);
        }
        NextTurn();
    }
    public void ResetPass()
    {
        pass = 0;
    }

    public void NextTurn()
    {
        turnCount++;
        if(turnCount >3)
        {
            turnCount = 0;
        }
        activePlayer = players[turnCount];
        activePlayer.canvas.enabled = true;
        foreach(Player p in players)
        {
            if(p != activePlayer)
            {
                p.canvas.enabled = false;
            }
        }
        // call it with StartCoroutine:
        if (!moving)
        { // never start a new MoveObject while it's already running!
            Quaternion rotationAmount = Quaternion.Euler(0, 90, 0);
            Quaternion postRotation = camera.rotation * rotationAmount;
            StartCoroutine(TurnCamera(camera, camera.rotation, postRotation, 1));
        }
        if (activePlayer.defeated)
        {
            Pass();
        }
    }

    IEnumerator TurnCamera(Transform thisTransform, Quaternion startRot, Quaternion endRot, float time)
    {
        moving = true; // MoveObject started
        float i = 0;
        float rate = 1 / time;
        while (i < 1)
        {
            i += Time.deltaTime * rate;
            thisTransform.rotation = Quaternion.Slerp(startRot, endRot, i);
            yield return 0;
        }
        moving = false; // MoveObject ended
    }

    public Player GetActivePlayer()
    {
        return activePlayer;
    }
}
