using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour {
	GameObject currentSlide;
	GameObject nextSlide;
	GameObject prevSlide;
	GameObject[] slides;
	public GameObject InstructionScreen;


	// Use this for initialization
	void Start () {
		slides = new GameObject[InstructionScreen.transform.childCount];
		for(int i = 0; i < slides.Length; i++)
		{
			slides[i] = InstructionScreen.transform.GetChild(i).gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void NextSlide() {
		currentSlide.SetActive(false);
		nextSlide.SetActive(true);
		currentSlide = nextSlide;

	}

	void PrevSlide() {
		currentSlide.SetActive(false);
		prevSlide.SetActive(true);
		currentSlide = prevSlide;
	}

}
