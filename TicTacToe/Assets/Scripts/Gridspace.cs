using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Gridspace : MonoBehaviour
{
    private Gameboard gb;
    private TileIndex tileIndex;
    private GameController gameController;

    public GameObject gameBoard;
    public Button b;
    public Text t;

    void Start()
    {
       gb = gameBoard.GetComponent<Gameboard>();
       tileIndex = GetComponent<TileIndex>();
    }

    public void SetGameController(GameController controller)
    {
        gameController = controller;
    }

    public void SetSpace()
    {
        byte xPos, yPos;
        xPos = tileIndex.GetXPos();
        yPos = tileIndex.GetYPos();

        if (gb.CheckSpaceIsEmpty(xPos, yPos))
        {
            //Debug.Log("EMPTY");
            gb.UpdateGameboard(xPos,yPos,gameController.GetPlayerIdentifier());
            t.text = gameController.GetPlayerText();
            b.interactable = false;
            gameController.EndTurn();
        }
    }
}
