using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * EnemyHealth Class
 * 
 * DESCRIPTION
 * 		The EnemyHealth class handles all of the behaviors of health associated with the Enemy. It keeps
 * 		track of the current health it has, the value to "sink" the enemy off the worldspace once destroyed,
 * 		a score value to indicate what to add to the the player's score, and its respective audio and animator clips
 * 
 * SYNOPSIS
 * 		int startingHealth 		-> Starting health of enemy
 * 		int currentHealth		-> Health enemy currently has
 * 		float sinkSpeed 		-> How fast the enemy should sink
 * 		int scoreValue			-> Score value of enemy upon death
 * 		AudioClip deathClip		-> Audio source of death clip
 * 		Animator anim			-> Reference to animator
 * 		AudioSource enemyAudio	-> Audio source of enemyAttack;
 * 		ParticleSystem hitParticles -> Particle effect that is instantiated when enemy is shot
 * 		CapsuleCollider capsuleCollider -> Collider object for enemy
 * 		EnemyMovement enemyMovement	-> Reference to EnemyMovement script
 * 		bool isDead				-> Determines if enemy is dead
 * 		bool isSinking			-> Determines if enemy is sinking
*/
public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public float sinkSpeed = 2.5f;
	public int scoreValue = 10;
	public AudioClip deathClip;

	Animator anim;
	AudioSource enemyAudio;
	AudioClip zombieHitClip;
	ParticleSystem hitParticles;
	CapsuleCollider capsuleCollider;
	EnemyMovement enemyMovement;
	bool isDead;
	bool isSinking;


	/**/
	/*
	EnemyHealth::Awake() EnemyHealth::Awake()

	NAME

		EnemyHealth::Awake - processes initial variables for this class 

	SYNOPSIS

		EnemyHealth::Awake()


	DESCRIPTION

		This function is played on Awake and records and stores all the initial variables
		in the EnemyHealth class

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
		anim = GetComponent<Animator> ();
		enemyAudio = GetComponent<AudioSource> ();
		hitParticles = GetComponentInChildren<ParticleSystem> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		enemyMovement = GetComponent<EnemyMovement> ();

		currentHealth = startingHealth;
		zombieHitClip = enemyAudio.clip;
	}
	/* void EnemyHealth::Awake();*/


	/**/
	/*
	EnemyHealth::Update() EnemyHealth::Update()

	NAME
		
		EnemyHealth::Update - Called 60 frames per second and processes isSinking variable

	SYNOPSIS

		EnemyAttack::Update()
	
		isSinking	-> Determines if enemy is sinking out of world space view
	DESCRIPTION

		This function is called 60 frames per second, and checks to see if the isSinking variable
		is set to true. It sets to true if the enemy is dead.
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
		if (isSinking) {

			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
			
	}
	/* void EnemyHealth::Update(); */




	/**/
	/*
	EnemyHealth::TakeDamage() EnemyHealth:TakeDamage()

	NAME
		
		EnemyHealth::TakeDamage - Called when enemy takes damage

	SYNOPSIS

		void EnemyHealth::TakeDamage(int amount, Vector3 hitPoint)
	
			amount		-> amount of damage to deal
			hitPoint 	-> where on the enemy to show hitParticle

	DESCRIPTION

		This function deals damage to the enemy as well as marks for the hitParticle where on the enemy
		it was hit
		
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/
	public void TakeDamage(int amount, Vector3 hitPoint)
	{
		if (isDead)
			return;
		enemyAudio.clip = zombieHitClip;
		enemyAudio.Play ();

		currentHealth -= amount;

		hitParticles.transform.position = hitPoint;
		hitParticles.Play ();

		if (currentHealth <= 0) {
			Death ();

		}

	}
	/* public void EnemyHealth::TakeDamage(int amount, Vector3 hitPoint); */



	/**/
	/*
	EnemyHealth::TakeDamage() EnemyHealth:TakeDamage()

	NAME
		
		EnemyHealth::TakeDamage - Called when enemy takes damage

	SYNOPSIS

		void EnemyHealth::TakeDamage(int amount) PROTOTYPE

			amount		-> amount of damage to deal

	DESCRIPTION

		This function deals damage to the enemy and changes its respective health value
		
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/

	public void TakeDamage(int amount)

	{
		if (isDead)
			return;

		enemyAudio.clip = zombieHitClip;
		enemyAudio.Play ();

		currentHealth -= amount;

		if (currentHealth <= 0) {
			Death ();
		}

	}
	/* public void EnemyHealth::TakeDamage(int amount); */


	/**/
	/*
	EnemyHealth::Death() EnemyHealth:Death()

	NAME
		
		EnemyHealth::Death - Called when enemy dies

	SYNOPSIS

		void EnemyHealth::TakeDamage(int amount)

		isDead		-> boolean determining if enemy is dead
		capsuleCollider -> boolean determining if it is a trigger
		

	DESCRIPTION

		This function displays the enemy dying by setting an animation and playing a death clip

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/
	void Death()
	{
		isDead = true;

		capsuleCollider.isTrigger = true;

		anim.SetTrigger ("Dead");

		enemyAudio.clip = deathClip;
		enemyAudio.Play ();

	}
	/* void EnemyHealth::Death(); */


	/**/
	/*
	EnemyHealth::StartSinking() EnemyHealth:StartSinking()

	NAME
		
		EnemyHealth::StartSinking - Called when enemy dies and sinks the enemy into the ground

	SYNOPSIS

		void EnemyHealth::StartSinking()

		isSinking	-> boolean determining if enemy is sinking
		

	DESCRIPTION

		This function disables the kinematic rigid body component, nav mesh agent component (component used for tracking player)
		and begins sinking the enemy into the ground. It also increases the score manager counts and then eventually destroys the object.
		
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/
	public void StartSinking()
	{
		GetComponent<NavMeshAgent> ().enabled = false;
		GetComponent<Rigidbody> ().isKinematic = true;
		isSinking = true;
		ScoreManager.score += scoreValue;
		ScoreManager.killCount += 1;
		Destroy (gameObject, 2f);

	}
	/* public void EnemyHealth:StartSinking(); */

}
