using UnityEngine;
using UnityEngine.UI;

public class Gridspace : MonoBehaviour
{
    /// <summary>
    /// Reference to the game board script
    /// </summary>
    private Gameboard _gb;

    /// <summary>
    /// Reference to the game object that hosts the gameboard script
    /// </summary>
    public GameObject GameBoard;

    /// <summary>
    /// Reference to the description of the button's position in the board
    /// </summary>
    private TileIndex _tileIndex;

    /// <summary>
    /// Reference to game controller script
    /// </summary>
    private GameController _gameController;

    /// <summary>
    /// Reference to the UI Button
    /// </summary>
    public Button B;

    /// <summary>
    /// Reference to the text of the UI button
    /// </summary>
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

    // On Click function for human player, set grid space to player marker
    public void SetSpace()
    {
        _gb.UpdateGameboard(this.gameObject,1);
    }

    //Updates the visual gridspace with player marker and deactivates button
    public void UpdateContent()
    {
        T.text = _gameController.GetPlayerMark();
        B.interactable = false;
    }
}
