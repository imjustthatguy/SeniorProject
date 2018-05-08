using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * ScoreManager Class
 * 
 * DESCRIPTION
 * 		The ScoreManager class keeps count of how much score the player has. It
 * 		then updates the text on the screen respectively
 * SYNOPSIS
 * 		public static int score		-> Count of how much score the player has
 * 		public static int killCount	-> Count of how many kills the player has
 * 		
 * 		Text text					-> Reference to Text component
 * 		
*/
public class ScoreManager : MonoBehaviour {

	public static int score = 0;
	public static int killCount = 0;

	Text text;
	/**/
	/*
	ScoreManager::Awake() ScoreManager::Awake()

	NAME

		ScoreManager::Awake - processes initial variables for this class 

	SYNOPSIS

		ScoreManager::Awake()


	DESCRIPTION

		This function is played when the script is called and intializes the text and score
		variables

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/
	void Awake(){
		text = GetComponent<Text> ();
		score = 0;
	}
	/*void ScoreManager::Awake();*/


	/**/
	/*
	ScoreManager::Update() ScoreManager::Update()

	NAME
		
		ScoreManager::Update - Called 60 frames per second
		
	SYNOPSIS

		ScoreManager::Update()
	
	DESCRIPTION
		This function is called 60 frames per second and updates the score displayed on the screen
		
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		30 April 2018

	*/
	/**/
	void Update()
	{
		text.text = "Score: " + score;

	}
	/*void ScoreManager::Update();*/
}
