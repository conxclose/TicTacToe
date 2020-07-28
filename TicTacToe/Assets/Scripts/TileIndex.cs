using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class TileIndex : MonoBehaviour
{
    [SerializeField]
    private byte xPos = 0;

    [SerializeField]
    private byte yPos = 0;

    public byte GetXPos()
    {
        return xPos;
    }

    public byte GetYPos()
    {
        return yPos;
    }

}
