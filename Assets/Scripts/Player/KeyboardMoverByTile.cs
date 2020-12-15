using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;


/**
* This component allows the player to move by clicking the arrow keys,
* but only if the new position is on an allowed tile.
*/
public class KeyboardMoverByTile: KeyboardMover {
	[SerializeField] Tilemap tilemap = null;
	[SerializeField] AllowedTile allowedTile = null;
	[SerializeField] QarryingTile qarryingTile = null;
	[SerializeField] Tile tile = null;
	[SerializeField] KeyCode toQarrying;
	private bool qarry = false;

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
		else if(qarryingTile.Contain(tileOnNewPosition)){
			var position = new Vector3Int((int)(newPosition.x), (int)(newPosition.y), 0);
			if(Input.GetKeyDown(toQarrying)){
				tilemap.SetTile(position, tile);
			}
		} 
		else {
			Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
		}
	}
}
