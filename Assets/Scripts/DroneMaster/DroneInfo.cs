using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneInfo : MonoBehaviour {
	public Text posX;
	private float posfX;
	public Text posY;
	private float posfY;
	public Text posZ;
	private float posfZ;
	public Text rotX;
	private float rotfX;
	public Text rotY;
	private float rotfY;
	public Text rotZ;
	private float rotfZ;

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		posfX = transform.position.x;
		posfY = transform.position.y;
		posfZ = transform.position.z;
		rotfX = transform.rotation.x;
		rotfY = transform.rotation.y;
		rotfZ = transform.rotation.z;
		
		posX.text = "X: " + posfX.ToString("F2");
		posY.text = "Y: " + posfY.ToString("F2");
		posZ.text = "Z: " + posfZ.ToString("F2");
		rotX.text = "X: " + rotfX.ToString("F2");
		rotY.text = "Y: " + rotfY.ToString("F2");
		rotZ.text = "Z: " + rotfZ.ToString("F2");
	}
}
