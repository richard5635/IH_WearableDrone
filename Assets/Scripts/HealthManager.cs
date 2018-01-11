using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShootRun;
using PaddleRun;

public class HealthManager : MonoBehaviour
{
    public int hitPoints = 3;
    private Slider playerHPSlider;
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
        float collDis;
        float collForce;

        //This script will handle collissions with monsters and hazards
        if (!Invincible)
        {
            if (other.gameObject.tag == "Enemy")
            {
                hitPoints--;
                Debug.Log("got hit!");
                playerHPSlider.value = hitPoints;
                //playerMovement.StartCoroutine("SelfBalance");
                StartCoroutine(StunRecovery());
                playerMovement.UpdateSpeed(-colSpdDrop);
                gameObject.GetComponent<Rigidbody>().AddExplosionForce(2.0f, other.gameObject.transform.position, 5.0f, 0.0f, ForceMode.Impulse);
                collDis = transform.position.x - other.gameObject.transform.position.x;
                collForce = Mathf.Abs(other.GetComponent<Rigidbody>().mass * other.GetComponent<Rigidbody>().velocity.z);
                //arduinoCom.PhysicalCollision(collDis,collForce);
            }
            if (other.gameObject.tag == "Monster")
            {

            }
        }

        if (hitPoints == 0)
        {
            PlayerDies();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerDies()
    {
        Instantiate(playerExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
        gameController.GameOver();
    }

    IEnumerator StunRecovery()
    {
        Invincible = true;
        paddleEnd01.GetComponent<Renderer>().material.color = Color.red;
        paddleEnd02.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(stunRecoveryTime);
        Invincible = false;
        paddleEnd01.GetComponent<Renderer>().material.color = defaultColor;
        paddleEnd02.GetComponent<Renderer>().material.color = defaultColor;
        yield break;
    }
}
