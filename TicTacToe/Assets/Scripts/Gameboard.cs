using UnityEngine;

public class Gameboard : MonoBehaviour
{
    public byte[,] BoardArray;
    private const byte empty = 0;
    private const byte x = 1;
    private const byte o = 2;

    private bool isEmpty;
    
    public GameController GameController;

    // Start is called before the first frame update
    void Start()
    {
        BoardArray = new byte[3, 3]
        {
            {0, 1, 2,},
            {3, 4, 5,},
            {6, 7, 8}
        };

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                BoardArray[i, j] = empty;
            }
        }
    }

    public void ClearArray()
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                BoardArray[i, j] = empty;
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
            BoardArray[xPos, yPos] = x;
        else if (id == 2)
            BoardArray[xPos, yPos] = o;

        gs.UpdateContent();
        GameController.EndTurn();
    }

    public bool CheckSpaceIsEmpty(byte xPos, byte yPos)
    {
        if (BoardArray[xPos, yPos] == empty)
        {
            isEmpty = true;
            return isEmpty;
        }
        return isEmpty;
    }

    public bool CheckForWinningMove(byte pt)
    {
        //Horizontal Top
        if (BoardArray[0, 0] == pt && BoardArray[1, 0] == pt && BoardArray[2, 0] == pt)
            return true;
        //Horizontal Middle
        if (BoardArray[0, 1] == pt && BoardArray[1, 1] == pt && BoardArray[2, 1] == pt)
            return true;
        //Horizontal Bottom
        if (BoardArray[0, 2] == pt && BoardArray[1, 2] == pt && BoardArray[2, 2] == pt)
            return true;
        //Diagonal L-R
        if (BoardArray[0, 0] == pt && BoardArray[1, 1] == pt && BoardArray[2, 2] == pt)
            return true;
        //Diagonal R-L
        if (BoardArray[2, 0] == pt && BoardArray[1, 1] == pt && BoardArray[0, 2] == pt)
            return true;
        //Vertical Left
        if (BoardArray[0, 0] == pt && BoardArray[0, 1] == pt && BoardArray[0, 2] == pt)
            return true;
        //Vertical Middle
        if (BoardArray[1, 0] == pt && BoardArray[1, 1] == pt && BoardArray[1, 2] == pt)
            return true;
        //Vertical Right
        if (BoardArray[2, 0] == pt && BoardArray[2, 1] == pt && BoardArray[2, 2] == pt)
            return true;
        return false;
    }
}