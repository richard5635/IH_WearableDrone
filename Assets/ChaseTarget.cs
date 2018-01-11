using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MonoBehaviour {
	public GameObject target;
	public PlayerMovementPaddle02 playerMovement;
	Vector3 diff;
	private float followSpeed;

	// Use this for initialization
	void Start () {
		diff = target.transform.position - transform.position;
		followSpeed = playerMovement.forwardSpeed;
	}

	void OnEnable()
	{
		transform.position = new Vector3(0,0,target.transform.position.z - 5.0f);
		diff = target.transform.position - transform.position;
		followSpeed = playerMovement.forwardSpeed;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 playerPosition = target.transform.position;
		transform.position = Vector3.Lerp(
			transform.position,
			target.transform.position,
			Time.deltaTime * followSpeed * 5
		);
	}
}
