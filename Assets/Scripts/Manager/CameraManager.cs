using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * CameraManager Class
 * 
 * DESCRIPTION
 * 		The CameraManager class manages where the camera is pointing at in the worldspace. It 
 * 		has a smoothing value and a target to specify who to follow.
 * SYNOPSIS
 * 		Transform target	-> Player's position
 * 		float smoothing		-> Float representing how smooth to move the camera
 * 		Vector3 offset		-> Difference in position from the current place the target is at and where it was originally
*/
public class CameraManager : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;

	Vector3 offset;

	/**/
	/*
	CameraManager::Start() CameraManager::Start()

	NAME

		CameraManager::Start - processes initial variable for this class 

	SYNOPSIS

		CameraManager::Start()

		offset		-> Sets the position of the offset


	DESCRIPTION

		This function is played on start of the script and records and stores all the initial variables
		in the PlayerHealth class

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		24 April 2018

	*/
	/**/
	void Start()
	{
		offset = transform.position - target.position;

	}
	/* void CameraManager::Start();*/


	/**/
	/*
	CameraManager::FixedUpdate() CameraManager::FixedUpdate()

	NAME

		CameraManager::FixedUpdate - Called every physics update 
	SYNOPSIS

		CameraManager::FixedUpdate()

		targetCamPos		-> Sets the position of the camera


	DESCRIPTION

		This function is called every physics update and manages where the camera position is relative to the target

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		24 April 2018

	*/
	/**/
	//Used for objects with physics, as opposed to Update()
	void FixedUpdate()
	{
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);

	}
	/* void CameraManager::FixedUpdate();*/

}
