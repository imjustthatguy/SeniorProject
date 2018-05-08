using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Grenade Class
 * 
 * DESCRIPTION
 * 		The Grenade class handles what the grenade does when its instantiated.
 * 		It has a countdown timer which when reachs 0 instantiates an explosion effect
 * 		onto the world space. It has its OverlapSphere, that, once exploded, will
 * 		generate an array of colliders that it touched. If any of the colliders are enemies,
 * 		we deal damage to them.
 * SYNOPSIS
 * 		public float delay 					-> Delay time for grenade
 * 		public float radius					-> Radius of grenade
 * 		public float force 					-> Force of grenade
 * 		public AudioClip clip				-> Clip for grenade explosion
 * 		public GameObject explosionEffect	-> GameObject that contains the explosion effect
 * 		GameObject explosion				-> Will later be an instance of the explosion
 * 		bool hasExploded					-> Boolean indicating whether grenade has exploded or not
 * 		AudioSource grenadeAudio			-> AudioSource component of grenade
 * 
 * WISHLIST
 * 		Bug where sound from grenade doesnt play. I had it fixed before but it showed up again.
 * 		It has something to do with the audio clip not being able to play because the object is destroyed once it blows up.
 * 		I had a timer set to wait for the object to destroy until the sound is over but it still hasnt fixed it.
*/
public class Grenade : MonoBehaviour {

	public float delay = 3f;
	public float radius = 5f;
	public float force = 700f;
	public AudioClip clip;


	public GameObject explosionEffect;

	GameObject explosion;
	float countdown;
	bool hasExploded = false;
	AudioSource grenadeAudio;




	/**/
	/*
	Grenade::Start() Grenade::Start()

	NAME

		Grenade::Start - processes initial variables for this class 

	SYNOPSIS

		Grenade::Start()


	DESCRIPTION

		This function intializes countdown and grenadeAudio

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		5 May 2018

	*/
	/**/
	// Use this for initialization
	void Start () {
		countdown = delay;
		grenadeAudio = gameObject.GetComponent<AudioSource> ();
	}
	/*void Grenade::Start();*/


	/**/
	/*
	Grenade::Update() Grenade::Update()

	NAME

		Grenade::Update - Called 60 frames per second

	SYNOPSIS

		Grenade::Update()


	DESCRIPTION

		This function updates 60 frames per second and checks to see if countdown has reached.
		If it has, then we explode and play the audio

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		5 May 2018

	*/
	/**/

	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;
		if (countdown <= 0f && !hasExploded) {
			Explode ();
			hasExploded = true;
			grenadeAudio.clip = clip;
			grenadeAudio.Play ();
		}
	}
	/*void Grenade::Update();*/


	/**/
	/*
	Grenade::Explode() Grenade::Explode()

	NAME

		Grenade::Explode - Explodes the grenade

	SYNOPSIS

		Grenade::Explode()


	DESCRIPTION

		This function instantiates an explosion effect onto the world space. It then
		detects and generates a list of colliders that touched the grenade explosion(using the radius)
		and checks each collider if it has an enemy tag. If it does, then we deal damage

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		5 May 2018

	*/
	/**/
	void Explode()
	{
		//show effect
		explosion = Instantiate(explosionEffect, transform.position, transform.rotation);


		//Get nearby objects
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

		foreach(Collider nearbyObject in colliders)
		{
			Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
			//Deal damage to player instead on final release
			//Testing purposes only
			if (rb != null && nearbyObject.gameObject.tag == "Enemy"){
				EnemyHealth enemyHealth = nearbyObject.GetComponent<EnemyHealth> ();
				rb.AddExplosionForce (force, transform.position, radius);

				enemyHealth.TakeDamage (50);

			}

		}

		InvokeRepeating("Wait", 3, 0);

	}

	//Simply destroys objects
	void Wait()
	{
		Destroy (gameObject);
		Destroy (explosion);
	}


}
