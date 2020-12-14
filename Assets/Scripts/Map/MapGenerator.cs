using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
	public Tilemap tilemap;
	public float seed = 100;
	public float multiplier = 100;

	public TileBase def;

	public Entry[] entries;

	public Vector2Int size;

	public bool recreate = false;

	[System.Serializable]
	public class Entry
	{
		public TileBase tile;
		public float prob;
		public float offset;
		public float multiplier = 1;
	}

	// Start is called before the first frame update
	void Start()
	{
		CreateMap();
	}

	private void CreateMap()
	{
		var map = Generate();

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

	private float GetNoise(float x, float y)
	{
		var f = Mathf.PerlinNoise(x * multiplier + seed, y * multiplier + seed);
		f = Mathf.Clamp(f, 0, 1);
		return f;
	}
}