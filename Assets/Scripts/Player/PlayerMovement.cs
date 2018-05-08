using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * PlayerMovement Class
 * 
 * DESCRIPTION
 * 		The PlayerMovement class handles all of the behaviors of movement associated with the Player. It keeps
 * 		track of the current movement, where the player is looking, as well as animating the player when its doing so
 * 
 * SYNOPSIS
 * 		float speed					-> Speed of player
 * 		Vector3 movement			-> Vector3 position of player
 * 		Animator anim				-> Reference to animator
 * 		Rigidbody playerRigidbood	-> Reference to player's physics
 * 		int floorMask				-> The "floor" the player is looking on
 * 		float camRayLength			-> Float representing how far the ray cast can go
 * 		Camera mainCamera			-> Reference to the main camera in the scene
 * 		static bool useController	-> boolean determining if player is using controller or mouse/keyboard
 * 		
*/

public class PlayerMovement : MonoBehaviour 
{
	public float speed = 6f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;
	Camera mainCamera;
	public static bool useController = false;


	/**/
	/*
	PlayerMovement::Awake() PlayerMovement::Awake()

	NAME

		PlayerMovement::Awake - processes initial variables for this class 

	SYNOPSIS

		PlayerMovement::Awake()


	DESCRIPTION

		This function is played on Awake and records and stores all the initial variables
		in the PlayerMovement class

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		22 April 2018

	*/
	/**/
	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		mainCamera = FindObjectOfType<Camera> ();

	}
	/* void PlayerMovement::Awake();*/




	/**/
	/*
	PlayerMovement::FixedUpdate() PlayerMovement::FixedUpdate()

	NAME
		
		PlayerMovement::FixedUpdate - Called 60 frames per second and processes moving, turning, and animating in the world space

	SYNOPSIS

		PLayerMovement::FixedUpdate()

		float h	 -> float representing the input on the horizontal axis
		float v	 -> float representing the input on the vertical axis
			
	DESCRIPTION

		This function is called every physics update, and manages the moving, turning, and animating in one function
		
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		23 April 2018

	*/
	/**/

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turning ();
		Animating (h, v);
	}
	/* void PlayerMovement::FixedUpdate();*/


	/**/
	/*
	PlayerMovement::Move() PlayerMovement::Move()

	NAME
		
		PlayerMovement::Move - Move position of player

	SYNOPSIS

		PlayerMovement::Move(float h, float v)

		float h	 -> float representing the input on the horizontal axis
		float v	 -> float representing the input on the vertical axis
			
	DESCRIPTION

		This function is called when determining where the position of the player is in the world space and positioning him there. 
		This is called every physics update
		
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		23 April 2018

	*/
	/**/
	void Move (float h, float v)
	{
		//Rotate with Controller
		if (useController) {
			Vector3 playerDirection = Vector3.right * Input.GetAxisRaw ("RHorizontal") + Vector3.forward * -Input.GetAxisRaw ("RVertical");

			if (playerDirection.sqrMagnitude > 0.0f) {
				transform.rotation = Quaternion.LookRotation (playerDirection, Vector3.up);
			}


		}

		movement.Set (h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);

	}
	/* void PlayerMovement::Move(float h, float v);*/


	/**/
	/*
	PlayerMovement::Turning() PlayerMovement::Turning()

	NAME
		
		PlayerMovement::Turning - Turn rotation of player

	SYNOPSIS

		PlayerMovement::Turning()

		Ray camRay				-> Ray cast that is shot out from the camera onto the mouse position in the game
		RaycastHit floorHit 	-> Variable that stores the point where it hit if the camRay hits a position in the game
		
	DESCRIPTION

		This function is called when determining where to rotate the player in the world space. This is called every physics update		
		
	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		23 April 2018

	*/
	/**/

	void Turning()
	{
		Ray camRay = mainCamera.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);



		}

	}
	/* void PlayerMovement::Turning();*/



	/**/
	/*
	PlayerMovement::Animating() PlayerMovement::Animating()

	NAME
		
		PlayerMovement::Animating - Animates player when moving

	SYNOPSIS

		PlayerMovement::Animating(float h, float v)

		float h	 	-> float representing the input on the horizontal axis
		float v	 	-> float representing the input on the vertical axis
		bool walking -> bool determining if player is moving
		
	DESCRIPTION

		This function is called when animating the player. The player only animates when its moving, using boolean walking.

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		23 April 2018

	*/
	/**/
	void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);

	}
	/* void PlayerMovement::Animating();*/


}
