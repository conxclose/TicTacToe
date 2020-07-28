using System;
using System.Collections.Generic;
using UnityEngine;

public class MinimaxNode 
{
    public MinimaxNode Parent { get; set; }
    public List<MinimaxNode> Children = new List<MinimaxNode>();
    public byte Player;
    public byte[,] GameState = new byte[3,3];
    public (byte, byte) ModifiedCoordinate;

    public int Depth
    {
        get
        {
            if (Parent == null)
                return 0;

            return Parent.Depth + 1;
        }
    }

    public int Score
    {
        get
        {
            if (CheckForWinningMove(1))
                return 1;

            if (CheckForWinningMove(2))
                return -1;

            return 0;
        }
    }

    public int Evaluate(out MinimaxNode bestNode)
    {
        bestNode = null;

        if(Children.Count == 0)
            return Score;

        var runningEval = Player == 1 ? int.MinValue : int.MaxValue;

        foreach (var child in Children)
        {
            var eval = child.Evaluate(out var ignored);
            runningEval = Player == 1 ? Math.Max(runningEval, eval) : Math.Min(runningEval, eval);
            if (runningEval == eval) bestNode = child;
        }

        return runningEval;
    }

    public bool CheckForWinningMove(byte pt)
    {
        //Horizontal Top
        if (GameState[0, 0] == pt && GameState[1, 0] == pt && GameState[2, 0] == pt)
            return true;
        //Horizontal Middle
        if (GameState[0, 1] == pt && GameState[1, 1] == pt && GameState[2, 1] == pt)
            return true;
        //Horizontal Bottom
        if (GameState[0, 2] == pt && GameState[1, 2] == pt && GameState[2, 2] == pt)
            return true;
        //Diagonal L-R
        if (GameState[0, 0] == pt && GameState[1, 1] == pt && GameState[2, 2] == pt)
            return true;
        //Diagonal R-L
        if (GameState[2, 0] == pt && GameState[1, 1] == pt && GameState[0, 2] == pt)
            return true;
        //Vertical Left
        if (GameState[0, 0] == pt && GameState[0, 1] == pt && GameState[0, 2] == pt)
            return true;
        //Vertical Middle
        if (GameState[1, 0] == pt && GameState[1, 1] == pt && GameState[1, 2] == pt)
            return true;
        //Vertical Right
        return GameState[2, 0] == pt && GameState[2, 1] == pt && GameState[2, 2] == pt;
    }
}