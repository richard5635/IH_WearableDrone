using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehavior : MonoBehaviour {
	public GameObject SplashPrefab;

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Debug.Log("splashed!");
			Vector3 paddlePos = other.transform.position;
			Vector3 closestPoint = other.ClosestPointOnBounds(paddlePos);
			Instantiate(SplashPrefab, closestPoint, Quaternion.identity);
		}
	}
}
