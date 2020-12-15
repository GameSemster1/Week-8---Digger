using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads the levels.
/// </summary>
public class LevelLoader : MonoBehaviour
{
	[Tooltip("The name of the main scene.")]
	public string mainScene;

	[Tooltip("The 'MapGenerator' gameObject.")]
	public MapGenerator map;

	[Tooltip("The initial size of the map.")]
	public Vector2Int startSize;

	[Tooltip("By how much should we increase the map every level?")]
	public float sizeScale;

	[Tooltip("The current level.")] public int level = 1;

	public bool mainSceneLoaded { get; private set; }

	[Tooltip("Actions to perform every time a new level is loaded.")]
	public UnityAction onReload;

	private void Awake()
	{
		if (!SceneManager.GetSceneByName(mainScene).isLoaded)
		{
			SceneManager.LoadSceneAsync(mainScene, LoadSceneMode.Additive);
		}

		mainSceneLoaded = true;

	}

	private void Start()
	{
		map.size = startSize;
		map.CreateMap();
		
		LevelInfo.instance.levelLoader = this;
	}

	/// <summary>
	/// Loads the next level.
	/// </summary>
	public void LoadNextLevel()
	{
		level++;
		LoadLevel();
	}

	/// <summary>
	/// Loads the previous level.
	/// </summary>
	public void LoadLastLevel()
	{
		level--;
		LoadLevel();
	}

	/// <summary>
	/// Sets MapGenerator's map size.
	/// </summary>
	private void SetMapSize()
	{
		map.size = new Vector2Int((int) (startSize.x * Mathf.Pow(sizeScale, level)),
			(int) (startSize.y * Mathf.Pow(sizeScale, level)));
	}

	/// <summary>
	/// Loads the level 'level'.
	/// </summary>
	public void LoadLevel()
	{
		mainSceneLoaded = false;
		SceneManager.UnloadSceneAsync(mainScene);
		onReload?.Invoke();
		SceneManager.LoadSceneAsync(mainScene, LoadSceneMode.Additive);
		SceneManager.sceneLoaded += (scene, mode) =>
		{
			if (scene.name == mainScene)
			{
				mainSceneLoaded = true;
				LevelInfo.instance.levelLoader = this;
				StartLevel();
			}
		};
	}

	/// <summary>
	/// Starts the current level after it was loaded.
	/// </summary>
	private void StartLevel()
	{
		SetMapSize();
		map.CreateMap();
	}

	private void OnGUI()
	{
		GUILayout.Label("Level: " + level);
	}
}