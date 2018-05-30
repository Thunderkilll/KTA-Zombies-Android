using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie2 : Instantiation {

	private GameObject player;
	public Transform blood; 

	private float walkSpeed = 11f,runSpeed=3f;
	private Animator zombieAnimator;
	private bool isAttacking=false,died=false;
	private float health=12f;
	//audio
	public AudioClip zombie21,crack11;
	public AudioSource zombieAudioSource;


	void Start () {
		player = GameObject.FindWithTag ("player");
		zombieAnimator = GetComponent<Animator> ();



	}

	// Update is called once per frame
	void Update () {
		//focusOnPlayer = player.transform.position.x * 90f + player.transform.position.y * 90f;

		FollowingPlayer ();
		//zombieAnimator.SetInteger ("attack", 1);

		Attack ();
	}

	void FollowingPlayer()
	{
		if ((Vector2.Distance (transform.position, player.transform.position) < 15f) && (isAttacking ==false)&& (died ==false)) {
			zombieAnimator.SetInteger ("idle", 0);
			zombieAnimator.SetBool ("run", true);
			//look at player
			Vector3 dir = player.transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			//end look at player


			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, walkSpeed * Time.deltaTime);
		} else {
			zombieAnimator.SetBool ("run", false);
			int randomIdle = Random.Range (1, 3);
			zombieAnimator.SetInteger ("idle", randomIdle);

		}
	}

	void Attack()
	{
		zombieAnimator.SetInteger ("idle", 0);

		if ((Vector2.Distance (transform.position, player.transform.position) < 10f)&& (died ==false)) {
			isAttacking = true;
			int randomAttack = Random.Range (1, 4);
			zombieAnimator.SetInteger ("attack", randomAttack);
		} else {
			isAttacking = false;
			zombieAnimator.SetInteger ("attack", 0);
			int randomIdle = Random.Range (1, 3);
			zombieAnimator.SetInteger ("idle", randomIdle);
		}



	}

	public void TakeDamage(float amount)
	{
		zombieAnimator.SetInteger ("idle", 0);

		if (died == false) {
			float x = Random.Range (0, 7f);
			float y = Random.Range (0, 7f);
			Transform bloodClone = (Transform)Instantiate (blood, transform.position - new Vector3 (x, y, 0), Quaternion.identity);
			Destroy (bloodClone.gameObject, 60f);
			zombieAudioSource.Stop ();
			if (!zombieAudioSource.isPlaying) {
			
				zombieAudioSource.clip = crack11;
				zombieAudioSource.Play ();
			}
			health -= amount;
			if (health < 0) {
				died = true;
				Die ();
			}
		}

	}
	private void Die()
	{
		zombieAnimator.SetInteger ("idle", 0);

		EnemyLeftCalc ();
		ScorePlus (3);
		zombieAudioSource.Stop ();
		int randomDeath = Random.Range (1, 3);
		zombieAnimator.SetInteger ("death", randomDeath);
		GetComponent<CircleCollider2D> ().isTrigger = true;
		//GetComponent<Rigidbody2D> ().
		Destroy (gameObject,60f);
	}

	void  OnTriggerEnter2D(Collider2D col)
	{
		
		if((col.tag.Equals( "player"))&& (isAttacking))
			
		{
			player.GetComponent<Player> ().TakeDamage (1f);


		}


	}



		
}
