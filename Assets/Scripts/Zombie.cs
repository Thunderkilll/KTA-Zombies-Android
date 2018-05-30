using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zombie : Instantiation {

	private GameObject player;
	public Transform blood; 
	private float walkSpeed = 3f,runSpeed=9f;
	private Animator zombieAnimator;
	//audio
	public AudioClip zombie21,crack11;
	public AudioSource zombieAudioSource;
	//attack and health
	private float health=4f;
	private bool isAttacking=false,died=false;



	void Start () {
		player = GameObject.FindWithTag ("player");
		zombieAnimator = GetComponent<Animator> ();

		
	}
	
	// Update is called once per frame
	void Update () {
		FollowingPlayer ();
		//Invoke("Attack",3f);
		Attack();
	}

	void FollowingPlayer()
	{
		if (Vector2.Distance (transform.position, player.transform.position) < 15f) {
			

			zombieAnimator.SetBool ("move", true);
			//look at player
			Vector3 dir = player.transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			//end look at player

			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, walkSpeed * Time.deltaTime);
		} else {
			zombieAnimator.SetBool ("move", false);

		}
	}

	void Attack()
	{

		if (Vector2.Distance (transform.position, player.transform.position) < 10f) {
			isAttacking = true;
			zombieAudioSource.clip = zombie21;
			zombieAudioSource.Play ();
			Invoke ("SetAttackTrue", 3f);

		} else {
			isAttacking = false;
			zombieAnimator.SetBool ("attack", false);
		}

	}

	void SetAttackTrue()
	{
		zombieAnimator.SetBool ("attack", true);
	}

	public void TakeDamage(float amount)
	{
		if (died == false) {
			
			float x = Random.Range (0, 7f);
			float y = Random.Range (0, 7f);
			Transform bloodClone = (Transform)Instantiate (blood, transform.position + new Vector3 (x, y, 0), Quaternion.identity);
			Destroy (bloodClone.gameObject, 2f);
			zombieAudioSource.Stop ();
			if (!zombieAudioSource.isPlaying) {
				zombieAudioSource.clip = crack11;
				zombieAudioSource.Play ();
			}
			health -= amount;
			if (health <= 0)  {
				died = true;
				Die ();
			}
		}

	}
	private void Die()
	{
		died = true;
		zombieAudioSource.Stop ();
		EnemyLeftCalc ();
		ScorePlus (1);
		Destroy (gameObject);
	}

	void  OnTriggerEnter2D(Collider2D col)
	{
		
		if((col.tag.Equals( "player"))&& (isAttacking))


		{
			player.GetComponent<Player> ().TakeDamage (1f);



		}
	}



}
