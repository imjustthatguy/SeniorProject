using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * MainMenu Class
 * 
 * DESCRIPTION
 * 		The MainMenu class manages the starting menu and the buttons located in it.
 * SYNOPSIS
 * 		public int index			-> Index into which scene
 * 		public stringlevelName		-> Index into level by name
 * 		public Image black			-> Fade color
 * 		public Animator anim		-> Reference to the animator
 * 		
*/
public class MainMenu : MonoBehaviour {

	public int index;
	public string levelName;

	public Image black;
	public Animator anim;

	/**/
	/*
	MainMenu::PlayGame() MainMenu::PlayGame()

	NAME

		MainMenu::PlayGame - Starts scene

	SYNOPSIS

		MainMenu::PlayGame()


	DESCRIPTION

		Starts the Coroutine of fading into the next scene

	RETURNS

		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/
	public void PlayGame()
	{
		StartCoroutine (Fading ());
	}
	/*public void MainMenu::PlayGame();>*/



	/**/
	/*
	MainMenu::Quit() MainMenu::Quit()

	NAME
		
		MainMenu::Quit 

	SYNOPSIS

		MainMenu::Quit()
			
	DESCRIPTION

		Closes application
		
	RETURNS
	
		None

	AUTHOR

		Jeremy Hernandez

	DATE

		30 April 2018

	*/
	/**/
	public void Quit()
	{
		//Just to make sure we actually quit...
		//Debug.Log ("Quit");
		Application.Quit ();
	}
	/*public void MainMenu::Quit();>*/



	/**/
	/*
	MainMenu::Fading() MainMenu::Fading()

	SYNOPSIS

		MainMenu::Fading
			
	DESCRIPTION

		Coroutine that transtions from start menu to game using animations
		
	RETURNS
	
		None

	AUTHOR

		Jeremy Hernandez

	DATE

		29 April 2018

	*/
	/**/
	IEnumerator Fading()
	{
		anim.SetBool ("Fade", true);
		anim.SetBool ("FadePlay", true);
		yield return new WaitUntil (() => black.color.a == 1);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
	/*IEnumerator MainMenu::Fading();*/
}
