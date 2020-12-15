using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class AllowedTile : MonoBehaviour
{
    [SerializeField] TileBase[] allowedTiles = null;

    public bool Contain(TileBase tile) {
        return allowedTiles.Contains(tile);
    }

    public TileBase[] Get() { return allowedTiles;  }
}
