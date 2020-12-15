using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
	public Vector2 maxDist;
	public Transform target;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		var pos = transform.position;
		
		if (pos.x > target.position.x + maxDist.x)
		{
			pos.x = target.position.x + maxDist.x;
		}
		if (pos.x < target.position.x - maxDist.x)
		{
			pos.x = target.position.x - maxDist.x;
		}
		
		if (pos.y > target.position.y + maxDist.y)
		{
			pos.y = target.position.y + maxDist.y;
		}
		if (pos.y < target.position.y - maxDist.y)
		{
			pos.y = target.position.y - maxDist.y;
		}

		transform.position = pos;
	}
}