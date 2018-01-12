﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PaddleRun;

public class PlayerMovementPaddle02 : MonoBehaviour
{
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
    public float maxSpeed = 2.0f;

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
        UpdateSpeed(0.0f);
        SpeedSlider.maxValue = maxSpeed;
        SpeedSlider.value = forwardSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.position += new Vector3(0, 0, forwardSpeed);
        gameController.AddScore((int)forwardSpeed);
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0, -moveHorizontal * 0.5f, moveHorizontal);

        GetComponent<Rigidbody>().angularVelocity = -movement * 5;

        transform.eulerAngles = new Vector3(
            0,
            // Mathf.Clamp(transform.eulerAngles.y, -tiltAngle, tiltAngle),
            0,
            Mathf.Clamp(transform.eulerAngles.z, 90-tiltAngle, 90+tiltAngle)
        );
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

        if(forwardSpeed > maxSpeed)
        {
            forwardSpeed = maxSpeed;
            ExtraSpeed = forwardSpeed-initSpeed;
        }

        SpeedSlider.value = forwardSpeed;
        forwardSpeedText.text = "SPEED: " + initSpeed.ToString("0.0") + " + " + ExtraSpeed.ToString("0.0");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "River")
        {
            //StartCoroutine(speedBoost.PaddleSpeedBoost(other));
            UpdateSpeed(0.02f);
        }

    }

}