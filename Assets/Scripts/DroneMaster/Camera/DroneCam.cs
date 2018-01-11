using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCam : MonoBehaviour {

	public Transform drone;
	public float distance;
	public float height;
	public float rotationDamping;
	public float heightDamping;
	public float zoomRatio;
	public float defaultFOV;
	public float rotation_vector;
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 local_velocity = drone.InverseTransformDirection (drone.GetComponent<Rigidbody>().velocity);
		if (local_velocity.z < 0.5f){
			rotation_vector = drone.eulerAngles.y;
		}
		else {
			rotation_vector = drone.eulerAngles.y;
		}

		float acceleration = drone.GetComponent<Rigidbody>().velocity.magnitude;
		Camera.main.fieldOfView = defaultFOV + acceleration * zoomRatio * Time.deltaTime;
	}

	void LateUpdate(){
		float wantedAngle = rotation_vector;
		float wantedHeight = drone.position.y + height;
		float myAngle = transform.eulerAngles.y;
		float myHeight = transform.position.y;

		myAngle = Mathf.LerpAngle (myAngle, wantedAngle, rotationDamping * Time.deltaTime);
		myHeight = Mathf.LerpAngle (myHeight, wantedHeight, heightDamping * Time.deltaTime);

		Quaternion currentRotation = Quaternion.Euler (0, myAngle, 0);

		transform.position = drone.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		
		Vector3 temp = transform.position;
		temp.y = myHeight;
		transform.position = temp;

		transform.LookAt (drone);
	}
}
