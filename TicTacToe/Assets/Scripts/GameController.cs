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

    private byte playerID;
    private string playerMark;
    private int moveCount = 0;

    private Gameboard gb;

    void Awake()
    {
        SetGameControllerReference();
        gameOverPanel.SetActive(false);
        playerID = 1;
        playerMark = "X";
        gb = gameBoard.GetComponent<Gameboard>();
    }

    void SetGameControllerReference()
    {
        foreach (var t in buttonList)
            t.GetComponentInParent<Gridspace>().SetGameController(this);
    }

    public string GetPlayerMark()
    {
        return playerMark;
    }

    public void EndTurn()
    {
        moveCount++;
        if (gb.CheckForWinningMove(playerID))
        {
            GameOver();
            gameOverPanel.SetActive(true);
           // gameOverText.text = playerMark + " Wins!";
        }
        else if(moveCount >= 9)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "It's a Draw!";
        }
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
            moveCount = 0;
            gameOverPanel.SetActive(false);
            gameOverText.text = "";
            t.GetComponentInParent<Button>().interactable = true;
            t.text = "";
            gb.ClearArray();
        }
    }
}
