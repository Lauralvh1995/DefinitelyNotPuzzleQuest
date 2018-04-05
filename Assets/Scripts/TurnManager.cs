using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    public Transform camera;
    bool moving;
    int turnCount = 0;
    int pass = 0;

    [SerializeField]
    Player[] players;

    public Canvas player1;
    public Canvas player2;
    public Canvas player3;
    public Canvas player4;

    IEnumerator reset;
    private void Awake()
    {
        players = FindObjectsOfType<Player>();
        player2.enabled = false;
        player3.enabled = false;
        player4.enabled = false;

        reset = GetComponent<Board>().ResetBoard();
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
        switch (turnCount) {
            case 0:
            {
                    player1.enabled = true;
                    player2.enabled = false;
                    player3.enabled = false;
                    player4.enabled = false;
                    break;
            }
            case 1:
            {
                    player1.enabled = false;
                    player2.enabled = true;
                    player3.enabled = false;
                    player4.enabled = false;
                    break;
            }
            case 2:
            {
                    player1.enabled = false;
                    player2.enabled = false;
                    player3.enabled = true;
                    player4.enabled = false;
                    break;
            }
            case 3:
            {
                    player1.enabled = false;
                    player2.enabled = false;
                    player3.enabled = false;
                    player4.enabled = true;
                    break;
            }
        }
        // call it with StartCoroutine:
        if (!moving)
        { // never start a new MoveObject while it's already running!
            Quaternion rotationAmount = Quaternion.Euler(0, 90, 0);
            Quaternion postRotation = camera.rotation * rotationAmount;
            StartCoroutine(TurnCamera(camera, camera.rotation, postRotation, 1));
        }
        GetComponent<Board>().ChangeDirection((Direction)turnCount);
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
}
