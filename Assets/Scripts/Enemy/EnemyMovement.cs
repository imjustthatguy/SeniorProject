using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
 * EnemyMovement
 * 
 * DESCRIPTION
 * 		The EnemyMovement class handles all of the behaviors associated with the enemy in the worldspace. It uses a navmesh agent, as well as a navmesh
 * 		to track the player in the world. It also has variables to set the movementSpeed and spawnTime;
 * 
 * SYNOPSIS
 * 		int movementSpeed			-> Speed of the enemy
 * 		float timer					-> Variable to keep track of time
 * 		bool spawned				-> Bool indicating if enemy has spawned yet
 * 		Transform player			-> Reference to player
 * 		PlayerHealth playerHealth 	-> Reference to the player's health script
 * 		EnemyHealth enemyHealth		-> Reference to the enemy's health script
 * 		NavMeshAgent nav 			-> Reference to a nav mesh agent
 * 		Animator anim 				-> Reference to an animator
 * 		Vector3 location			-> location in which to spawn the nav mesh agent
*/
public class EnemyMovement : MonoBehaviour {


	public int movementSpeed = 5;

	float spawnTime = 2f;
	float timer;
	bool spawned = false;
	Transform player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	NavMeshAgent nav;
	Animator anim;
	Vector3 location;


	/**/
	/*
	EnemyMovement::Awake() EnemyMovement::Awake()

	NAME

		EnemyMovement::Awake - processes initial variables for this class 

	SYNOPSIS

		EnemyMovement::Awake()


	DESCRIPTION

		This function is played on Awake and records and stores all the initial variables
		in the EnemyMovement class

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
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		anim = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth>();
		nav = GetComponent<NavMeshAgent>();
		nav.speed = movementSpeed;

	}
	/* void EnemyMovement::Awake();*/



	/**/
	/*
	EnemyMovement::Update() EnemyMovement::Health()

	NAME
		
		EnemyMovement::Update - Called 60 frames per second and processes a timer

	SYNOPSIS

		EnemyMovement::Update()

		timer	-> Checks each call if timer has reach the spawn time, if so set the spawned animation
	
	DESCRIPTION

		This function is called 60 frames per second, and checks to see if spawnTime has expired. If it has, then enemy has
		completed the spawn animation and is ready to start navigating towards the player
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		23 April 2018

	*/
	/**/

	//Not fixed update because enemy isnt using physics to detect player, using nav mesh
	void Update()
	{
		timer += Time.deltaTime;

		if (timer >= spawnTime) {
			spawned = true;
			anim.SetBool ("Spawned", spawned);

			if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 && spawned)
			{
				nav.SetDestination(player.position);
			}
			else
			{
				nav.enabled = false;
			}
		}

	}
	/* void EnemyMovement::Update();*/

}
