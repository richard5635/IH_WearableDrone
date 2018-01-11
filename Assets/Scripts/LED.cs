using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class LED : MonoBehaviour {
	public SerialPort serial = new SerialPort("/dev/cu.usbmodem1441",9600);
	private bool lightState = false;

	public void OnMouseDown(){
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
	}

}
