using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerFunctionality : MonoBehaviour
{
    private Gameboard gb;
    private TileIndex ti;

    public GameObject gameBoard;
    public Button b;
    public Text t;
    public string playerIndentity;

    void Start()
    {
       gb = gameBoard.GetComponent<Gameboard>();
       ti = GetComponent<TileIndex>();
    }

    public void SetSpace()
    {
        byte xPos, yPos;
        xPos = ti.GetXPos();
        yPos = ti.GetYPos();

        if (gb.CheckSpaceIsEmpty(xPos, yPos))
        {
            Debug.Log("EMPTY");
            //t.text = playerIndentity;
           // b.interactable = false;
        }
    }
}
