using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeAI : MonoBehaviour
{
    public GameController GameController;
    public Gameboard Gameboard;
    public GameObject GbO;

    public void MakeMove()
    {
        MinimaxNode rootNode = new MinimaxNode {Player = 1, GameState = Gameboard.BoardArray.Clone() as byte[,]};
        GenerateNode(ref rootNode);
        rootNode.Evaluate(out var bestNode);
        var index = bestNode.ModifiedCoordinate.Item2 * 3 + bestNode.ModifiedCoordinate.Item1;
        var childGameObject = GbO.transform.GetChild(index).gameObject;
        Gameboard.UpdateGameboard(childGameObject, 2);
    }

    void GenerateNode(ref MinimaxNode parentNode)
    {
        //parent node = current perm of the board
        //generate all possible perm of board and assign to nodes
        //check current depth of the current node, if = limit break, if !- limit recurse generate node on new node

        if (parentNode.Depth == 2)
            return;

        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                if (parentNode.GameState[x,y] != 0)
                    continue;

                MinimaxNode childNode = new MinimaxNode();
                childNode.GameState = parentNode.GameState.Clone() as byte[,];
                childNode.GameState[x, y] = (byte) (parentNode.Player == 1 ? 2 : 1);
                childNode.Player = (byte)(parentNode.Player == 1 ? 2 : 1);
                childNode.ModifiedCoordinate = ((byte) x, (byte) y);
                childNode.Parent = parentNode;
                if (!(childNode.CheckForWinningMove(1) || childNode.CheckForWinningMove(2)))
                {
                    GenerateNode(ref childNode);
                }
                parentNode.Children.Add(childNode);
            }
        }
    }
}
