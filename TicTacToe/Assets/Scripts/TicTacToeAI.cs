using System;
using UnityEngine;

/// <summary>
/// Base class for the TicTacToe AI implementation
/// </summary>
public class TicTacToeAI : MonoBehaviour
{
    /// <summary>
    /// Reference to the gameboard script
    /// </summary>
    public Gameboard Gameboard;

    /// <summary>
    /// The GameObject that hosts the gameboard script
    /// </summary>
    public GameObject GbO;

    /// <summary>
    /// How difficult is the game
    /// </summary>
    public int DepthCap;

    /// <summary>
    /// Have the AI calculate its next move
    /// </summary>
    public void MakeMove()
    {
        //Set player = 1 as human generated current board state
        MinimaxNode rootNode = new MinimaxNode {Player = 1, GameState = Gameboard.BoardArray.Clone() as byte[,]};
        GenerateNode(ref rootNode);
        rootNode.Evaluate(out var bestNode, int.MinValue, int.MaxValue);

        //Get the index of the button in the board
        var index = bestNode.ModifiedCoordinate.Item2 * 3 + bestNode.ModifiedCoordinate.Item1;
        var childGameObject = GbO.transform.GetChild(index).gameObject;
        Gameboard.UpdateGameboard(childGameObject, 2);
    }

    /// <summary>
    /// Recursively populates the tree using the parent node as a base
    /// </summary>
    /// <param name="parentNode">current perm of the board</param>
    void GenerateNode(ref MinimaxNode parentNode)
    {
        //generate all possible perm of board and assign to nodes
        //check current depth of the current node, if = limit break, if !- limit recurse generate node on new node

        if (parentNode.Depth == DepthCap)
            return;

        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                if (parentNode.GameState[x,y] != 0)
                    continue;

                MinimaxNode childNode = new MinimaxNode();
                childNode.GameState = parentNode.GameState.Clone() as byte[,];

                if (childNode.GameState == null)
                    throw new Exception("Cannot clone Game State");

                childNode.GameState[x, y] = (byte) (parentNode.Player == 1 ? 2 : 1);
                childNode.Player = (byte)(parentNode.Player == 1 ? 2 : 1);
                childNode.ModifiedCoordinate = ((byte) x, (byte) y);
                childNode.Parent = parentNode;

                //Don't generate permutations of children that generate win conditions
                if (!(childNode.CheckForWinningMove(1) || childNode.CheckForWinningMove(2)))
                {
                    GenerateNode(ref childNode);
                }
                parentNode.Children.Add(childNode);
            }
        }
    }
}
