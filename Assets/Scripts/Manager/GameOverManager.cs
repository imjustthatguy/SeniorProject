using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/*
 * GameOverManager Class
 * 
 * DESCRIPTION
 * 		The GameOverManager class is executed when the player dies and displays a Game Over message to the user, as 
 * 		well as two buttons on whether to try again or quit
 * SYNOPSIS
 * 		PlayerHealth playerHealth		-> Reference to PlayerHealth script
 * 		float restartDelay				-> Delay by 5 seconds
 * 		int score						-> Current score of player
 * 		int killCount					-> Kill count of player
 * 		float timeAlive					-> Time spent alive in game
 * 		Animator anim					-> Reference to the animator
 * 		float restartTimer				-> Restart the timer
 * WISHLIST
 * 		Wanted to be able to display killCount, timeSpentAlive and score to user. Lots of bugs and time constraints
 * 		prevented this from happening
*/

public class GameOverManager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public float restartDelay = 5f;
	public int score;
	public int killCount;
	public float timeAlive;


	Animator anim;
	float restartTimer;

	/**/
	/*
	GameOverManager::Awake() GameOverManager::Awake()

	NAME

		GameOverManager::Awake - processes initial variables for this class 

	SYNOPSIS

		GameOverManager::Awake()


	DESCRIPTION

		This function is played when the script is called and records and intializes all the variables
		in the GameOverManager class

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		30 April 2018

	*/
	/**/
	void Awake()
	{
		score = ScoreManager.score;
		killCount = ScoreManager.killCount;
		timeAlive = SpawningManager.delta;
		anim = GetComponent<Animator> ();

	}
	/* void GameOverManager::Awake();*/



	/**/
	/*
	GameOverManager::Update() GameOverManager::Update()

	NAME
		
		GameOverManager::Update - Called 60 frames per second and processes if player health is less than 0

	SYNOPSIS

		GameOverManager::Update()

		playerHealth.currentHealth		-> current health of player
	
	DESCRIPTION

		This function is called 60 frames per second, and checks to see if player's health is less than 0.
		If it is, then we will activate the GameOver animation, rendering the message and buttons
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

		if (playerHealth.currentHealth <= 0) {
			anim.SetTrigger ("GameOver");
		}


	}
	/* void GameOverManager::Update(); */


	/**/
	/*
	GameOverManager::PlayAgain() GameOverManager::PlayAgain()

	NAME
		
		GameOverManager::PlayAgain - 

	SYNOPSIS

		GameOverManager::PlayAgain()
			
	DESCRIPTION

		Simply loads the same scene to replay 
		
	RETURNS
	
		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/
	public void PlayAgain()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

	}
	/* public void GameOverManager::PlayAgain(); */


	/**/
	/*
	GameOverManager::Quit() GameOverManager::Quit()

	NAME
		
		GameOverManager::Quit 

	SYNOPSIS

		GameOverManager::Quit()
			
	DESCRIPTION

		Simply quits the game
		
	RETURNS
	
		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/
	public void Quit()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);

	}
	/* public void GameOverManager::Quit(); */


}


