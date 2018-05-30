using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MyGame {
	private static int score = 0;
	public  int kills=0;
	protected Rigidbody2D BulletRB;
	private GameObject player;
	protected Animator EnemyAnimator;
	protected Rigidbody2D myRigidbody;
	bool dead,followPlayer = false;
	string level = "level1";
	public Text scoretxt;
	public Image playerHealth;
	private static int colcount = 0;
	private Vector3 offset;
	int MoveSpeed = 4;
	int MaxDist = 10;
	int MinDist = 5;
	public float enemySpeed = 3f;
	private float rotationSpeed=3f,muzzleSize,muzzleRotation,playerAngle;

	//audio
	public AudioClip ak47,gun,walkingGrass,reloadRifle,reloadGun,emptyGun,emptyRifle;
	public AudioSource playerAudioSource;
	//weapons
	[SerializeField]
	private Transform[] Weapons;
	private int gunMagazine=12,rifleMagazine=30;
	public Transform MuzzleFlash;
	public Transform gunMuzzleFlash;
	private Transform bulletOut,muzzleOut;
	public Vector2 bulletDirection;







	void Start () 
	{
		bulletOut = transform.GetChild (1);
		//bulletOut = GameObject.Find ("BulletOutEnemy");
		//muzzleOut = GameObject.Find ("MuzzleOutEnemy");
		muzzleOut = transform.GetChild(2);

		//feet = GameObject.Find ("FeetEnemy");
		player = GameObject.FindWithTag ("player");
		EnemyAnimator = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
		scoretxt.text =  score.ToString ();
	}

	void Update ()
	{
		//look at player
		Vector3 dir = player.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		bulletDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
		//end look at player


		
		if ((Vector2.Distance (transform.position, player.transform.position) < 15f)&&(!dead))
		{
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, enemySpeed * Time.deltaTime);
		}
	}

	void  OnTriggerEnter2D(Collider2D col)
	{

		if ((col.tag.Equals ("bullet")) && (col.gameObject.transform.position.x-transform.position.x<1 )&& (col.gameObject.transform.position.y-transform.position.y<1 ))
		{
			//Damage ();
		}

		if((col.tag.Equals( "player")) &&(dead ==false))
		{
			

			StartCoroutine(fire());
			followPlayer = true;

		}
	}

	public void Damage()
	{
		if (!dead) {
			dead = true;
			followPlayer = false;
			GetComponent<PolygonCollider2D> ().isTrigger = true;	
			score++;
			scoretxt.text = score.ToString ();
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		
	}


	IEnumerator fire() 
	{
		ShootGun ();

		yield return new WaitForSeconds(3);
	}

	public void ShootGun()
	{
		ActivateLayer ("Gun", EnemyAnimator);
		if (gunMagazine != 0)
		{
			playerAudioSource.clip = gun;
			playerAudioSource.Play ();
			gunMagazine--;
			EnemyAnimator.SetBool ("idle", false);
			EnemyAnimator.SetBool ("shoot", true);
			Transform gunBulletClone = (Transform)Instantiate (Weapons [0], bulletOut.position,Quaternion.identity);
			BulletRB = gunBulletClone.GetComponent<Rigidbody2D> ();
			float accuracy = Random.Range (0f, 1f);
			Vector3 accuracyVector = new Vector3 (transform.position.x+accuracy, transform.position.y+accuracy,0f);
			//BulletRB.velocity= (player.transform.position - transform.position).normalized*70f;
			BulletRB.velocity= (player.transform.position - transform.position ).normalized*70f;

			//BulletRB.velocity += new Vector2 (10f, 0f);
			gunBulletClone.rotation = gameObject.transform.rotation;
			Destroy (gunBulletClone.gameObject, 2F);
			Transform gunMuzzleFlashClone = (Transform)Instantiate (gunMuzzleFlash, muzzleOut.position,Quaternion.identity);
			muzzleSize = Random.Range (0.2f, 1.5f);
			gunMuzzleFlashClone.rotation = gameObject.transform.rotation;
			gunMuzzleFlashClone.localScale = new Vector3 (muzzleSize, muzzleSize, 0f);
			Destroy(gunMuzzleFlashClone.gameObject,0.03f);
		} 
		else 
		{
			playerAudioSource.clip = emptyGun;
			playerAudioSource.Play ();
		}

	}

	void LateUpdate () 
	{
		
	}

}
