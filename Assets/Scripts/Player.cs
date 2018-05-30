using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MyGame {
	
	public VirtualJoystick joystick;
	[SerializeField]
	private Transform[] Weapons;
	private float walkSpeed=17f,runSpeed=14f;
	private Animator playerAnimator,feetAnimator;
	private Vector2 direction;
	public Vector2 bulletDirection;
	protected Vector3 directionRotation = new Vector3 (0, 0, 10),shottingPos= new Vector3 (0, 0, 0);
	protected Rigidbody2D myRigidbody;
	protected bool isAttacking = false;
	private float a=0f, b=0f,muzzleSize,muzzleRotation;
	public float angle;
	public Image playerHealth,BreathingHealth;
	private float playerHealthValue=50f,BreathingHealthValue=100f;
	private bool isTakingBreathingDamage=false,isMelee=false,automaticFire=false;
	protected float lastfired,FireRate=10f;

	public bool inVehicule,nearVehicule=false,dead=false,isWalking = false,isMove=false,isReload=false;
	private Vehicule v;
	public GameObject damageScreen;
	public GameObject deathScreen;
	private int gunMagazine=12,gunAmmo=0,rifleMagazine=30,rifleAmmo=0;
	public bool idle;
	private GameObject feet,bulletOut,muzzleOut;
	private Text GunMagazineText,GunAmmoText,RifleMagazineText,RifleAmmoText;
	public Transform MuzzleFlash;
	public Transform gunMuzzleFlash;
	public AudioClip ak47,gun,reloadRifle,reloadGun,emptyGun,emptyRifle,heartBeat;
	public AudioSource playerAudioSource;


	public void setAutomaticFireTrue()
	{
		automaticFire =true;
	}
	public void setAutomaticFireFalse()
	{
		automaticFire =false;
	}
	public void Melee()
	{
		isMelee = true;
		playerAnimator.SetBool ("melee", isMelee);
	}

	public void StopMelee()
	{
		isMelee = false;
		playerAnimator.SetBool ("melee", isMelee);
	}

	public void Reload()
	{
		isReload = !isReload;
		if (GetDominantLayer (playerAnimator) == 1) {
			if (gunAmmo > 0) {
				playerAnimator.SetBool ("reload", isReload);

				
				playerAudioSource.clip = reloadGun;
				playerAudioSource.Play ();
				if (gunAmmo <= 12 - gunMagazine) {
					int gunMagazine2 = gunMagazine;
					gunMagazine += gunAmmo;
					gunAmmo = 0;

				} else {
					gunAmmo -= 12 - gunMagazine;
					gunMagazine = 12;
				}
				GunMagazineText.color = Color.yellow;
				GunMagazineText.text = gunMagazine.ToString ();
				GunAmmoText.text = gunAmmo.ToString ();


			}
		}
		if (GetDominantLayer (playerAnimator) == 2) {
			if (rifleAmmo > 0) {
				playerAnimator.SetBool ("reload", isReload);

				playerAudioSource.clip = reloadRifle;
				playerAudioSource.Play ();
				if (rifleAmmo <= 30 - rifleMagazine) {
					int rifleMagazine2 = rifleMagazine;
					rifleMagazine += rifleAmmo;
					rifleAmmo = 0;

				} else {
					rifleAmmo -= 30 - rifleMagazine;
					rifleMagazine = 30;
				}
				RifleMagazineText.color = Color.yellow;
				RifleMagazineText.text = rifleMagazine.ToString ();
				RifleAmmoText.text = rifleAmmo.ToString ();


			}
		}
	}

	public void StopReload()
	{
		isReload = !isReload;
		playerAnimator.SetBool ("reload", isReload);
	}

	public void Move()
	{
		isMove = true;
		playerAnimator.SetBool ("move", isMove);
		direction.x = joystick.Horizontal();
		direction.y = joystick.Vertical();
		myRigidbody.velocity=direction.normalized*walkSpeed;

	}

	public void StopMove()
	{
		isMove = !isMove;
		playerAnimator.SetBool ("move", isMove);
	}

	public void MeleeKnife()
	{
		ActivateLayer ("Knife", playerAnimator);
		isMelee = true;
		playerAnimator.SetBool ("melee", isMelee);
	}

	public void StopMeleeKnife()
	{
		isMelee = false;
		playerAnimator.SetBool ("melee", isMelee);

	}

	public void ShootGun()
	{
		ActivateLayer ("Gun", playerAnimator);
		if (gunMagazine != 0)
		{
			playerAudioSource.clip = gun;
			playerAudioSource.Play ();
			GunMagazineText.color = Color.yellow;
			gunMagazine--;
			GunMagazineText.text = gunMagazine.ToString ();
			playerAnimator.SetBool ("idle", false);
			playerAnimator.SetBool ("shoot", true);
			Transform gunBulletClone = (Transform)Instantiate (Weapons [0], bulletOut.transform.position,Quaternion.identity);
			gunBulletClone.rotation = gameObject.transform.rotation;
			Destroy (gunBulletClone.gameObject, 2F);
			Transform gunMuzzleFlashClone = (Transform)Instantiate (gunMuzzleFlash, muzzleOut.transform.position,Quaternion.identity);
			muzzleSize = Random.Range (0.2f, 1.5f);
			gunMuzzleFlashClone.rotation = gameObject.transform.rotation;
			gunMuzzleFlashClone.localScale = new Vector3 (muzzleSize, muzzleSize, 0f);
			Destroy(gunMuzzleFlashClone.gameObject,0.03f);
		} 
		else 
		{
			playerAudioSource.clip = emptyGun;
			playerAudioSource.Play ();
			GunMagazineText.color = Color.red;
		}

		}

	public void ShootRifle()
	{
			if (Time.time - lastfired > 1 / FireRate) {
				lastfired = Time.time;
				ActivateLayer ("Rifle", playerAnimator);
				if (rifleMagazine != 0) {
					playerAudioSource.clip = ak47;
					playerAudioSource.Play ();
					RifleMagazineText.color = Color.yellow;
					rifleMagazine--;
					RifleMagazineText.text = rifleMagazine.ToString ();
					playerAnimator.SetBool ("idle", false);
					playerAnimator.SetBool ("shoot", true);
					Transform rifleBulletClone = (Transform)Instantiate (Weapons [1], bulletOut.transform.position, Quaternion.identity);
					rifleBulletClone.rotation = gameObject.transform.rotation;
					Destroy (rifleBulletClone.gameObject, 3F);
					Transform MuzzleFlashClone = (Transform)Instantiate (MuzzleFlash, muzzleOut.transform.position, Quaternion.identity);
					muzzleSize = Random.Range (0.1f, 3.5f);
					muzzleRotation = Random.Range (0f, 90f);
					MuzzleFlashClone.rotation = Quaternion.Euler (muzzleRotation, 0f, angle);
					MuzzleFlashClone.localScale = new Vector3 (muzzleSize, muzzleSize, 0f);
					//yield return 0;  Destroy(muzzleFlashClone); //skip 1 frame
					Destroy (MuzzleFlashClone.gameObject, 0.03f);
				} else {
					playerAudioSource.clip = emptyRifle;
					playerAudioSource.Play ();
					RifleMagazineText.color = Color.red;
				}
			}




	}

	public void StopShoot()
	{
		playerAnimator.SetBool ("shoot", false);

	}

	 void Start () 
	{
		GunMagazineText = GameObject.Find ("GunMagazineText").gameObject.GetComponent<Text> ();
		GunAmmoText = GameObject.Find ("GunAmmoText").gameObject.GetComponent<Text> ();

		RifleMagazineText = GameObject.Find ("RifleMagazineText").gameObject.GetComponent<Text> ();
		RifleAmmoText = GameObject.Find ("RifleAmmoText").gameObject.GetComponent<Text> ();

		bulletOut = GameObject.Find ("BulletOut");
		muzzleOut = GameObject.Find ("MuzzleOut");

		feet = GameObject.Find ("Feet");
		feetAnimator = feet.GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
		playerAnimator = GetComponent<Animator> ();


	}
		
	  void Update () 
	{
		if (automaticFire)
			ShootRifle ();
		else
		{
			StopShoot();
		}
		Mouvement ();
		PCShooting ();

	}
	 void FixedUpdate () 
	{
		Move ();
	}


	protected void Mouvement()
	{
		if (joystick.Vertical () != 0)
		{
				 a = joystick.Vertical ();
				isWalking = true;
				isMove = true;
				feetAnimator.SetBool ("run", true);
		}
			if (joystick.Horizontal () != 0)
			{
				 b = joystick.Horizontal ();
				isWalking = true;
				isMove = true;
				feetAnimator.SetBool ("run", true);
			}
			if ((joystick.Horizontal () == 0)&&(joystick.Horizontal () == 0)) 
			{
				playerAnimator.SetBool("move",false);
				isMove = false;
				isWalking = false;
				feetAnimator.SetBool ("run", false);
			}
			 angle = Mathf.Atan2(a,b) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler( 0, 0,angle);
			bulletDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));




	}

	protected void StopAttack()
	{
		isAttacking=false;
		playerAnimator.SetBool("attack",isAttacking);

	}

	public void TakeDamage(float amount)
	{
		if (!playerAudioSource.isPlaying) {
			playerAudioSource.clip = heartBeat;
			playerAudioSource.Play ();
		}
		if (playerHealthValue > 0f) {
			playerHealth.fillAmount -= amount*0.02f;
			playerHealthValue -= amount;
			Invoke ("ShowDamageScreen", 0f);
			Invoke ("DissmissDamageScreen", 0.5f);
		}
		else
		{
			dead = true;
			Invoke ("ShowGameOverScreen", 0f);

		}
	}
	public void ShowDamageScreen()
	{
		damageScreen.SetActive (true);

	}

	public void ShowGameOverScreen()
	{
		deathScreen.SetActive (true);

	}
	public void DissmissDamageScreen()
	{
		damageScreen.SetActive (false);

	}

	public void BreathingDamage()
	{
		if (BreathingHealthValue >= 0f) {
			BreathingHealth.fillAmount -= 0.01f;
			BreathingHealthValue -=1f;
		}
		if (BreathingHealthValue < 0f)  {
			TakeDamage (0.1f);
			}

	}
	public void HealBreathing()
	{
		if (BreathingHealthValue < 100f) {

			BreathingHealth.fillAmount += 0.01f;
			BreathingHealthValue+=1f;
		}
		
	}

	void  OnTriggerEnter2D(Collider2D col)
	{

		if(col.tag.Equals( "heart"))
		{
			playerHealthValue += 10f;
			playerHealth.fillAmount += 10f*0.02f;
			Destroy (col.gameObject);
		}

		if(col.tag.Equals( "gunAmmo"))
		{
			int r = Random.Range (1, 12);
			gunAmmo += r;
			GunAmmoText.text = gunAmmo.ToString ();
			Destroy (col.gameObject);
		}

		if(col.tag.Equals( "rifleAmmo"))
		{
			int r = Random.Range (1, 30);
			rifleAmmo += r;
			RifleAmmoText.text = rifleAmmo.ToString ();
			Destroy (col.gameObject);
		}



	}

	void  OnTriggerStay2D(Collider2D col)
	{
		if (col.tag.Equals ("box")) {
			if (isMelee) {
				col.GetComponent<Box> ().TakeDamage (0.05f);
			}
		}


	}
	void  OnTriggerExit2D(Collider2D col)
	{

		/*if(col.tag.Equals( "vehicule"))
		{
			nearVehicule = false;


		}*/


	}

public void PCShooting()
{
	if (Input.GetKey (KeyCode.Space)) 
	{
			ShootRifle ();
	}
	if (Input.GetKeyUp (KeyCode.Space)) 
	{
		StopShoot ();
	}
		if (Input.GetKeyDown (KeyCode.N)) 
		{
			ShootGun ();
		}
		if (Input.GetKeyUp (KeyCode.N)) 
		{
			StopShoot ();
		}
	if (Input.GetKeyDown (KeyCode.M)) 
	{
		Melee ();
	}
	if (Input.GetKeyUp (KeyCode.M)) 
	{
		StopMelee ();
	}
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			Reload ();
		}
		if (Input.GetKeyUp (KeyCode.R)) 
		{
			StopReload ();
		}
}



		
}
