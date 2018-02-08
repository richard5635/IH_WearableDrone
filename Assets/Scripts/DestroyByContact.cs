using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShootRun;
using PaddleRun;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    public PaddleGameController gameController;
	
    void Awake(){
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
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
		Instantiate(explosion, transform.position, transform.rotation);
        gameController.AddScore ();
        if(other.tag != "Player"){
            //Destroy(other.gameObject);
        }
        //Destroy(gameObject);
    }
}
