using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShootRun;

public class EnemyBehavior : MonoBehaviour {
	public float speed = 1.0f;
	public int hitPoint = 2;
	public string enemyType = "Grunt"; //Apply dropdown? 

	// Use this for initialization
	void Start () {
		
	}

	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Missile"){
			int damage = other.gameObject.GetComponent<missileBehavior>().damage;
			hitPoint -= damage;
			Debug.Log("Enemy got hit!");
		}
		else if( other.gameObject.tag == "Player"){
			hitPoint--;
			Debug.Log("Enemy got hit!");
		}
		if(hitPoint == 0){
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= Vector3.forward * speed;
	}
}
