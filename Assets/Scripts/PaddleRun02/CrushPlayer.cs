using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaddleRun{
	public class CrushPlayer : MonoBehaviour {
	public GameObject player;
	public HealthManager healthManager;

	// Use this for initialization
	void Start () {
		healthManager = GameObject.Find("Paddle(Drone)").GetComponent<HealthManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == player)
		{
			healthManager.PlayerDies();
		}
	}
}
}

