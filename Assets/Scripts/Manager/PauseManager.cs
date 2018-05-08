using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * PauseManager Class
 * 
 * DESCRIPTION
 * 		The PauseManager class simply pauses the game when we click the spacebar
 * SYNOPSIS
 * 		bool isPaused		-> Determines if game is paused or not
 * 		
*/
public class PauseManager : MonoBehaviour {

	bool isPaused = false;


	/**/
	/*
	PauseManager::Update() PauseManager::Update()

	NAME
		
		PauseManager::Update - Updates 60 frames per second

	SYNOPSIS

		PauseManager::Update()


	DESCRIPTION
		
		Updates 60 frams per second and checks the player input. If player
		presses space then the timescale is set to 0 (effectively stopping the game).
		Game resumes if clicked spacebar again.

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		30 April 2018

	*/
	/**/
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			if (!isPaused) {
				Time.timeScale = 0;
				isPaused = true;
			} else {
				Time.timeScale = 1;
				isPaused = false;
			}
		}
	}
}
