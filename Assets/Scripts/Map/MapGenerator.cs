using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

/// <summary>
/// Generates Tilemap maps using perlin noise;
/// </summary>
public class MapGenerator : MonoBehaviour
{
	private Tilemap tilemap => LevelInfo.instance.tilemap;

	[Tooltip("Should the seed be randomized on start?")]
	public bool useRandomSeed;

	[Tooltip("The seed for the noise randomizer.")]
	public float seed = 100;

	[Tooltip("Multiplies the noise to change the density.")]
	public float multiplier = 100;

	[Tooltip("The default tile to use when there are none.")]
	public TileBase def;

	[Tooltip("A list of tiles to put in the map and their properties.")]
	public TileType[] entries;

	[Tooltip("The size of the map. This is overriden by LevelLoader.")]
	public Vector2Int size;

	[Tooltip("For debugging. Lave as 'false'.")]
	public bool recreate = false;

	public int[] TileCount { get; private set; }

	[System.Serializable]
	public class TileType
	{
		public TileBase tile;
		public float prob;
		public float offset;
		public float multiplier = 1;
	}

	private readonly Random rand = new Random();

	/// <summary>
	/// Create the map.
	/// </summary>
	public void CreateMap()
	{
		if (useRandomSeed)
		{
			seed = rand.Next(0, 1000);
		}

		var map = Generate();
		TileCount = new int[entries.Length + 1];

		for (int i = 0; i < map.GetLength(0); i++)
		{
			for (int j = 0; j < map.GetLength(1); j++)
			{
				var position = new Vector3Int(i - size.x / 2, j - size.y / 2, 0);
				TileBase tile = def;

				var type = map[i, j];

				if (type >= 0 && type < entries.Length)
				{
					tile = entries[type].tile;
					TileCount[type + 1]++;
				}
				else
				{
					TileCount[0]++;
				}

				tilemap.SetTile(position, tile);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (recreate)
			CreateMap();
	}

	/// <summary>
	/// Generates a 2d array representing the tiles.
	/// </summary>
	/// <returns></returns>
	private int[,] Generate()
	{
		var map = new int[size.x, size.y];

		for (int i = 0; i < map.GetLength(0); i++)
		{
			for (int j = 0; j < map.GetLength(1); j++)
			{
				map[i, j] = GenerateTile(i, j);
			}
		}

		return map;
	}

	/// <summary>
	/// Generates the tile at x, y.
	/// </summary>
	private int GenerateTile(float x, float y)
	{
		int ret = -1;

		for (var i = 0; i < entries.Length; i++)
		{
			var en = entries[i];
			if (GetNoise(x * en.multiplier + en.offset, y * en.multiplier + en.offset) <= en.prob)
			{
				ret = i;
			}
		}

		return ret;
	}

	/// <summary>
	/// Wraps unity's built-in perlin noise function with the multiplier an seed.
	/// </summary>
	/// <returns></returns>
	private float GetNoise(float x, float y)
	{
		var f = Mathf.PerlinNoise(x * multiplier + seed, y * multiplier + seed);
		f = Mathf.Clamp(f, 0, 1);
		return f;
	}
}