using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementPaddle : MonoBehaviour
{

    [Header("Movement")]
    public float forwardSpeed;
    public float initSpeed;
    public GameObject missileSet;
    public SpeedBoost speedBoost;
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

    //SpeedBoost
    public float ExtraSpeed;
    public Text forwardSpeedText;

    [Header("ShootingParameter")]

    public float fireRate;
    private float nextFire;

    // Use this for initialization
    void Awake()
    {
        dTransform = GetComponent<Transform>();
        leftWing = new Vector3(-dTransform.localScale.x, 0, 0);
        rightWing = new Vector3(dTransform.localScale.x, 0, 0);
    }

    void Start()
    {
        UpdateSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        // forwardMovement = forwardSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);



        // GetComponent<Rigidbody>().rotation = Quaternion.Euler 
        // (
        //     0.0f, 
        //     0.0f,
        // 	Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax)
        // );

        // GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0,0,errRotY), dTransform.position + dTransform.TransformDirection(rightWing));

        //Need to add without affecting collision
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, 90 + (moveHorizontal * -tilt));
        //GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * );//how to use this?
    }

    void FireMissile()
    {
        Instantiate(missileSet, shotPos.position, shotPos.rotation);
        GetComponent<AudioSource>().Play();
    }

    // IEnumerator SpeedBoostControl()
    // {
    // 	if(Time.deltaTime % 5 == 0 && SpeedBoost>0)
    // 	{
    // 		SpeedBoost--;
    // 	}
    // 	yield return StartCoroutine(SpeedBoostControl());
    // }

    IEnumerator SelfBalance()
    {
        Debug.Log("Now Balancing!");
        Vector3 lastPos = transform.position;
        yield return new WaitForSeconds(1.0f);
        //transform.position = Vector3.Lerp(transform.position, lastPos, Time.deltaTime * 2);
        //transform.rotation = Quaternion.Euler(0,0,0);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,0), Time.deltaTime);
    }

    public void UpdateSpeed()
    {
        //Min : 1.00, Max : 10:00
        forwardSpeed = initSpeed + ExtraSpeed;
        forwardSpeedText.text = "SPEED: " + initSpeed.ToString("0.0") + " + " + ExtraSpeed.ToString("0.0");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "River"){
            //StartCoroutine(speedBoost.PaddleSpeedBoost(other));
            ExtraSpeed += 0.1f;
            UpdateSpeed();
        }
        
    }

    // void OnTriggerExit(Collider other)
    // {
    //     StopCoroutine(speedBoost.PaddleSpeedBoost(other));
    // }
}