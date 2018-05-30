using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour {

	public AudioClip walkingGrass;
	public AudioSource feetAudioSource;
	private GameObject player;
	private bool isWalking2;
	private Player playerScript;
	void Start () {
		player = GameObject.FindWithTag ("player");
		playerScript = player.GetComponent<Player> ();

		
	}
	
	// Update is called once per frame
	void Update () {
		isWalking2 = playerScript.isWalking;
				/*if (isWalking2) {
			if (!feetAudioSource.isPlaying) {
				feetAudioSource.PlayOneShot (walkingGrass, 1f);
			}
			//playerAudioSource.clip = walkingGrass;
			//playerAudioSource.Play ();

		} else {
			feetAudioSource.Stop ();
		}*/

		
	}
}
