using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
	[SerializeField]
	protected float speed=5f;

	//public GameObject Player;
	protected Animator animator;
	public Vector2 direction;
	protected Vector3 directionRotation = new Vector3 (0, 0, 10);
	public VirtualJoystick joystick;


	protected Rigidbody2D myRigidbody;
	protected bool isAttacking = false;

	protected bool IsMoving
	{
		get 
		{
			return direction.x != 0 || direction.y != 0;
		}
	}

	// Use this for initialization
	protected virtual void Start () 
	{
		
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		//Animate (direction);
		HandleLayer ();
	}

	protected virtual void FixedUpdate () 
	{
		//Animate (direction);
		Move ();
		CharacterRotation ();
	}

	protected void HandleLayer()
	{
		if (IsMoving) {
			ActivateLayer ("WalkLayer");
			animator.SetFloat ("x", joystick.Horizontal());
			animator.SetFloat ("y", joystick.Vertical());
			//animator.SetFloat ("x", direction.x);
			//animator.SetFloat ("y", direction.y);
		} 
		else if (isAttacking)
		{
			ActivateLayer ("AttackLayer");
		}
			else 
		{ //back to idle
			ActivateLayer("IdleLayer");
		}
	}

	public void Move()
	{
		//transform.Translate(direction*speed*Time.deltaTime);
		myRigidbody.velocity=direction.normalized*speed;

	}

	public void CharacterRotation()
	{
		//transform.Translate(direction*speed*Time.deltaTime);
		//myRigidbody.MoveRotation=directionRotation.normalized*speed;
		//myRigidbody.rotation=100f;


	}
		
	protected void ActivateLayer(string LayerName)
	{
		for (int i = 0; i < animator.layerCount; i++)
		{
			animator.SetLayerWeight (i, 0);

		}
		animator.SetLayerWeight (animator.GetLayerIndex (LayerName), 1);
	}

	protected void StopAttack()
	{
		isAttacking=false;
		animator.SetBool("attack",isAttacking);

		}



}
