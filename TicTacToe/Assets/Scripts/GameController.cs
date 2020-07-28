using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] ButtonList;
    public GameObject GameBoard;
    public GameObject GameOverPanel;
    public Text GameOverText;

    private byte _playerId;
    private int _moveCount = 0;

    private Gameboard _gb;
    private TicTacToeAI _ai;

    void Awake()
    {
        SetGameControllerReference();
        GameOverPanel.SetActive(false);
        _playerId = 1;
        _gb = GameBoard.GetComponent<Gameboard>();
        _ai = this.gameObject.GetComponent<TicTacToeAI>();
    }

    void SetGameControllerReference()
    {
        foreach (var t in ButtonList)
            t.GetComponentInParent<Gridspace>().SetGameController(this);
    }

    public string GetPlayerMark()
    {
        if (_playerId == 1)
            return "X";

        if (_playerId == 2)
            return "O";

        return "!!";
    }

    public byte GetPlayerID()
    {
        return _playerId;
    }

    public void EndTurn()
    {
        _moveCount++;
        if (_gb.CheckForWinningMove(_playerId))
        {
            GameOver();
            GameOverPanel.SetActive(true);
            GameOverText.text = GetPlayerMark() + " Wins!";
        }
        else if(_moveCount >= 9)
        {
            GameOverPanel.SetActive(true);
            GameOverText.text = "It's a Draw!";
        }

        if (_playerId == 1)
        {
            _playerId = 2;
            _ai.MakeMove();
        }
        else if (_playerId == 2)
            _playerId = 1;
    }

    void GameOver()
    {
        foreach (var t in ButtonList)
        {
            t.GetComponentInParent<Button>().interactable = false;
        }
    }

    public void RestartGame()
    {
        foreach (var t in ButtonList)
        {
            _moveCount = 0;
            GameOverPanel.SetActive(false);
            GameOverText.text = "";
            t.GetComponentInParent<Button>().interactable = true;
            t.text = "";
            _gb.ClearArray();
        }
    }
}
