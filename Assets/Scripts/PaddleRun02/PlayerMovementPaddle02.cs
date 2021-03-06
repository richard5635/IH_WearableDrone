﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PaddleRun;

public class PlayerMovementPaddle02 : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject Player;
    public PaddleGameController gameController;
    [Header("Movement")]
    public float forwardSpeed;
    public float initSpeed;
    public GameObject missileSet;
    public SpeedBoost speedBoost;
    public Transform shotPos;
    public Slider SpeedSlider;

    public float tiltAngle;

    Vector3 leftWing, rightWing;
    Transform dTransform;
    private float errRotY;

    //Speed Related
    public float ExtraSpeed;
    public Text forwardSpeedText;
    private float maxSpeed = 25.0f;

    [Header("Others")]
    public GameObject splashPrefab;

    public bool lockMovement = false;

    [Header("Water Splash")]
    public GameObject paddleEnd;
    public GameObject waterEffect;

    // Use this for initialization
    void Awake()
    {
        dTransform = GetComponent<Transform>();
        leftWing = new Vector3(-dTransform.localScale.x, 0, 0);
        rightWing = new Vector3(dTransform.localScale.x, 0, 0);
    }

    void Start()
    {
        StartCoroutine(UpdateUI());
        SpeedSlider.maxValue = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Player.transform.position += new Vector3(0, 0, forwardSpeed);
        
    }



    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical * -1, 0, moveHorizontal);

        if (!lockMovement)
        {
            //transform.eulerAngles = new Vector3(0, 0, moveVertical * 45);
            GetComponent<Rigidbody>().angularVelocity = -movement * 5;
        }

        // transform.eulerAngles = new Vector3(
        //     0,
        //     // Mathf.Clamp(transform.eulerAngles.y, -tiltAngle, tiltAngle),
        //     0,
        //     Mathf.Clamp(transform.eulerAngles.z, 90-tiltAngle, 90+tiltAngle)
        // );
    }



    IEnumerator SelfBalance()
    {
        Debug.Log("Now Balancing!");
        Vector3 lastPos = transform.position;
        yield return new WaitForSeconds(1.0f);
    }

    public void UpdateSpeed(float extraSpeedDif)
    {
        //Min : 1.00, Max : 10:00

        if (extraSpeedDif < 0 && ExtraSpeed < Mathf.Abs(extraSpeedDif))
        {
            ExtraSpeed = 0;
        }
        else
        {
            ExtraSpeed += extraSpeedDif;
        }

        forwardSpeed = initSpeed + ExtraSpeed;

        if (forwardSpeed > maxSpeed)
        {
            forwardSpeed = maxSpeed;
            ExtraSpeed = forwardSpeed - initSpeed;
        }

        SpeedSlider.value = forwardSpeed;
        forwardSpeedText.text = "SPEED: " + initSpeed.ToString("0.0") + " + " + ExtraSpeed.ToString("0.0");
    }

    void AddForce(float forceValue)
    {
        Player.GetComponent<Rigidbody>().AddForce(0,0,forceValue);

        //Velocity Min : 1.00, Max : 10:00

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "River")
        {
            //StartCoroutine(speedBoost.PaddleSpeedBoost(other));
            //UpdateSpeed(0.012f);
            AddForce(16.0f);
            Player.GetComponent<Rigidbody>().drag = 0.2f;
            
            
            // RaycastHit hit;
            // if(Physics.Raycast(transform.position, transform.forward, out hit))
            // {
            //     Debug.Log("Point of contact:" + hit.point);
            //     Instantiate(splashPrefab,hit.point,Quaternion.identity);
            // }


        }

    }

    // void OnCollisionEnter(Collision collision)
    // {
        
	// 	if (collision.gameObject.name == "RiverSurface")
    //     {
    //         ContactPoint contact = collision.contacts[0];
    //         Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
    //         Vector3 pos = contact.point;
    //         Debug.Log("collided with water!");
    //         GetComponent<AudioSource>().Play();
    //         Instantiate(splashPrefab, pos, rot);
    //     }
    // }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "River")
        {
            //StartCoroutine(speedBoost.PaddleSpeedBoost(other));
            //speedBoost.SpeedDrop -= 0.001f;
            Player.GetComponent<Rigidbody>().drag = 0.1f;
        }
    }

    IEnumerator UpdateUI()
    {
        SpeedSlider.value = Player.GetComponent<Rigidbody>().velocity.magnitude;
        yield return new WaitForSeconds(0.4f);
        yield return UpdateUI();
    }

}
