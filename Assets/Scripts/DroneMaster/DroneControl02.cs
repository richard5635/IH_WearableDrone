using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl02 : MonoBehaviour {
	Vector3 frontLeft, frontRight, rearLeft, rearRight;
	Rigidbody body;
	Transform mTransform;

	const float MAX_FORCE = 50;
	const float MAX_TILT = 60;
	const float STEER_FORCE = .05f;
	const float MAX_SPIN = .5f;

	void Awake(){
		body = GetComponent<Rigidbody>();
		mTransform = GetComponent<Transform>();
		frontLeft = new Vector3(-mTransform.localScale.x, 0, mTransform.localScale.x);
		frontRight = new Vector3(mTransform.localScale.x, 0, mTransform.localScale.x);
		rearLeft = new Vector3(-mTransform.localScale.x, 0, -mTransform.localScale.x);
		rearRight = new Vector3(mTransform.localScale.x, 0, -mTransform.localScale.x);
	}

	void FixedUpdate(){
		//Change them to pitch, roll, throttle, yaw
		float forward = Input.GetAxis("Vertical");
		float right = Input.GetAxis("Horizontal");
		float up = Input.GetAxis("Trigger") * 20;
		float spin = Input.GetAxis("Horizontal2") * 3.5f;

		Vector3 orientation = mTransform.localRotation.eulerAngles;
		orientation.y = 0;
		//FixRanges(ref orientation);

		Vector3 localangularvelocity = mTransform.InverseTransformDirection(body.angularVelocity);

		float velY = body.velocity.y;

		float desiredForward = forward * MAX_TILT - ( orientation.x + localangularvelocity.x * 15 );
		float desiredRight = -right * MAX_TILT - ( orientation.z + localangularvelocity.z * 15 );
		float desiredSpin = spin - localangularvelocity.y;

		//ApplyForces( desiredForward / MAX_TILT, desiredRight / MAX_TILT, up - velY, desiredSpin );
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
