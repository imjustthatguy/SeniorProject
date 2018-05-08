using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * PlayerShooting Class
 * 
 * DESCRIPTION
 * 		The PlayerShooting class handles all of the behaviors of shooting associated with the Player. It keeps track
 * 		of where the mouse cursor is pointing, how many shots you can fire per second, and the effects associated
 * 		with firing the gun
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

public class PlayerShooting : MonoBehaviour {

	public int damagePerShot = 20;
	public float timeBetweenBullets = 0.15f;
	public float range = 100f;

	float timer;
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
	ParticleSystem gunParticles;
	LineRenderer gunLine;
	AudioSource gunAudio;
	Light gunLight;
	float effectsDisplayTime = 0.2f;


	void Awake()
	{
		shootableMask = LayerMask.GetMask ("Shootable");
		gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent<LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();

	}

	void Update()
	{
		timer += Time.deltaTime;
		if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets) {
			Shoot ();
		}

		if (PlayerMovement.useController ) {
			if (Input.GetKeyDown (KeyCode.Joystick1Button14)){//|| Input.GetAxisRaw("RTrigger") > 0.0f) {
				Shoot();
			}
				
		}

		if (timer >= timeBetweenBullets * effectsDisplayTime) {
			DisableEffects ();
		}

	}

	public void DisableEffects()
	{
		gunLine.enabled = false;
		gunLight.enabled = false;

	}

	void Shoot()
	{
		timer = 0f;

		gunAudio.Play ();

		gunLight.enabled = true;

		gunParticles.Stop ();
		gunParticles.Play ();

		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth> ();
			if (enemyHealth != null) {

				enemyHealth.TakeDamage (damagePerShot, shootHit.point);

			}
			gunLine.SetPosition (1, shootHit.point);

		} else {
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}

}
