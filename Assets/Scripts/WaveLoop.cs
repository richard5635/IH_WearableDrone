using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaddleRun;

public class WaveLoop : MonoBehaviour
{
    public bool spawnOn;
    public PlayerMovementPaddle playerMovement;
    public PaddleGameController gameController;
    private float playerSpeed;

    // Use this for initialization
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovementPaddle>();
        }
        if(spawnOn)
        {
          gameController.SpawnWaves();
        }

    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = playerMovement.forwardSpeed;
        transform.parent.position += new Vector3(0, 0, -playerSpeed);
    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Boundary")
        {
            Debug.Log("Left Boundary!");
            //delete children except wavecollider
            foreach(Transform child in transform.parent)
            {
                if(child.gameObject.tag == "Enemy")
                {
                    Destroy(child.gameObject);
                }
            }

            //move back to initial position;
            spawnOn = true;
            gameController.SpawnWaves();
            transform.parent.position = new Vector3(0,0,500);
        }
    }
}
