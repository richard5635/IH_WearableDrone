using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorMovement : MonoBehaviour {

	private Rigidbody rb;
	public float thrust;

	// Use this for initialization
	void Start () {

	}

	void Awake () {
		// rb  = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		// if(Input.GetKey(KeyCode.M)){
		// 	rb.AddForce(transform.up * thrust);
		// }
		
	}
}
