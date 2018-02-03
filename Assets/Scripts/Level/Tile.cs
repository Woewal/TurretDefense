using UnityEngine;
using System.Collections;

public class Tile
{
    public enum Type { Empty, Floor }

    public Type type;

    public int x;
    public int y;

    public Tile()
    {
        type = Type.Empty;
    }
}
