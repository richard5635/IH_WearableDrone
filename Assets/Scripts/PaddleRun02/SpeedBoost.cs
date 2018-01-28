using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

	public PlayerMovementPaddle02 playerMovement;
	public float SpeedBooster = 0.01f;
	float timer;

	[Header ("Decceleration")]
	public float dropTime = 0.5f;
	public float SpeedDrop = 0.002f;

	// Use this for initialization
	void Start () {
		StartCoroutine(NaturalSpeedDrop());
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator NaturalSpeedDrop()
	{
		yield return new WaitForSeconds(dropTime);
			if(playerMovement.ExtraSpeed > SpeedDrop)
			{
				playerMovement.UpdateSpeed(-SpeedDrop);
			}
			else
			{
				playerMovement.UpdateSpeed(0.0f);
			}
		yield return StartCoroutine(NaturalSpeedDrop());
	}
	public IEnumerator PaddleSpeedBoost(Collider other)
	{
		float maxSpeedUp = 0.2f;
        float curSpeedUp = 0f;
        // while (curSpeedUp < maxSpeedUp)
        // {
        //     if (other.tag == "River")
        //     {
        //         curSpeedUp += 0.01f;
                playerMovement.UpdateSpeed(SpeedBooster);
        //     }
        // }
		yield return null;
	}
}
