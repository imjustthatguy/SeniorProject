using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * GrenadeManager Class
 * 
 * DESCRIPTION
 * 		The GrenadeManager class manages how many grenades the player has in his hand. Intially starting with 3,
 * 		the player's grenade count decreases as he uses more. 
 * SYNOPSIS
 * 		Text textbox			-> Reference to textbox to display how many grenades
 * 		static int grenadeCount	-> Count of grenades in player
 * 		static bool threwGrenade-> Detects if player has just thrown grenade
 * 		
*/


public class GrenadeManager : MonoBehaviour {

	Text textBox;

	public static int grenadeCount;
	public static bool threwGrenade;


	/**/
	/*
	GrenadeManager::Start() GrenadeManager::Start()

	NAME

		GrenadeManager::Start - processes initial variable for this class 

	SYNOPSIS

		GrenadeManager::Start()


	DESCRIPTION

		This function is played on start of the script and initializes all the initial variables
		in the GrenadeManager class

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		24 April 2018

	*/
	/**/
	// Use this for initialization
	void Start () {
		grenadeCount = 3;
		threwGrenade = false;
		textBox = GetComponentInChildren<Text> ();
	}
	/* void GrenadeManager::Start();*/




	/**/
	/*
	GrenadeManager::Update() GrenadeManager::Update()

	NAME

		GrenadeManager::Update - Called 60 frames per second
		
	SYNOPSIS

		GrenadeManager::Update()


	DESCRIPTION

		This function is called 60 frames per second and checks if the player has thrown a grenade.
		If he has, decrease count by 1, then set threwGrenade to false to uncheck.
		
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		24 April 2018

	*/
	/**/
	// Update is called once per frame
	void Update () {
		if (threwGrenade) {
			grenadeCount -= 1;
			threwGrenade = false;
		}


		textBox.text = grenadeCount.ToString ();

	}
	/* void GrenadeManager::Update();*/


}
