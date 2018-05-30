using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour {

	private GameObject player;
	void Start () {
		player = GameObject.FindWithTag ("player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void LateUpdate()
	{
		Vector3 newPosition = player.transform.position;
		newPosition.y = player.transform.position.y;
		transform.position = new Vector3 (newPosition.x, newPosition.y, -10f);
		//transform.rotation = Quaternion.Euler (0f, 0f,player.transform.eulerAngles.z);

	}
}
