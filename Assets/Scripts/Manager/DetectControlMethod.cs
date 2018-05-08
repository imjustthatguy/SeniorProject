using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * DetectControlMethod Class
 * 
 * DESCRIPTION
 * 		The DetectControlMethod class simply checks to see if the player is using a controller or mouse/keyboard
 * 		and sets the boolean in the PlayerMovement class respectively
 * SYNOPSIS
 * 		No Variables
*/

//Currently a bug in the program where the look rotation doesnt apply to the xbox controller
public class DetectControlMethod : MonoBehaviour {
	/**/
	/*
	DetectControlMethod::Update() DetectControlMethod::Update()

	NAME

		DetectControlMethod::Update - Updates 60 frams a second

	SYNOPSIS

		DetectControlMethod::Update()
		
		PlayerMovement.useController	-> Static boolean value in the PlayerMovement determining whether player is using a controlloer or not


	DESCRIPTION

		This function checks 60 frames per second if input is being received from the mouse/keyboard or controller.
		If input is being received by mouse/keyboard, then player is not using the controller and vice versa.

	RETURNS

		None
		
	AUTHOR

		Jeremy Hernandez

	DATE

		20 April 2018

	*/
	/**/

	// Update is called once per frame
	void Update () {

		//Detect Mouse Input
		if (Input.GetMouseButton (0) || Input.GetMouseButton (1) || Input.GetMouseButton (2)) {
			PlayerMovement.useController = false;
		}

		if (Input.GetAxisRaw ("Mouse X") != 0.0f || Input.GetAxisRaw ("Mouse Y") != 0.0f) {
			PlayerMovement.useController = false;
		}

		//Detect Controller Input
		if (Input.GetAxisRaw ("RHorizontal") != 0.0f || Input.GetAxisRaw ("RVertical") != 0.0f) {
			PlayerMovement.useController = true;
		}

		if(Input.GetKey(KeyCode.JoystickButton0) ||
			Input.GetKey(KeyCode.JoystickButton1) ||
			Input.GetKey(KeyCode.JoystickButton2) ||
			Input.GetKey(KeyCode.JoystickButton3) ||
			Input.GetKey(KeyCode.JoystickButton4) ||
			Input.GetKey(KeyCode.JoystickButton5) ||
			Input.GetKey(KeyCode.JoystickButton6) ||
			Input.GetKey(KeyCode.JoystickButton7) ||
			Input.GetKey(KeyCode.JoystickButton8) ||
			Input.GetKey(KeyCode.JoystickButton9) ||
			Input.GetKey(KeyCode.JoystickButton10) ||
			Input.GetKey(KeyCode.JoystickButton11) ||
			Input.GetKey(KeyCode.JoystickButton12) ||
			Input.GetKey(KeyCode.JoystickButton13) ||
			Input.GetKey(KeyCode.JoystickButton14) ||
			Input.GetKey(KeyCode.JoystickButton15) ||
			Input.GetKey(KeyCode.JoystickButton16) ||
			Input.GetKey(KeyCode.JoystickButton17) ||
			Input.GetKey(KeyCode.JoystickButton18) ||
			Input.GetKey(KeyCode.JoystickButton19)){

			PlayerMovement.useController = true;

		}


	}
	/*void DetectControlMethod::Update();*/

}
