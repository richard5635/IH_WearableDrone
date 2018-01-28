using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentInfo : MonoBehaviour {
	
	public GameObject drone;
	public Color segmentColor;
	private Color currentColor;

	void OnCollisionEnter(Collision collision)
	{
		StartCoroutine(CollisionColorChange());
		// if(collision.gameObject.tag == "Enemy"){
		// 	drone.GetComponent<HealthManager>().hitPoints--;
		// 	Debug.Log("got hit!");
		// }
		// if(drone.GetComponent<HealthManager>().hitPoints == 0){
		// 	Destroy(transform.parent.gameObject);
		// }
	}
	void Awake()
	{
		currentColor = GetComponent<Renderer>().material.color;
	}

	// Use this for initialization
	void Start () {
		
	}

	IEnumerator CollisionColorChange()
	{
		GetComponent<Renderer>().material.color = segmentColor; 	
		yield return new WaitForSeconds(1);
		GetComponent<Renderer>().material.color = currentColor;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
