using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}
public class PlayerMovementForward : MonoBehaviour {
	
	[Header("Movement")]
	public float forwardSpeed = 4.0f;
	public GameObject missileSet;
	public Transform shotPos;
	public float shotForce = 1000f;
	public float moveSpeed = 10f;
	
	public float speed;
    public float tilt;
    public Boundary boundary;
	public PID wingPID;

	Vector3 leftWing, rightWing;
	Transform dTransform;
	private float errRotY;

	[Header("ShootingParameter")]

	public float fireRate;
	private float nextFire;
	
	// Use this for initialization
	void Awake () {
		dTransform = GetComponent<Transform>();
		leftWing = new Vector3(-dTransform.localScale.x,0,0);
		rightWing = new Vector3(dTransform.localScale.x,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		// forwardMovement = forwardSpeed * Time.deltaTime;
		// transform.Translate(new Vector3(0,0,forwardMovement));
		if(Input.GetButtonUp("Fire1") && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			FireMissile();
		}
	}

	void FixedUpdate()
	{
		errRotY = wingPID.Update(transform.rotation.y, 0.0f, Time.deltaTime);
		//errRotY = dTransform.rotation.y * 10;
		float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f );
        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3 
        (
            Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
            Mathf.Clamp (GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax),
			0.0f
        );

		GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0,0,errRotY), dTransform.position + dTransform.TransformDirection(rightWing));

		//Need to add without affecting collision
        //GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}

	void FireMissile(){
		Instantiate(missileSet,shotPos.position,shotPos.rotation);
		GetComponent<AudioSource>().Play();
	}

	IEnumerator SelfBalance(){
		Debug.Log("Now Balancing!");
		Vector3 lastPos = transform.position;
		yield return new WaitForSeconds(1.0f);
		//transform.position = Vector3.Lerp(transform.position, lastPos, Time.deltaTime * 2);
		//transform.rotation = Quaternion.Euler(0,0,0);
		//transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,0), Time.deltaTime);
	}
}
