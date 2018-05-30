using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MyGame {
	GameObject MainCamera,e;
	bool exploded =false,isMakingDamage=false;
	private GameObject player,enemy;
	//private Enemy enemy;
	private Animator barrelAnimator;
	//audio
	public AudioClip explode,explodemini;
	public AudioSource barrelAudioSource;
	//health
	private float barrelHealth=2f;

	void Start () 
	{
		player = GameObject.FindWithTag ("player");
		enemy = GameObject.FindWithTag ("enemy");
		barrelAnimator = GetComponent<Animator> ();
		MainCamera = GameObject.Find ("Main Camera");

	}
	
	void Update () 
	{


	}
	public void TakeDamage(float amount)
	{
		barrelHealth -= amount;
		if (barrelHealth < 0) {
			if (!barrelAudioSource.isPlaying) {

				barrelAudioSource.clip = explodemini;
				barrelAudioSource.Play ();
			
				barrelAnimator.SetBool ("explode", true);
				ActivateLayer ("ExplosionLayer", barrelAnimator);
				MainCamera.GetComponent<CameraShake> ().shakeDuration = 3f;
				;
				MainCamera.GetComponent<CameraShake> ().Shake ();
				exploded = true;
				isMakingDamage = true;
				Invoke ("MakingDamage", 1.5f);
				Invoke ("Die", 6f);
			}
		}
	}

	/*void  OnTriggerEnter2D(Collider2D col)
	{
		if ((col.tag.Equals ("bullet"))&&(exploded ==false))
		{
			






		}

		
	}*/

	void  OnTriggerStay2D(Collider2D col)
	{
		
		if ((exploded == true)&&(isMakingDamage==true))
		{
			
			if (col.tag.Equals ("zombie")) {
				col.GetComponent<Zombie> ().TakeDamage (0.3f);
				//print ("barel zombie");

			}
			if (col.tag.Equals ("zombie2")) {
				col.GetComponent<Zombie2> ().TakeDamage (1f);
				//print ("barel zombie 2");

			}
			if (col.tag.Equals ("player")) {
				col.GetComponent<Player> ().TakeDamage (0.1f);
				//print ("barel player");

			}
			if (col.tag.Equals ("box")) {
				col.GetComponent<Box> ().TakeDamage (1f);
				//print ("barel player");

			}

		}

	}
	void MakingDamage()
	{
		barrelAudioSource.clip = explode;
		barrelAudioSource.Play ();
		isMakingDamage = false;
		//Destroy (gameObject);
	}
	void Die()
	{
		Destroy (gameObject);
	}

}
