using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyGame : MonoBehaviour {



	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

		
	protected void ActivateLayer(string LayerName,Animator animator)
	{
		for (int i = 0; i < animator.layerCount; i++)
		{
			animator.SetLayerWeight (i, 0);

		}
		animator.SetLayerWeight (animator.GetLayerIndex (LayerName), 1);
	}

	public  int GetDominantLayer( Animator Animator) {
		int dominant_index = 0;
		float dominant_weight = 0f;
		float weight = 0f;
		for (int index = 0; index < Animator.layerCount; index++) {
			weight = Animator.GetLayerWeight(index);
			if (weight > dominant_weight) {
				dominant_weight = weight;
				dominant_index = index;
			}
		}
		return dominant_index;
	}
}
