using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickBehavior : MonoBehaviour {
	public float mFactor = 2.0f;
	private float maxLength = 50.0f;
	private float minLength = 1.0f;
	Vector3 lTemp;
	// Use this for initialization
	void Start () {
		lTemp = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyUp("z")){
			if(transform.localScale.y < maxLength){
				lTemp += new Vector3(0, mFactor, 0);
			}
		}
		if(Input.GetKeyUp("x")){
			if(transform.localScale.y > minLength){
				lTemp -= new Vector3(0, mFactor, 0);
				
			}
		}
		transform.localScale = Vector3.Lerp(transform.localScale,lTemp,Time.deltaTime*5);
	}
}
