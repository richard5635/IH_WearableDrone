using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightSizeControl : MonoBehaviour {
	public float mFactor = 0.2f;
	private float maxSize;
	private float minSize;
	Vector3 lTemp;

	// Use this for initialization
	void Start () {
		lTemp = transform.localScale;
		maxSize = transform.localScale.x + 2;
		minSize = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("a")){
			if(transform.localScale.y < maxSize){
				lTemp += new Vector3(mFactor, mFactor, mFactor);
			}
		}
		if(Input.GetKeyUp("s")){
			if(transform.localScale.y > minSize){
				lTemp -= new Vector3(mFactor, mFactor, mFactor);
				
			}
		}
		transform.localScale = Vector3.Lerp(transform.localScale,lTemp,Time.deltaTime*5);
	}
}
