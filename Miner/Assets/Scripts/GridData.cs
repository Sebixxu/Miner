using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    public Vector3Int Position { get; set; }
    public int Durability { get; set; }

    public GridData(Vector3Int position, int durability)
    {
        Position = position;
        Durability = durability;    
    }
}
