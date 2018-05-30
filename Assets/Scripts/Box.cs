using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	float boxHealth=2f;
	public GameObject[] collects;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	public void TakeDamage(float amount)
	{
		boxHealth -= amount;
		if (boxHealth < 0) {
			int c = Random.Range (0, 4);
			Instantiate (collects [c], transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

}
