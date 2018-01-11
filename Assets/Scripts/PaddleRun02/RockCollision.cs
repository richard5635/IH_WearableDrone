using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaddleRun;

public class RockCollision : MonoBehaviour {

	public GameObject explosion;
    //public GameObject playerExplosion;
    //public int scoreValue;
    public PaddleGameController gameController;
	public HealthManager healthManager;
	
    void Awake(){
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		healthManager = GameObject.Find("Paddle(Drone)").GetComponent<HealthManager>();
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<PaddleGameController>();
        }
        if (gameController == null)
        {
            Debug.Log ("Cannot find 'GameController' script");
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Boundary")
        {
            return;
        }
		if(healthManager.Invincible == false)
		{
			if (other.tag == "Player")Instantiate(explosion, transform.position, transform.rotation);
		}
		
        //gameController.AddScore (scoreValue);
        if(other.tag != "Player"){
            //Destroy(other.gameObject);
        }
        //Destroy(gameObject);
    }
}
