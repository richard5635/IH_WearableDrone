using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoCom : MonoBehaviour {

	private float AmountToMove;
    public float x;
    public float y;
    public float z;
	private float rotX;
	private float rotY;
	private float rotZ;
    SerialPort serial = new SerialPort("/dev/cu.usbmodem1441",9600);

    void Start(){
        serial.Open();
        serial.ReadTimeout = 2000;
        //Add algorithm to differ
        StartCoroutine("ExtendStickCom");
    }

    void Update(){
        
		
        serial.BaseStream.Flush();
		rotX = transform.rotation.x;
		rotY = transform.rotation.y;
		rotZ = transform.rotation.z;

        if(!serial.IsOpen)serial.Open();


        serial.Write("Rotation angle : "  );
		serial.Write("x: " +rotX);
		serial.Write("y: " +rotY);
		serial.Write("x: " +rotZ);
		
        // x = AcX - 190;
        // y += GyY / 3000;
        // z += GyX / 3000;

        // transform.localEulerAngles = new Vector3 (-x,-y,-z);

    }

    public void PhysicalCollision(float collDist, float collForce){
        rotX = transform.rotation.x;
        rotY = transform.rotation.y;
        rotZ = transform.rotation.z;

        if(!serial.IsOpen)serial.Open();

        serial.Write("Rotation angle : "  );
		serial.Write("x: " +rotX);
		serial.Write("y: " +rotY);
		serial.Write("x: " +rotZ);

    }

    IEnumerator ExtendStickCom(){
        yield return null;
    }
}
