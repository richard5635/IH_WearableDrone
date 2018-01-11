using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushPlayer : MonoBehaviour {
	public GameObject player;
	public HealthManager healthManager;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		healthManager = GameObject.Find("Paddle(Drone)").GetComponent<HealthManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			healthManager.PlayerDies();
		}
	}
}
