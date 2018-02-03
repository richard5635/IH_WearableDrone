using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDController : MonoBehaviour {

	public SerialHandler serialHandler;

	void Update()
    {
        if ( Input.GetKeyDown(KeyCode.A) ) {
            serialHandler.Write("0");
        }
        if ( Input.GetKeyDown(KeyCode.S) ) {
            serialHandler.Write("1");
        }
    }
}
