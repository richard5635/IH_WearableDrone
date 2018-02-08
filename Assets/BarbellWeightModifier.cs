using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbellWeightModifier : MonoBehaviour
{
    public GameObject[] Weights = new GameObject[2];
	public float WeightDensity = 1.0f;
    public GameObject Stick;
	public float StickDensity = 1.0f;
    private float totalMass;
    private float weightMass = 0;
    private float stickMass = 0;


    // Use this for initialization
    void Start()
    {
		StartCoroutine(AdjustWeight_());
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	IEnumerator AdjustWeight_()
	{
		AdjustWeight();
		yield return new WaitForSeconds(0.2f);
		yield return AdjustWeight_();
	}

    public void AdjustWeight()
    {
        float stickVol = Stick.transform.localScale.x * Stick.transform.localScale.y * Stick.transform.localScale.z;
        stickMass = stickVol * StickDensity;

        float weightVol01 = Weights[0].transform.localScale.x * Weights[0].transform.localScale.y * Weights[0].transform.localScale.z;
        float weightVol02 = Weights[1].transform.localScale.x * Weights[1].transform.localScale.y * Weights[0].transform.localScale.z;
        weightMass = (weightVol01 + weightVol02) * WeightDensity;

        totalMass = stickMass + weightMass;
        GetComponent<Rigidbody>().mass = totalMass;
    }
}
