using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Experimental.ParticleSystemJobs;

public class Gameboard : MonoBehaviour
{
    private byte[,] boardArray;

    private const byte empty = 0;
    private const byte x = 1;
    private const byte o = 2;

    bool isEmpty = false;

    // Start is called before the first frame update
    void Start()
    {
        boardArray = new byte[3, 3] {
            {0, 1, 2,} ,
            {3, 4, 5,} ,
            {6, 7, 8}
        };

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                boardArray[i, j] = empty;
            }
        }
    }

    void UpdateGameboard(byte xPos, byte yPos, byte symbol)
    {
        if (symbol == x)
            boardArray[xPos, yPos] = x;
        else
            boardArray[xPos, yPos] = o;
    }

    public bool CheckSpaceIsEmpty(byte xPos, byte yPos)
    {
        if (boardArray[xPos, yPos] == empty)
        {
            isEmpty = true;
            return isEmpty;
        }
        else
            return isEmpty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
