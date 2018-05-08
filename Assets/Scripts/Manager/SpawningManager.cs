using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SpawningManager Class
 * 
 * DESCRIPTION
 * 		The SpawningManager handles all of the different spawnable objects in the game.
 * 		It selects out of an array of ~100 spawnpoints randomly and spawns a gameObject given
 * 		the specifed time
 * SYNOPSIS
 * 		public PlayerHealth playerHealth	-> Reference to the playerHealth script
 * 		public float spawnTime				-> Spawn time for enemies
 * 		public float spawnTimeHealth		-> Spawn time for health packs
 * 		public float spawnTimeGrenades		-> Spawn time for grenades
 * 		public Transform[] spawnPoints		-> Array of spawnpoints
 * 		public GameObject[] enemies			-> Array of different type of enemies
 * 		public GameObject HealthPotion		-> HealthPotion object to instantiate
 * 		public GameObject Grenade			-> Grenade object to instantiate
 * 		public static float delta 			-> Time elapsed
 * 		int increase						-> Time to increase
 * 		
*/

public class SpawningManager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public float spawnTime = 3f;
	public float spawnTimeHealth = 30f;
	public float spawnTimeGrenades = 15f;

	public Transform[] spawnPoints;
	public GameObject[] enemies;

	public GameObject HealthPotion;
	public GameObject Grenade;

	public static float delta = 0f;
	int increase = 60;


	/**/
	/*
	SpawningManager::Start() SpawningManager::Start()

	NAME

		SpawningManger::Start - processes initial variables for this class 

	SYNOPSIS

		SpawningManager::Start()


	DESCRIPTION

		This function calls the InvokeRepeating method on the three gameObjects, therefore spawning
		them repeatedly.

	RETURNS

		None

	AUTHOR
			
		Jeremy Hernandez

	DATE

		5 May 2018

	*/
	/**/
	void Start()
	{	
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		InvokeRepeating ("SpawnHealth", spawnTimeHealth, spawnTimeHealth);
		InvokeRepeating ("SpawnGrenade", spawnTimeGrenades, spawnTimeGrenades);
	}
	/*void SpawningManager::Start();*/


	/**/
	/*
	SpawningManager::Spawn() SpawningManager::Spawn()

	NAME

		SpawningManger::Spawn - Spawns enemy
		
	SYNOPSIS

		SpawningManager::Spawn()

		EnemyHealth enemyHealth		-> Reference to the enemy's health
		EnemyMovement enemyMovement	-> Reference to the enemy's movement
		EnemyAttack enemyAttack		-> Reference to the enemy's attack


	DESCRIPTION
		This function spawns an enemy onto the world space. It stops executing once the player dies.
		As time progresses, the enemy's health, movement, and attack get stronger/faster.
		It uses the array and picks a random index and spawns the object at that position.


	RETURNS
		None

	AUTHOR
			
		Jeremy Hernandez

	DATE

		5 May 2018

	*/
	/**/
	void Spawn()
	{
		delta += Time.time - delta;
		//Debug.Log ("Time elapsed ->" + delta + " seconds");

		if (playerHealth.currentHealth <= 0f) {
			return;
		}
		//Every minute increase movement speed of all enemies by one
		//Also increase health and attack
		if (delta >= increase) {
			foreach (GameObject enemy in enemies) {
				EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth> ();
				EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement> ();
				EnemyAttack enemyAttack = enemy.GetComponent <EnemyAttack> ();

				enemyHealth.startingHealth += 10;
				enemyAttack.attackDamage += 5;
				spawnTime -= 0.1f;

			}
			increase += 60;
		}

		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		int enemyIndex = Random.Range (0, enemies.Length);

		Instantiate (enemies[enemyIndex], spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);


	}
	/*void SpawningManager::Spawn();*/


	/**/
	/*
	SpawningManager::SpawnHealth() SpawningManager::SpawnHealth()

	NAME

		SpawningManger::SpawnHealth - Spawns health object
		
	SYNOPSIS

		SpawningManager::SpawnHealth()

	DESCRIPTION
		This function spawns a health object onto the world space. It stops executing once the player dies.
		It uses the array and picks a random index and spawns the object at that position.

	RETURNS
		None

	AUTHOR
		Jeremy Hernandez

	DATE

		5 May 2018

	*/
	/**/
	void SpawnHealth()
	{
		if (playerHealth.currentHealth <= 0f) {
			return;
		}
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (HealthPotion, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
	}
	/*void SpawningManager::SpawnHealth();*/


	/**/
	/*
	SpawningManager::SpawnGrenade() SpawningManager::SpawnGrenade()

	NAME

		SpawningManger::SpawnGrenade - Spawns grenade object
		
	SYNOPSIS

		SpawningManager::SpawnGrenade()

	DESCRIPTION
		This function spawns a grenade object onto the world space. It stops executing once the player dies.
		It uses the array and picks a random index and spawns the object at that position.

	RETURNS
		None

	AUTHOR
		Jeremy Hernandez

	DATE
		5 May 2018

	*/
	/**/
	void SpawnGrenade()
	{
		if (playerHealth.currentHealth <= 0f) {
			return;
		}

		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (Grenade, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);

	}
	/*void SpawningManager::SpawnGrenade();*/

}
