using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public GameObject gameBoard;
    public GameObject gameOverPanel;
    public Text gameOverText;

    private byte playerIdentifier;
    private Gameboard gb;
    private int moveCount = 0;


    void Awake()
    {
        SetGameControllerReference();
        gameOverPanel.SetActive(false);
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
            gameOverPanel.SetActive(true);
            gameOverText.text = GetPlayerText() + " Wins!";
        }
        else if(moveCount >= 9)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "It's a Draw!";
        }
        else
            moveCount++;
    }

    void GameOver()
    {
        foreach (var t in buttonList)
        {
            t.GetComponentInParent<Button>().interactable = false;
        }
    }

    public void RestartGame()
    {
        foreach (var t in buttonList)
        {
            gameOverPanel.SetActive(false);
            gameOverText.text = "";
            t.GetComponentInParent<Button>().interactable = true;
            t.text = "";
            gb.ClearArray();
        }
    }
}
