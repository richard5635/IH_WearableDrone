using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddleCollision : MonoBehaviour {

	
    public GameObject splashPrefab;
	public AudioSource splashSound;
	void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Debug.Log("collided with water!");
			splashSound.Play();
            Instantiate(splashPrefab, pos, rot);
		if (collision.gameObject.name == "River")
        {
            
        }
    }
}
