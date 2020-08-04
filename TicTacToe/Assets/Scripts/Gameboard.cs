using UnityEngine;

/// <summary>
/// Implementation for the state of the game
/// </summary>
public class Gameboard : MonoBehaviour
{
    /// <summary>
    /// 2D 3x3 array to represent the game board
    /// </summary>
    public byte[,] BoardArray;

    /// <summary>
    /// Representation of empty space in array
    /// </summary>
    private const byte Empty = 0;

    /// <summary>
    /// Representation of a space held by X
    /// </summary>
    private const byte X = 1;

    /// <summary>
    /// Representation of a space held by O
    /// </summary>
    private const byte O = 2;

    /// <summary>
    /// Boolean that returns true if a space in the array is empty
    /// </summary>
    private bool _isEmpty;
    
    /// <summary>
    /// Reference to the game controller script
    /// </summary>
    public GameController GameController;

    // Start is called before the first frame update
    void Start()
    {
        //Initialise 3x3 array
        BoardArray = new byte[3, 3]
        {
            {0, 1, 2,},
            {3, 4, 5,},
            {6, 7, 8}
        };

        //Loop through array at start of the game and store empty in each
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                BoardArray[i, j] = Empty;
            }
        }
    }

    /// <summary>
    /// Clears Board array when called by replacing each entry of the array with empty
    /// </summary>
    public void ClearArray()
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                BoardArray[i, j] = Empty;
            }
        }
    }

    /// <summary>
    /// Mutator function that updates the board array when for each turn
    /// </summary>
    /// <param name="gridSpace">Button representing the space in the game board</param>
    /// <param name="id">the byte ID of the marker being placed in the game board</param>
    public void UpdateGameboard(GameObject gridSpace, byte id)
    {
        var index = gridSpace.GetComponent<TileIndex>();
        var gs = gridSpace.GetComponent<Gridspace>();

        var xPos = index.GetXPos();
        var yPos = index.GetYPos();

        if (!CheckSpaceIsEmpty(xPos, yPos))
        {
            return;
        }

        if (id == 1)
            BoardArray[xPos, yPos] = X;
        else if (id == 2)
            BoardArray[xPos, yPos] = O;

        gs.UpdateContent();
        GameController.EndTurn();
    }

    /// <summary>
    /// Check to make sure the space is empty in the array
    /// </summary>
    /// <param name="xPos">Row position</param>
    /// <param name="yPos">Column position</param>
    /// <returns>True if space is empty, false if taken</returns>
    public bool CheckSpaceIsEmpty(byte xPos, byte yPos)
    {
        if (BoardArray[xPos, yPos] == Empty)
        {
            _isEmpty = true;
            return _isEmpty;
        }
        return _isEmpty;
    }

    /// <summary>
    /// Checks board array for winning conditions
    /// </summary>
    /// <param name="pt">byte ID for X and O</param>
    /// <returns>True if win condition met for either player</returns>
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