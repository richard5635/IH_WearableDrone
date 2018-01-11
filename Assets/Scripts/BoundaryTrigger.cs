using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Enemy"){
			Destroy(other.gameObject);
			Instantiate(other.gameObject,new Vector3(Random.Range(-5,5),6,32),Quaternion.identity);
			//Make respawn script. Use instantiate, additional things that will make the respawn successful
		} 
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
