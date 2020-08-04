using UnityEngine;

/// <summary>
/// Description of the button's position in the visual gameboard
/// </summary>
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
