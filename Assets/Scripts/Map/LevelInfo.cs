using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelInfo : MonoBehaviour
{
	public static LevelInfo instance;

	public KeyboardMoverByTile player;
	public Tilemap tilemap;

	public LevelLoader levelLoader;

	private void Awake()
	{
		instance = this;
	}
}