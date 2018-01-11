using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour {
	public GameObject target;
	Vector3 diff;
	public float followSpeed;

	// Use this for initialization
	void Start () {
		diff = target.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 playerPosition = target.transform.position;
		// transform.position = Vector3.Lerp(
		// 	transform.position,
		// 	target.transform.position - diff,
		// 	Time.deltaTime * followSpeed
		// );
		transform.position = target.transform.position - diff;
	}
}
