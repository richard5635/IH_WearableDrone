using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamePosition : MonoBehaviour {
	public Transform target;
	float yPos;

	// Use this for initialization
	void Start () {
		yPos = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (
			target.position.x,
			target.position.y + yPos,
			target.position.z
		);
	}
}
