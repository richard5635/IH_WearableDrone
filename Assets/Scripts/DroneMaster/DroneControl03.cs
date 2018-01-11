//==============================
/*This script controls the movement of the drone with eight motors.  */
//==============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl03 : MonoBehaviour {
	const float MAX_FORCE = 50;
	const float MAX_TILT = 60;
	const float STEER_FORCE = .1f;
	const float MAX_SPIN = .5f;
	Vector3 frontLeft, frontRight, rearLeft, rearRight, frontLeftIn, frontRightIn, rearLeftIn, rearRightIn;

	Rigidbody body;
	Transform mTransform;

	void Awake(){
		body = GetComponent<Rigidbody>();
		mTransform = GetComponent<Transform>();
		frontLeft = new Vector3(-1.2f, 0, 0.2f);
		frontRight = new Vector3(1.2f, 0, 0.2f);
		rearLeft = new Vector3(-1.2f, 0, -0.2f);
		rearRight = new Vector3(1.2f, 0, -0.2f);
		frontLeftIn = new Vector3(-0.8f, 0, 0.2f);
		frontRightIn = new Vector3(0.8f, 0, 0.2f);
		rearLeftIn = new Vector3(-0.8f, 0, -0.2f);
		rearRightIn = new Vector3(0.8f, 0, -0.2f);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	//
	void FixedUpdate () 
	{
		float forward = Input.GetAxis("Pitch");
		float right = Input.GetAxis("Roll");
		float up = Input.GetAxis("Throttle") * 20;
		float spin = Input.GetAxis("Yaw") * 3.5f;

		Vector3 orientation = mTransform.localRotation.eulerAngles;
		orientation.y = 0;
		FixRanges(ref orientation);

		Vector3 localangularvelocity = mTransform.InverseTransformDirection(body.angularVelocity);

		float velY = body.velocity.y;

		float desiredForward = forward * MAX_TILT - ( orientation.x + localangularvelocity.x * 15 );
		float desiredRight = -right * MAX_TILT - ( orientation.z + localangularvelocity.z * 15 );
		float desiredSpin = spin - localangularvelocity.y;

		ApplyForces( desiredForward / MAX_TILT, desiredRight / MAX_TILT, up - velY, desiredSpin );
		SelfBalancing();
	}

	void ApplyForces( float forward, float right, float up, float spin )
	{
		//need to maintain this level of upwards thrust to gain/lose altitude at the desired rate
		float totalY = Mathf.Min( (up * 100) + 9.81f, MAX_FORCE );

		if (totalY < 0) totalY = 0;
		
		//Steer Force ??


		//distribute according to forward/right (which are indices based on max tilt)
		//The AddForceAtPosition function below requires you to input (Force, Position)
		//front left
		body.AddForceAtPosition( mTransform.up * ( totalY * .125f - forward * STEER_FORCE - right * STEER_FORCE ), mTransform.position + mTransform.TransformDirection( frontLeft ) );

		//front right
		body.AddForceAtPosition( mTransform.up * ( totalY * .125f - forward * STEER_FORCE + right * STEER_FORCE ), mTransform.position + mTransform.TransformDirection( frontRight ) );

		//rear left
		body.AddForceAtPosition( mTransform.up * ( totalY * .125f + forward * STEER_FORCE - right * STEER_FORCE ), mTransform.position + mTransform.TransformDirection( rearLeft ) );

		//rear right
		body.AddForceAtPosition( mTransform.up * ( totalY * .125f + forward * STEER_FORCE + right * STEER_FORCE ), mTransform.position + mTransform.TransformDirection( rearRight ) );

		//front left inner
		body.AddForceAtPosition( mTransform.up * ( totalY * .125f - forward * STEER_FORCE - right * STEER_FORCE ), mTransform.position + mTransform.TransformDirection( frontLeftIn ) );

		//front right inner
		body.AddForceAtPosition( mTransform.up * ( totalY * .125f - forward * STEER_FORCE + right * STEER_FORCE ), mTransform.position + mTransform.TransformDirection( frontRightIn ) );

		//rear left inner
		body.AddForceAtPosition( mTransform.up * ( totalY * .125f + forward * STEER_FORCE - right * STEER_FORCE ), mTransform.position + mTransform.TransformDirection( rearLeftIn ) );

		//rear right inner
		body.AddForceAtPosition( mTransform.up * ( totalY * .125f + forward * STEER_FORCE + right * STEER_FORCE ), mTransform.position + mTransform.TransformDirection( rearRightIn ) );

		spin = Mathf.Min(MAX_SPIN, spin);

		//Front
		body.AddForceAtPosition( mTransform.right * spin, mTransform.position + mTransform.forward );
		//Rear
		body.AddForceAtPosition( -mTransform.right * spin, mTransform.position - mTransform.forward );
	}

	void FixRanges( ref Vector3 euler )
	{
		if (euler.x < -180)
			euler.x += 360;
		else if (euler.x > 180)
			euler.x -= 360;

		if (euler.y < -180)
			euler.y += 360;
		else if (euler.y > 180)
			euler.y -= 360;

		if (euler.z < -180)
			euler.z += 360;
		else if (euler.z > 180)
			euler.z -= 360;
	}

	void SelfBalancing(){

	}
	// Update is called once per frame
	//If we use unity input, it might solve the unstable jumping force
	// void Update () {
	// 	if(Input.GetKey(KeyCode.M)){
	// 		rb.AddForce(transform.up * thrust);
	// 	}
	// }
}
