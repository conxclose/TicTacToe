using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Implementation for the behaviour of the game
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// Reference to the text of each button in the array
    /// </summary>
    public Text[] ButtonList;

    /// <summary>
    /// Reference to the game object that hosts the game board
    /// </summary>
    public GameObject GameBoard;

    /// <summary>
    /// Reference to the Game Over Panel object
    /// </summary>
    public GameObject GameOverPanel;

    /// <summary>
    /// Reference to the text of the game over panel
    /// </summary>
    public Text GameOverText;

    /// <summary>
    /// Reference to the player byte ID from game board 
    /// </summary>
    private byte _playerId;

    /// <summary>
    /// Track the number of moves for possible draw condition
    /// </summary>
    private int _moveCount = 0;

    /// <summary>
    /// Reference to the game board script
    /// </summary>
    private Gameboard _gb;

    /// <summary>
    /// Reference to the AI script
    /// </summary>
    private TicTacToeAI _ai;

    /*
     * At the start of the game
     * Initialise player as X (id = 1)
     * Set game to easy mode (depth = 2)
     * Hide GameOver Panel
     * Calls setgamecontroller reference to apply gamecontroller functionality to each button
     */
    void Start()
    {
        SetGameControllerReference();
        GameOverPanel.SetActive(false);
        _playerId = 1;
        _gb = GameBoard.GetComponent<Gameboard>();
        _ai = this.gameObject.GetComponent<TicTacToeAI>();
        _ai.DepthCap = 2;
    }

    /// <summary>
    /// Loops through each member of the button list array, gets the parent and sets the game controller
    /// </summary>
    void SetGameControllerReference()
    {
        foreach (var t in ButtonList)
            t.GetComponentInParent<Gridspace>().SetGameController(this);
    }

    /// <summary>
    /// Human = X, AI = O
    /// </summary>
    /// <returns>Board Marker for Human and AI Player</returns>
    public string GetPlayerMark()
    {
        if (_playerId == 1)
            return "X";

        if (_playerId == 2)
            return "O";

        return "!!";
    }

    /// <summary>
    /// X = 1, O = 2
    /// </summary>
    /// <returns>The byte ID of the current player</returns>
    public byte GetPlayerID()
    {
        return _playerId;
    }

    public void EndTurn()
    {
        //inc move counter by 1 as max of 9 moves if no win condition met
        _moveCount++;

        //first check for win condition at the end of each turn
        if (_gb.CheckForWinningMove(_playerId))
        {
            //Deactivate board
            GameOver();
            //Show GameOver Panel
            GameOverPanel.SetActive(true);
            //Set text of winning player
            GameOverText.text = GetPlayerMark() + " Wins!";
            return;
        }
        //Check for a draw
        else if(_moveCount >= 9)
        {
            GameOverPanel.SetActive(true);
            GameOverText.text = "It's a Draw!";
            return;
        }

        //Switch to AI and run AI turn
        if (_playerId == 1)
        {
            _playerId = 2;
            _ai.MakeMove();
        }
        //Switch back to player
        else if (_playerId == 2)
            _playerId = 1;
    }

    /// <summary>
    /// Loops through each button and deactivates at the end of the game
    /// </summary>
    void GameOver()
    {
        foreach (var t in ButtonList)
        {
            t.GetComponentInParent<Button>().interactable = false;
        }
    }

    /// <summary>
    /// To restart the game
    /// loop through each button and re activate
    /// reset move counter
    /// reset current player to human
    /// clear the board
    /// clear game over panel
    /// </summary>
    public void RestartGame()
    {
        foreach (var t in ButtonList)
        {
            t.GetComponentInParent<Button>().interactable = true;
            t.text = "";
        }
        _moveCount = 0;
        _playerId = 1;
        GameOverPanel.SetActive(false);
        GameOverText.text = "";
        _gb.ClearArray();
    }
}
