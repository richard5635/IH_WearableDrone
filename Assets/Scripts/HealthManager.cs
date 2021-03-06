﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShootRun;
using PaddleRun;

namespace PaddleRun
{
    public class HealthManager : MonoBehaviour
    {
        public int hitPoints = 3;
        [Header("UI")]
        private Slider playerHPSlider;
        public GameObject HPAlert;
        private PaddleGameController gameController;
        public float colSpdDrop = 0.4f;
        public GameObject playerExplosion;
        public PlayerMovementPaddle02 playerMovement;
        public ArduinoCom arduinoCom;
        public float stunRecoveryTime = 2.0f;
        public GameObject paddleEnd01;
        public GameObject paddleEnd02;
        private Color defaultColor;
        public bool Invincible;

        public GameObject rower;
        [Header("Serial Communication")]
        [SerializeField] private SerialHandler serialHandler;




        // Use this for initialization
        void Start()
        {
            defaultColor = paddleEnd01.GetComponent<Renderer>().material.color;
            Invincible = false;

            GameObject gameControllerObject = GameObject.FindWithTag("GameController");
            if (gameControllerObject != null)
            {
                gameController = gameControllerObject.GetComponent<PaddleGameController>();
            }
            if (gameController == null)
            {
                Debug.Log("Cannot find 'GameController' script");
            }

            playerHPSlider = GameObject.Find("PlayerHPSlider").GetComponent<Slider>();
            playerHPSlider.maxValue = hitPoints;
            playerHPSlider.value = hitPoints;
        }

        void OnTriggerEnter(Collider other)
        {
            // float collDis;
            // float collForce;

            // //This script will handle collissions with monsters and hazards
            // if (!Invincible)
            // {
            //     if (other.gameObject.tag == "Enemy")
            //     {
            //         HPAdjustment(-1);
            //         Debug.Log("got hit!");
            //         //playerMovement.StartCoroutine("SelfBalance");
            //         StartCoroutine(StunRecovery());
            //         playerMovement.UpdateSpeed(-colSpdDrop);
            //         gameObject.GetComponent<Rigidbody>().AddExplosionForce(2.0f, other.gameObject.transform.position, 5.0f, 0.0f, ForceMode.Impulse);
            //         collDis = transform.position.x - other.gameObject.transform.position.x;
                    
            //         //Arduino Part
            //         //collForce = Mathf.Abs(other.GetComponent<Rigidbody>().mass * other.GetComponent<Rigidbody>().velocity.z);
            //         //arduinoCom.PhysicalCollision(collDis,collForce);
            //     }
            //     if (other.gameObject.tag == "Monster")
            //     {

            //     }
            // }
        }


        //If the obstacles have IsTrigger ticked
        void OnCollisionEnter(Collision collision)
        {
            string collidedPart = collision.contacts[0].thisCollider.name;
            if (!Invincible)
            {
                ContactPoint contact = collision.contacts[0];
                if (collision.gameObject.tag == "Enemy")
                {
                    HPAdjustment(-1);
                    Debug.Log("got hit!");
                    //playerMovement.StartCoroutine("SelfBalance");
                    StartCoroutine(StunRecovery());
                    playerMovement.UpdateSpeed(-colSpdDrop);
                    gameObject.GetComponent<Rigidbody>().AddExplosionForce(10.0f, contact.point, 5.0f, 0.0f, ForceMode.Impulse);
                    Debug.Log("The collision occurred at :" + collidedPart);
                    //Arduino Part
                    if(collidedPart == "EndLeft")
                    {
                        //LeftPaddle Collision
                        Debug.Log("Left collided with rock");
                        serialHandler.Write("l");
                    }
                    else if(collidedPart == "EndRight")
                    {
                        //RightPaddle Collision
                        Debug.Log("Right collided with rock");
                        serialHandler.Write("r");
                    }
                    //collForce = Mathf.Abs(other.GetComponent<Rigidbody>().mass * other.GetComponent<Rigidbody>().velocity.z);
                    //arduinoCom.PhysicalCollision(collDis,collForce);
                }
                if (collision.gameObject.tag == "Monster")
                {

                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HPAdjustment(int HPChange)
        {
            hitPoints += HPChange;
            Debug.Log("HP Changed by: " + HPChange);
            playerHPSlider.value = hitPoints;
            
            if(playerHPSlider.value / playerHPSlider.maxValue < 0.25f)
            {
                playerHPSlider.fillRect.transform.GetComponent<Image>().color = Color.red;
                HPAlert.SetActive(true);
            }
            if (hitPoints == 0)
            {
                PlayerDies();
                HPAlert.SetActive(false);
            }
        }

        public void PlayerDies()
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(rower);
            gameController.GameOver();
        }

        IEnumerator StunRecovery()
        {
            Invincible = true;
            Physics.IgnoreLayerCollision(8,10,true);
            paddleEnd01.GetComponent<Renderer>().material.color = Color.red;
            paddleEnd02.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(stunRecoveryTime);

            Invincible = false;
            Physics.IgnoreLayerCollision(8,10,false);
            paddleEnd01.GetComponent<Renderer>().material.color = defaultColor;
            paddleEnd02.GetComponent<Renderer>().material.color = defaultColor;
            yield break;
        }
    }
}

