using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiculeLevel2 : MonoBehaviour {

	public VirtualJoystick joystick;
	public Vector2 direction;
	protected Rigidbody2D myRigidbody;
	public float a=0f, b=0f,muzzleSize;
	[SerializeField]
	private Transform[] Weapons;
	private GameObject tbp;
	GameObject player,tank;
	public Vector2 bulletDirection;
	public AudioClip tankBullet;
	public AudioSource tankAudioSource;
	public Transform tankMuzzleFlash;
	private GameObject tankBulletOut,tankMuzzleOut;



	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		player = GameObject.FindWithTag ("player");
		tank = GameObject.Find ("Tank");
		tbp = GameObject.Find ("TankBulletPosition");
		tankBulletOut = GameObject.Find ("TankBulletPosition");
		tankMuzzleOut = GameObject.Find ("TankBulletPosition");
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.V)) 
		{
			Shoot ();
		}





	}

	void FixedUpdate()
	{
		Move ();

	}

	void  OnTriggerEnter2D(Collider2D col)
	{
		

	}

	void  OnTriggerStay2D(Collider2D col)
	{

	}

	void  OnTriggerExit2D(Collider2D col)
	{



	}


	/*public void EnterVehicule()
	{
		if (nearVehicule) {

			player.SetActive(inVehicule);
			Vector3 newPlayerPosition = player.transform.position;
			//player.transform.position.x = transform.position.x+1f;
			//player.transform.position.y = transform.position.y+1f;
			newPlayerPosition.x += 0.3f;
			newPlayerPosition.y += 0.3f;
			//player.transform.position = newPlayerPosition;
			player.transform.position = transform.position + new Vector3(3f,3f,0f);


			inVehicule = !inVehicule;

		}
		if ((inVehicule)&&(nearVehicule==false))
		{
			nearVehicule = true;
		}
	}*/
	void Move()
	{
			if (joystick.Vertical () != 0) {
				a = joystick.Vertical ();

			}
			if (joystick.Horizontal () != 0) {
				b = joystick.Horizontal ();
			}
			//if ((joystick.Horizontal () != 0) && (joystick.Horizontal () != 0)) {
			direction = new Vector2 (joystick.Horizontal (), joystick.Vertical ());
			//transform.position.x = joystick.Horizontal ();
			//transform.position.y = joystick.Vertical ();
			//tank.transform.position =direction*6f;
			float angle = Mathf.Atan2 (a, b) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (0, 0, angle+90f);
			myRigidbody.velocity = direction.normalized * 6f;
			bulletDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));



			//}


	}
	public void Shoot()
	{
		
			if (gameObject.name.Equals ("Tank"))
			{
				tankAudioSource.clip = tankBullet;
				tankAudioSource.Play ();
				Transform bulletClone = (Transform)Instantiate (Weapons [0], tbp.transform.position,Quaternion.identity);
				Destroy (bulletClone.gameObject, 2F);
				Transform tankMuzzleFlashClone = (Transform)Instantiate (tankMuzzleFlash, tankMuzzleOut.transform.position,Quaternion.identity);
				muzzleSize = Random.Range (0.2f, 1.5f);
				tankMuzzleFlashClone.rotation = gameObject.transform.rotation;
				tankMuzzleFlashClone.localScale = new Vector3 (muzzleSize, muzzleSize, 0f);
				Destroy(tankMuzzleFlashClone.gameObject,0.03f);

			}

	}


}
