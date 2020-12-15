using UnityEngine;

/**
 * This component chases a given target object.
 */
public class Chaser : TargetMover
{
	[Tooltip("The object that we try to chase")] [SerializeField]
	Transform targetObject = null;

	public Vector3 TargetObjectPosition()
	{
		return targetObject.position;
	}

	private void Update()
	{
		SetTarget(targetObject.position);

		if (Vector3Int.RoundToInt(transform.position) ==
		    Vector3Int.RoundToInt(LevelInfo.instance.player.transform.position))
		{
			LevelInfo.instance.levelLoader.LoadLastLevel();
		}
	}
}