using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DroneManager : MonoBehaviour {
	public GameObject Motor01;
	public GameObject Motor02;
	public GameObject Motor03;
	public GameObject Motor04;
	private Rigidbody[] Motor;
	public float thrust;
	
	// Use this for initialization
	void Start () {
		Motor = new Rigidbody[5];
		Motor[1] = Motor01.GetComponent<Rigidbody>();
		Motor[2] = Motor02.GetComponent<Rigidbody>();
		Motor[3] = Motor03.GetComponent<Rigidbody>();
		Motor[4] = Motor04.GetComponent<Rigidbody>();
	}
	
	void Awake () {
		
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.M)){
			Motor[1].AddForce(transform.up * thrust);
			Motor[2].AddForce(transform.up * thrust);
			Motor[3].AddForce(transform.up * thrust);
			Motor[4].AddForce(transform.up * thrust);
		}
	}
}
