using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public GameObject gameBoard;

    private byte playerIdentifier;
    private Gameboard gb;


    void Awake()
    {
        SetGameControllerReference();
        playerIdentifier = 1;
        gb = gameBoard.GetComponent<Gameboard>();
    }

    void SetGameControllerReference()
    {
        foreach (var t in buttonList)
            t.GetComponentInParent<Gridspace>().SetGameController(this);
    }

    public byte GetPlayerIdentifier()
    {
        return playerIdentifier;
    }

    public string GetPlayerText()
    {
        var identifier = "DEFAULT";

        if (playerIdentifier == 1)
            identifier = "X";
        else if (playerIdentifier == 2)
            identifier = "O";

        return identifier;
    }

    public void EndTurn()
    {
        if (gb.CheckForWinningMove(playerIdentifier))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        foreach (var t in buttonList)
        {
            t.GetComponentInParent<Button>().interactable = false;
        }
    }
}
