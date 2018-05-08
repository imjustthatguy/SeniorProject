using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * PlayerHealth Class
 * 
 * DESCRIPTION
 * 		The PlayerHealth class handles all of the behaviors of health associated with the Player. It keeps
 * 		track of the current health it has, a slider as a UI element and what to set it to, a damageImage to display 
 * 		when hurt, and manages when the player dies
 * 
 * SYNOPSIS
 * 		int startingHealth 				-> Starting health of enemy
 * 		int currentHealth				-> Health enemy currently has
 * 		Slider healthSlider				-> Slider object represengting on the HUD how much health player has
 * 		Image damageImage				-> Reference to the damageImage thats displayed when player is hurt
 * 		AudioClip deathClip				-> Audio reference to deathClip sound when player dies
 * 		Rigidbody rigidBody				-> Reference to the player's physics
 * 		float flashSpeed				-> Indicates how fast the damageImage should appear
 * 		Color flashColour				-> Indicates what color the damageImage should be, note here its .1 alpha so its transparent
 * 		Animator anim					-> Reference to animator
 * 		AudioSource						-> Reference to the player's audio component
 * 		PlayerMovement playerMovement	-> Reference to the PlayerMovement script
 * 		PlayerShooting playerShooting	-> Reference to the PlayerShooting script
 * 		bool isDead						-> Determines if player is dead
 * 		bool damaged					-> Determines if player is hit
*/


public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public Rigidbody rigidBody;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	Animator anim;
	AudioSource playerAudio;
	PlayerMovement playerMovement;
	PlayerShooting playerShooting;
	bool isDead;
	bool damaged;


	/**/
	/*
	PlayerHealth::Awake() PlayerHealth::Awake()

	NAME

		PlayerHealth::Awake - processes initial variables for this class 

	SYNOPSIS

		PlayerHealth::Awake()


	DESCRIPTION

		This function is played on Awake and records and stores all the initial variables
		in the PlayerHealth class

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		20 April 2018

	*/
	/**/

	void Awake()
	{
		anim = GetComponent<Animator> ();
		playerAudio = GetComponent<AudioSource> ();
		playerMovement = GetComponent<PlayerMovement> ();
		rigidBody = GetComponent<Rigidbody> ();
		playerShooting = GetComponentInChildren<PlayerShooting>();
		currentHealth = startingHealth;

	}
	/* void PlayerHealth::Awake();*/



	/**/
	/*
	PlayerHealth::Update() PlayerHealth::Update()

	NAME
		
		PlayerHealth::Update - Called 60 frames per second and processes if plalyer has been hurt

	SYNOPSIS

		EnemyAttack::Update()

		damaged		-> boolean that if true will display the damageImage to the screen
	
	DESCRIPTION

		This function is called 60 frames per second, and checks to see if the damaged boolean is
		true. If it is, displays it to the user
		
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		20 April 2018

	*/
	/**/

	void Update()
	{
		if (damaged) {
			damageImage.color = flashColour;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;

	}
	/* void PlayerHealth::Update(); */


	/**/
	/*
	PlayerHealth::TakeDamage() PlayerHealth:TakeDamage()

	NAME
		
		PlayerHealth::TakeDamage - Called when player takes damage

	SYNOPSIS

		void PlayerHealth::TakeDamage(int amount)

			amount		-> amount of damage to deal

	DESCRIPTION

		This function deals damage to the player and changes its respective health value
		as well as adjust the health slider respectively
		
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		20 April 2018

	*/
	/**/
	public void TakeDamage(int amount)
	{
		damaged = true;

		currentHealth -= amount;

		healthSlider.value = currentHealth;

		playerAudio.Play ();

		if (currentHealth <= 0f && !isDead) {
			Death();
		}

	}
	/* public void PlayerHealth::TakeDamage(int amount); */


	void Death()
	{
		isDead = true;

		playerShooting.DisableEffects();

		anim.SetTrigger ("Die");

		//Play death clip sound
		playerAudio.clip = deathClip;
		playerAudio.Play ();

		//No more movement after we die
		playerMovement.enabled = false;
		playerShooting.enabled = false;

		rigidBody.isKinematic = true;

	}

	/**/
	/*
	PlayerHealth::OnTriggerEnter() PlayerHealth::OnTriggerEnter()

	NAME

		PlayerHealth::OnTriggerEnter - Determines if player is colliding with other object

	SYNOPSIS

		void PlayerHealth::OnTriggerEnter(Collider other)

		other	-> object that it colided with

	DESCRIPTION

		This function is called when the player collides with another object. We then check the tag of the object to
		see if its the health or grenade tag. If its true, then the player is in range and we "pickup" those objects

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		20 April 2018

	*/
	/**/

	void OnTriggerEnter(Collider other)
	{	
		if (other.gameObject.tag == "Health") {
			if (currentHealth < 100) {
				//Increase health by 10
				currentHealth += 10;
				healthSlider.value = currentHealth;

				Destroy (other.gameObject);
			}

		}

		if (other.gameObject.tag == "Grenade") {
			//Get three grenades
			GrenadeManager.grenadeCount += 3;

			Destroy(other.gameObject);
		}

	}
	/* void PlayerHealth::OnTriggerEnter(Collider other);*/

}
