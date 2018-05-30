using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour {

	 Player p;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void  OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag.Equals( "player"))
		{
			p=col.gameObject.GetComponent<Player> ();
			p.BreathingDamage ();

		}
		if(col.tag.Equals( "vehicule"))
		{

		}


	}

	void  OnTriggerStay2D(Collider2D col)
	{
		if(col.tag.Equals( "player"))
		{
			p=col.gameObject.GetComponent<Player> ();
			p.BreathingDamage ();

		}


	}
}
