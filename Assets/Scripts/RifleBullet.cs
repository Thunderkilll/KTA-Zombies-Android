using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	private float bulletSpeed=100f;
	private GameObject player;
	private Vector2 dir;
	private Player p;

	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		player = GameObject.FindWithTag ("player");
		dir =player.GetComponent<Player>().bulletDirection ;

	}

	void Update () {
		//gameObject.transform.rotation = player.transform.rotation;


	}
	void FixedUpdate () 
	{ 
		myRigidbody.velocity = new Vector2 (dir.x * bulletSpeed, dir.y * bulletSpeed);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("zombie2")) {
			col.gameObject.GetComponent<Zombie2> ().TakeDamage (3f);
			Destroy (gameObject,0.07f);
		}
		if (col.CompareTag ("zombie")) {
			col.gameObject.GetComponent<Zombie> ().TakeDamage (3f);
			Destroy (gameObject,0.07f);

		}
		if (col.CompareTag ("box")) {
			col.gameObject.GetComponent<Box> ().TakeDamage (3f);
			Destroy (gameObject,0.07f);

		}
		if (col.CompareTag ("barrel")) {
			col.gameObject.GetComponent<Barrel> ().TakeDamage (3f);
			Destroy (gameObject,0.07f);

		}
		if (col.CompareTag ("wall")) {
			//col.gameObject.GetComponent<Barrel> ().TakeDamage (2f);
			Destroy (gameObject,0.07f);

		}
	}

}
