using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	private float bulletSpeed=100f;
	private GameObject player;
	private Vector2 dir;
	private Player p;
	Transform e;

	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		player = GameObject.FindWithTag ("enemy");

	}

	void Update () {
		dir =player.GetComponent<Enemy>().bulletDirection ;

		//dir = GetComponentInParent<Enemy> ().bulletDirection;

		//gameObject.transform.rotation = player.transform.rotation;


	}
	void FixedUpdate () 
	{ 
		myRigidbody.velocity = new Vector2 (dir.x * bulletSpeed, dir.y * bulletSpeed);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("zombie2")) {
			col.gameObject.GetComponent<Zombie2> ().TakeDamage (1f);
		}
	}

}
