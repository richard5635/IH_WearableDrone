using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	public void PaddleRun()
	{
		GameObject go;
		SceneManager.LoadScene("PaddleRun2");
	}
}
