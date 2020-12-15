using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
	public string mainScene;
	public MapGenerator map;
	public Vector2Int startSize;
	public float sizeScale;

	public int level = 1;

	public bool mainSceneLoaded = false;

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
	}

	public void LoadNextLevel()
	{
		level++;
		LoadLevel();
	}

	public void LoadLastLevel()
	{
		level--;
		LoadLevel();
	}

	private void SetMapSize()
	{
		map.size = new Vector2Int((int) (startSize.x * Mathf.Pow(sizeScale, level)),
			(int) (startSize.y * Mathf.Pow(sizeScale, level)));
	}

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
				StartLevel();
			}
		};
	}

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