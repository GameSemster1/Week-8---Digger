using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class QarryingTile : MonoBehaviour
{
    [SerializeField] TileBase[] qarryingTiles = null;

    public bool Contain(TileBase tile) {
        return qarryingTiles.Contains(tile);
    }

    public TileBase[] Get() { return qarryingTiles;  }
}
