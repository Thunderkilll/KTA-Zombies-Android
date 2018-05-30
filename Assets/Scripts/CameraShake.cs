using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	// How long the object should shake for.
	public float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 3f;
	public float decreaseFactor = 1.0f;
	private Vehicule vehiculeScript;
	private bool inVehicule;

	private GameObject player,vehicule,following;       //Public variable to store a reference to the player game object
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.



	private Vector3 offset; 

	Vector3 originalPos;




	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		//FlashScreen ();
		inVehicule = vehicule.GetComponent<Vehicule> ().inVehicule;

		Shake ();
		//transform.position = player.transform.position + offset;

	}
	public void Shake()
	{
		if (inVehicule) {
			following = vehicule;
		} else {
			following = player;
		}
		if (shakeDuration > 0)
		{
			//camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			transform.position = following.transform.position + offset + Random.insideUnitSphere * shakeAmount;


			shakeDuration -= Time.deltaTime * decreaseFactor;
			shakeAmount -= Time.deltaTime * decreaseFactor;

			//transform.position = player.transform.position + offset;

		}
		else
		{
			shakeDuration = 0f;
			shakeAmount = 3f;
			//camTransform.localPosition = originalPos;
			transform.position = following.transform.position + offset;

		}
	}
	void Start()
	{
		
		vehicule = GameObject.FindWithTag ("vehicule");
		player = GameObject.FindWithTag ("player");
		offset = transform.position - player.transform.position;

	}
	void LateUpdate () 
	{
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
	}

		
}