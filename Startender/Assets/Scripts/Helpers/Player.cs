using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour
{
	// Declare properties
	private static PlayerState playerInstance;

	private int starBucks;
	
	// ---------------------------------------------------------------------------------------------------
	// gamestate()
	// --------------------------------------------------------------------------------------------------- 
	// Creates an instance of gamestate as a gameobject if an instance does not exist
	// ---------------------------------------------------------------------------------------------------
	public static PlayerState Instance
	{
		get{
			if(PlayerState.playerInstance == null) {
				//TODO: Fix this
//				PlayerState.playerInstance = new GameObject("PlayerState").AddComponent("Sweet");
			}
			
			return PlayerState.playerInstance;
		}
	}	
	
	// Sets the instance to null when the application quits
	public void OnApplicationQuit() {
		PlayerState.playerInstance = null;
	}
	// ---------------------------------------------------------------------------------------------------
	
	
	// ---------------------------------------------------------------------------------------------------
	// startState()
	// --------------------------------------------------------------------------------------------------- 
	// Creates a new game state
	// ---------------------------------------------------------------------------------------------------
	public void startState()
	{
		Debug.Log("Creating a new Player State");

		this.starBucks = 40;

		//TODO: This Shit
		GameObject.FindGameObjectWithTag("GameManager");
	}

	public float getStarbucks() {
		return this.starBucks;
	}
}
