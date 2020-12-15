using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;


/**
* This component allows the player to move by clicking the arrow keys,
* but only if the new position is on an allowed tile.
*/
public class KeyboardMoverByTile : KeyboardMover
{
	[SerializeField] Tilemap tilemap = null;
	[SerializeField] AllowedTile allowedTile = null;
	[SerializeField] QarryingTile qarryingTile = null;
	[SerializeField] Tile tile = null;
	[SerializeField] KeyCode toQarrying;

	private Vector3 startingPosition;

	private void Awake()
	{
		startingPosition = transform.position;
	}

	private TileBase TileOnPosition(Vector3 worldPosition)
	{
		Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
		return tilemap.GetTile(cellPosition);
	}

	void Update()
	{
		Vector3 newPosition = NewPosition();
		TileBase tileOnNewPosition = TileOnPosition(newPosition);
		if (allowedTile.Contain(tileOnNewPosition))
		{
			transform.position = newPosition;
		}
		else if (qarryingTile.Contain(tileOnNewPosition))
		{
			if (Input.GetKey(toQarrying))
			{
				Vector3Int cellPosition = tilemap.WorldToCell(newPosition);
				tilemap.SetTile(cellPosition, tile);
			}
		}
		else
		{
			Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
		}
	}

	public void Reset()
	{
		transform.position = startingPosition;
	}
}