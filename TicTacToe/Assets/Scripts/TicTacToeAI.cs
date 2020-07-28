using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeAI : MonoBehaviour
{
    private byte aiID;
    private string aiMarker;

    void Awake()
    {
        aiID = 2;
        aiMarker = "O";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
