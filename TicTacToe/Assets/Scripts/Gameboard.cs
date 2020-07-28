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

    private bool isEmpty = false;

    [SerializeField]
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        boardArray = new byte[3, 3]
        {
            {0, 1, 2,},
            {3, 4, 5,},
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

    public void ClearArray()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                boardArray[i, j] = empty;
            }
        }
    }

    public void UpdateGameboard(GameObject gridSpace, byte id)
    {
        var index = gridSpace.GetComponent<TileIndex>();
        var gs = gridSpace.GetComponent<Gridspace>();

        byte xPos, yPos;
        xPos = index.GetXPos();
        yPos = index.GetYPos();

        if (!CheckSpaceIsEmpty(xPos, yPos))
        {
            return;
        }

        if (id == 1)
            boardArray[xPos, yPos] = x;
        else if (id == 2)
            boardArray[xPos, yPos] = o;

        gs.UpdateContent();
        gameController.EndTurn();
    }

    public bool CheckSpaceIsEmpty(byte xPos, byte yPos)
    {
        if (boardArray[xPos, yPos] == empty)
        {
            isEmpty = true;
            return isEmpty;
        }
        return isEmpty;
    }

    public bool CheckForWinningMove(byte pt)
    {
        //Horizontal Top
        if (boardArray[0, 0] == pt && boardArray[1, 0] == pt && boardArray[2, 0] == pt)
            return true;
        //Horizontal Middle
        else if (boardArray[0, 1] == pt && boardArray[1, 1] == pt && boardArray[2, 1] == pt)
            return true;
        //Horizontal Bottom
        else if (boardArray[0, 2] == pt && boardArray[1, 2] == pt && boardArray[2, 2] == pt)
            return true;
        //Diagonal L-R
        else if (boardArray[0, 0] == pt && boardArray[1, 1] == pt && boardArray[2, 2] == pt)
            return true;
        //Diagonal R-:
        else if (boardArray[2, 0] == pt && boardArray[1, 1] == pt && boardArray[0, 2] == pt)
            return true;
        //Vertical Left
        else if (boardArray[0, 0] == pt && boardArray[0, 1] == pt && boardArray[0, 2] == pt)
            return true;
        //Vertical Middle
        else if (boardArray[1, 0] == pt && boardArray[1, 1] == pt && boardArray[1, 2] == pt)
            return true;
        //Vertical Right
        else if (boardArray[2, 0] == pt && boardArray[2, 1] == pt && boardArray[2, 2] == pt)
            return true;
        else
            return false;
    }
}