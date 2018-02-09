using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPosition : MonoBehaviour {
	public GameObject Target;
	public bool Left;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(
			0,
			Target.transform.localScale.y * (Left ? -1 : 1),
			-0.1f
		);
	}
}
