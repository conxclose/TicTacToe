using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MinimaxNode 
{
    public MinimaxNode Parent { get; set; }
    public List<MinimaxNode> Children = new List<MinimaxNode>();
    public byte Player;
    public byte[,] GameState = new byte[3,3];
    public (byte, byte) ModifiedCoordinate;
    public int Score = 0;
    public MinimaxNode BestNode = null;

    public string BoardDisplay
    {
        get
        {
            var sb = new StringBuilder();

            for (var y = 0; y < 3; y++)
            {
                for (var x = 0; x < 3; x++)
                {
                    var data = GameState[x, y];
                    switch (data)
                    {
                        case 0:
                            sb.Append("b");
                            break;
                        case 1:
                            sb.Append("X");
                            break;
                        case 2:
                            sb.Append("O");
                            break;
                    }
                }

                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
    public int Depth
    {
        get
        {
            if (Parent == null)
                return 0;

            return Parent.Depth + 1;
        }
    }

    private void DoStaticEval()
    {
        if (CheckForWinningMove(1))
        {
            Score = 10 - Depth;
        }

        if (CheckForWinningMove(2))
        {
            Score = -10 + Depth;
        }
    }

    public int Evaluate(out MinimaxNode bestNode, int alpha, int beta)
    {
        bestNode = null;

        if (Children.Count == 0)
        {
            DoStaticEval();
            return Score;
        }

        var runningEval = Player == (byte)2 ? int.MinValue : int.MaxValue;

        foreach (var child in Children)
        {
            var eval = child.Evaluate(out var myBestNode, alpha, beta);
            var oldRunningEval = runningEval;

            if (Player == (byte) 2)
            {
                runningEval = Math.Max(runningEval, eval);
                alpha = Math.Max(alpha, eval);
                if (beta <= alpha)
                    break;
            }
            else
            {
                runningEval = Math.Min(runningEval, eval);
                beta = Math.Min(beta, eval);
                if (beta <= alpha)
                    break;
            }

            if (runningEval != oldRunningEval)
            {
                bestNode = child;
                BestNode = child;
            }

        }

        Score = runningEval;

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