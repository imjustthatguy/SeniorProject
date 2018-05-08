using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * EnemyAttack Class
 * 
 * DESCRIPTION
 * 		The EnemyAttack class handles all the behaviors associated with an
 * 		enemy attacking the player in the world space, as well as setting the right
 * 		animations for those attacks respectively and dealing the correct amount of damage
 * SYNOPSIS
 * 		float timeBetweenAttacks		-> Time to wait between each attack
 * 		int attackDamage				-> Attack power of enemy
 * 		AudioClip zombieMoan			-> Audio clip of zombie when attacking
 * 		AudioSource audio				-> Audio Source
 * 		Animator anim					-> Reference to animator
 * 		GameObject player				-> Reference to player object
 * 		PlayerHealth playerHealth		-> Reference to the PlayerHealth script
 * 		EnemyHealth enemyHealth			-> Reference to the EnemyHealth script
 * 		bool playerInRange				-> Bool determining whether enemy is in range to attack
 * 		float timer						-> Keeps track of time
*/
public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;
	public AudioClip zombieMoan;

	AudioSource audio;
	Animator anim;
	GameObject player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	bool playerInRange;
	float timer;

	/**/
	/*
	EnemyAttack::Awake() EnemyAttack::Awake()

	NAME
		
		EnemyAttack::Awake - processes initial variables for this class 

	SYNOPSIS

		void EnemyAttack::Awake()
	

	DESCRIPTION

		This function is played on Awake and records and stores all the initial variables
		in the Enemy class
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/
	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		audio = GetComponent<AudioSource> ();
		playerHealth = player.GetComponent<PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponent<Animator>();

	}
	/* void EnemyAttack::Awake();*/


	/**/
	/*
	EnemyAttack::Update() EnemyAttack::Update()

	NAME
		
		EnemyAttack:: Update - Called 60 frames per second and processes variables in class

	SYNOPSIS

		void EnemyAttack:Update()
	
		timer	-> The time between attacks
	DESCRIPTION

		This function is called 60 frames per second, sets and resets the time between attacks and determines
		when the enemy can hit the player. It will also play a clip upon attack, and set different animations depending if
		the player is hit or dead
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/
	void Update()
	{
		timer += Time.deltaTime;

		if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			Attack ();
			audio.clip = zombieMoan;
			audio.Play ();
		}
		if (playerHealth.currentHealth <= 0) {
			anim.SetTrigger ("PlayerDead");
		}
		if (playerHealth.currentHealth <= 0 && playerInRange) {
			anim.SetTrigger ("EatPlayer");
		}

	}
	/* void EnemyAttack::Update();*/


	/**/
	/*
	EnemyAttack::Attack() EnemyAttack::Attack()

	NAME
		
		EnemyAttack:: Attack - Deals damage to the main player in the game if applicable

	SYNOPSIS

		void EnemyAttack::Attack()

		playerHealth	-> The main players Health script	
	

	DESCRIPTION

		This function is called whenever the enemy is within range and can attack the player. If this is true,
		the enemy deals a set amount of damage to the player by calling its playerHealth function
		
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/	
	void Attack()
	{
		timer = 0f;
		if (playerHealth.currentHealth > 0) {
			playerHealth.TakeDamage (attackDamage);
		}

	}
	/* void EnemyAttack::Attack();*/




	/**/
	/*
	EnemyAttack::OnTriggerEnter() EnemyAttack::OnTriggerEnter()

	NAME

		EnemyAttack::OnTriggerEnter - Determines if enemy is colliding with other object

	SYNOPSIS

		void EnemyAttack::OnTriggerEnter(Collider other)

		other	-> object that it colided with

	DESCRIPTION

		This function is called when the enemy collides with another object. We then check the tag of the object to
		see if its the player tag. If its true, then the player is in range and we set the AttackPlayer animation

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/

	void OnTriggerEnter(Collider other)
	{	
		//Check to see if gameObject is player
		if (other.gameObject == player) {
			playerInRange = true;
			anim.SetBool ("AttackPlayer", true);

		}

	}
	/* void EnemyAttack::OnTriggerEnter(Collider other);*/



	/**/
	/*
	EnemyAttack::OnTriggerExit() EnemyAttack::OnTriggerExit()

	NAME

		EnemyAttack::OnTriggerExit - Determines if enemy has left the collider's trigger

	SYNOPSIS

		void EnemyAttack::OnTriggerExit(Collider other)

		other	-> object that it colided with

	DESCRIPTION

		This function is called when the enemy has left the objects' collision detection. If so we
		set playerInRange back to false and disable the attacking animation

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/



	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player) {
			playerInRange = false;
			anim.SetBool ("AttackPlayer", false);

		}

	}
	/* void EnemyAttack::OnTriggerExit(Collider other);*/

}
