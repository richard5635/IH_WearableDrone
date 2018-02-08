using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehavior : MonoBehaviour {
	public GameObject SplashPrefab;
	public SerialHandler serialHandler;

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Vector3 paddlePos = other.transform.position;
			Vector3 closestPoint = other.ClosestPointOnBounds(paddlePos);
			Instantiate(SplashPrefab, closestPoint, Quaternion.identity);
			serialHandler.Write("w");
		}
	}
}
