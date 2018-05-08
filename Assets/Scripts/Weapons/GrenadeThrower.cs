using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * GrenadeThrower Class
 * 
 * DESCRIPTION
 * 		The GrenadeThrower class is attached to a transform position on the player, and is the positon
 * 		in which the grenade is thrown from. This script controls how its thrown
 * SYNOPSIS
 * 		public float throwForce			-> Default throw force
 * 		public GameObject grenadePrefab -> Grenade model to throw
 */
public class GrenadeThrower : MonoBehaviour {

	public float throwForce = 40f;
	public GameObject grenadePrefab;



	/**/
	/*
	GrenadeThrower::Update() GrenadeThrower::Update()

	NAME

		GrenadeThrower::Update - Called 60 frames per second

	SYNOPSIS

		GrenadeThrower::Update()
		
		GrenadeManager.threwGrenade -> Static variable we set to true to let the manager know we threw the grenade

	DESCRIPTION

		This function updates 60 frames per second and checks what buttons the user has pressed.
		If player presses rightClick and they have enough grenades, then we throw grenade
		Also set GrenadeManager.threwGrenade to true

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		5 May 2018

	*/
	/**/
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1) && GrenadeManager.grenadeCount >= 1) {
			ThrowGrenade ();
			GrenadeManager.threwGrenade = true;
		}
		
	}
	/*void GrenadeThrower::Update();*/


	/**/
	/*
	GrenadeThrower::ThrowGrenade() GrenadeThrower::ThrowGrenade()

	NAME

		GrenadeThrower::ThrowGrenade - Physically throws the greande

	SYNOPSIS

		GrenadeThrower::ThrowGrenade()

		GameObject grenade		-> Grenade object to throw
		Rigidbody rb			-> Physics component of grenade object
		

	DESCRIPTION

		This function instantiates a grenade into the world space, gets the physics of the grenade,
		and throws it into the world from the transform.position using a force

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		5 May 2018

	*/
	/**/
	void ThrowGrenade()
	{
		GameObject grenade = Instantiate (grenadePrefab, transform.position, transform.rotation);
		Rigidbody rb = grenade.GetComponent<Rigidbody> ();

		rb.AddForce (transform.forward * throwForce, ForceMode.VelocityChange);
	}
	/*void GrenadeThrower::ThrowGrenade();*/
}
