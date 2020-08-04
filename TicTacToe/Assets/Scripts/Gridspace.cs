using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Gridspace : MonoBehaviour
{
    private Gameboard _gb;
    private TileIndex _tileIndex;
    private GameController _gameController;

    public GameObject GameBoard;
    public Button B;
    public Text T;

    void Start()
    {
       _gb = GameBoard.GetComponent<Gameboard>();
       _tileIndex = GetComponent<TileIndex>();
    }

    public void SetGameController(GameController controller)
    {
        _gameController = controller;
    }

    public void SetSpace()
    {
        _gb.UpdateGameboard(this.gameObject,1);
    }

    public void UpdateContent()
    {
        T.text = _gameController.GetPlayerMark();
        B.interactable = false;
    }
}
