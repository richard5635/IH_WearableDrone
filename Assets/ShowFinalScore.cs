using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowFinalScore : MonoBehaviour {
	public Text FinalScore;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		GetComponent<TextMeshProUGUI>().text = FinalScore.text;
	}
}
