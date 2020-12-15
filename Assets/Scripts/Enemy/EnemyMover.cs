using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;


public class EnemyMover : MonoBehaviour{
    [SerializeField] Tilemap tilemap = null;
	[SerializeField] AllowedTile allowedTile = null;

    private Vector3 NewPosition() {
        if (Input.GetKeyDown(KeyCode.A)) {
            return transform.position + Vector3.left;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            return transform.position + Vector3.right;
        } else if (Input.GetKeyDown(KeyCode.W)) {
            return transform.position + Vector3.up;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            return transform.position + Vector3.down;
        } else {
            return transform.position;
        }
    }

	private TileBase TileOnPosition(Vector3 worldPosition) {
		Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
		return tilemap.GetTile(cellPosition);
	}

	void Update() {
		Vector3 newPosition = NewPosition();
		TileBase tileOnNewPosition = TileOnPosition(newPosition);
		if (allowedTile.Contain(tileOnNewPosition)) {
			transform.position = newPosition;
		}
	}

}
