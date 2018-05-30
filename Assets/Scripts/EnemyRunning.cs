using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyRunning : MonoBehaviour {
	// Use this for initialization
	private static int score = 0;
	public  int kills=0;
	[SerializeField]
	private GameObject[] Weapons;
	protected Rigidbody2D BulletRB;
	GameObject p,g;
	protected Animator animator,animatorGrenade;
	protected Rigidbody2D myRigidbody;
	bool dead,followPlayer = false;
	string level = "level1";
	public Text scoretxt;
	public Image playerHealth;
	private static int colcount = 0;
	private Vector3 offset;
	private Vector3 bulletpos;
	int MoveSpeed = 4;
	int MaxDist = 10;
	int MinDist = 5;
	public float speed = 3f;
	public float rotationSpeed=3f;
	Collider m_Collider;




	void Start () 
	{
		m_Collider = GetComponent<Collider>();
		p = GameObject.Find ("Ahmed");
		g = GameObject.Find ("grenade");

		animator = GetComponent<Animator> ();
		animatorGrenade = g.GetComponent<Animator> ();

		myRigidbody = GetComponent<Rigidbody2D> ();
		scoretxt.text =  score.ToString ();
	}

	void Update ()
	{
		print((  transform.position-bulletpos));

		if ((Vector3.Distance (transform.position, p.transform.position) < 10f) && (!dead)) {
			transform.position = Vector2.MoveTowards (transform.position, p.transform.position, speed * Time.deltaTime);
		}
		//run
		else if(!dead){
			transform.position += new Vector3 (0, 0.01f, 0);
		}
		//print (bulletpos);

	}

	void  OnTriggerEnter2D(Collider2D col)
	{
		if ((col.tag.Equals ("bullet")) && (col.gameObject.transform.position.x-transform.position.x<1 )&& (col.gameObject.transform.position.y-transform.position.y<1 ))
		{
			if (!dead) {
				kills++;

				//Destroy (this.gameObject);
				if (kills == 2) {
					ActivateLayer ("deadLayer");
					dead = true;
					followPlayer = false;
					GetComponent<PolygonCollider2D>().isTrigger = true;	
					score++;
					scoretxt.text = score.ToString ();
					//kills = 0;

				}

				/*SceneManager.LoadScene(level);*/

			} 

		}

		if((col.gameObject.name.Equals( "Ahmed")) &&(dead ==false))
		{

			transform.rotation = Quaternion.Euler(0,0,p.transform.position.x * 90f + p.transform.position.y * 90f);
			StartCoroutine(fire());
			followPlayer = true;

		}
		if (col.gameObject.name.Equals ("grenade")) {
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name.Equals ("grenade")) {
		}

	}
	protected void ActivateLayer(string LayerName)
	{
		for (int i = 0; i < animator.layerCount; i++)
		{
			animator.SetLayerWeight (i, 0);

		}
		animator.SetLayerWeight (animator.GetLayerIndex (LayerName), 1);
	}

	protected void ActivateLayerGrenade(string LayerName)
	{
		for (int i = 0; i < animatorGrenade.layerCount; i++)
		{
			animatorGrenade.SetLayerWeight (i, 0);

		}
		animatorGrenade.SetLayerWeight (animatorGrenade.GetLayerIndex (LayerName), 1);
	}

	IEnumerator fire() 
	{
		var BulletShot = Instantiate(Weapons [0], transform.position, transform.rotation);
		BulletRB = BulletShot.GetComponent<Rigidbody2D> ();
		//if ( (BulletShot.transform.position - transform.position) < 2f) {
		bulletpos = BulletShot.transform.position;
			BulletRB.velocity = (p.transform.position - transform.position).normalized * 5f;
		animatorGrenade.SetBool("exploded",true);
		//StartCoroutine(explodeGrenade());

			Destroy (BulletShot, 3F);
		//animatorGrenade.SetBool("exploded",false);
		//}
		yield return new WaitForSeconds(3);
	}

	IEnumerator explodeGrenade() 
	{
		ActivateLayerGrenade ("explode");
		yield return new WaitForSeconds(2);
	}


	void LateUpdate () 
	{

	}

}
