using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScoreKeeper : MonoBehaviour
{
	public LevelLoader levelLoader;
	private Tilemap tilemap => LevelInfo.instance.tilemap;
	public TileBase gem, collected;

	public float percentNeeded = 1f;
	private Transform player => LevelInfo.instance.player.transform;

	public int score;

	private int GemCount => levelLoader.map.TileCount[3];

	private bool reloaded = false;

	// Start is called before the first frame update
	void Start()
	{
		levelLoader.onReload += Reload;

		Debug.Log("Gem count: " + GemCount);
	}

	// Update is called once per frame
	void Update()
	{
		if (!levelLoader.mainSceneLoaded)
			return;

		if (TileOnPosition(player.position) == gem)
		{
			Debug.Log("Found Gem");
			Collect(player.position);
		}

		if (score >= GemCount * percentNeeded)
		{
			levelLoader.LoadNextLevel();
		}
	}

	private void Reload()
	{
		score = 0;
		Debug.Log("Gem count: " + GemCount);
	}

	private void Collect(Vector3 pos)
	{
		Vector3Int cellPosition = tilemap.WorldToCell(pos);
		tilemap.SetTile(cellPosition, collected);
		score++;
	}

	private TileBase TileOnPosition(Vector3 worldPosition)
	{
		Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
		return tilemap.GetTile(cellPosition);
	}

	private void OnGUI()
	{
		GUILayout.Label("\nYou need " + (GemCount * percentNeeded - score) + " more gems");
	}
}