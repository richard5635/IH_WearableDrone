using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkByTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Sink",6.0f);
	}
	
	// Update is called once per frame
	void Sink()
	{
		if(GetComponent<CapsuleCollider>())GetComponent<CapsuleCollider>().isTrigger = true;
		if(GetComponent<MeshCollider>())GetComponent<MeshCollider>().isTrigger  = true;
		Destroy(gameObject,2);
	}
}
