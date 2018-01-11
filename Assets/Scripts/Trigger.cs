using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Trigger : MonoBehaviour {
	

	public SerialPort serial = new SerialPort("/dev/cu.usbmodem1441",9600);
	private bool lightState = false;
	bool switch01 = false;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player"){
			Debug.Log("Contact was made!");
			switch01 = !switch01;
			audio.pitch = Random.Range(0.1f, 1.5f);
			audio.Play();
			if(switch01) GetComponent<Renderer>().material.color = Color.red;
			else GetComponent<Renderer>().material.color = Color.white;
		}

		#region Requires USB
		if(serial.IsOpen == false){
			serial.Open();
		}

		if(lightState == false){
			serial.Write("A");
			lightState = true;
		}

		else{
			serial.Write("B");
			lightState = false;
		}
		#endregion
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
