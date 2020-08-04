using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    /*
     * Difficulty is set by capping the depth of the minimax searh tree
     * The higher the depth, the more possibilities the minimax can play out to find lowest minimizing score
     */

    public GameObject Gb;
    private TicTacToeAI _ai;
    private GameController _gc;

    void Start()
    {
        _ai = Gb.gameObject.GetComponent<TicTacToeAI>();
        _gc = Gb.gameObject.GetComponent<GameController>();
    }

    public void EasyMode()
    {
        _gc.RestartGame();
        _ai.DepthCap = 2;
    }

    public void ModerateMode()
    {
        _gc.RestartGame();
        _ai.DepthCap = 5;
    }

    public void HardMode()
    {
        _gc.RestartGame();
        _ai.DepthCap = 10;
    }
}
