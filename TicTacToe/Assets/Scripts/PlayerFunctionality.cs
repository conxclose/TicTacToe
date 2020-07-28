using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFunctionality : MonoBehaviour
{
    public GameObject gameBoard;
    private Gameboard gb;
    public Button b;
    public Text t;
    public string playerIndentity;

    void Start()
    {
       gb = gameBoard.GetComponent<Gameboard>();
    }

    public void SetSpace()
    {
        if (gb.CheckSpaceIsEmpty(0, 0))
        {
            Debug.Log("EMPTY");
            t.text = playerIndentity;
            b.interactable = false;
        }
    }
}
