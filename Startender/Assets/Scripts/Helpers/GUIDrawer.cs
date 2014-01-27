using UnityEngine;
using System.Collections;

public class GUIDrawer : MonoBehaviour
{
	private GameManager gameManager;

	private GUIText roundTime;

	public GUIDrawer() {
	}

	public void setGameManager(GameManager gameManager) {
		this.gameManager = gameManager;
	}

	void Start() {
		//Create HUD
		GameObject roundTime = GameObject.Find("RoundTime");
		this.roundTime = (GUIText) roundTime.GetComponent(typeof(GUIText));
		this.roundTime.text = "";
	}

	public void drawMainMenu() {
		//Draw Game Menu Here
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 30, Screen.height /2 - 30, 100, 100));

		this.drawBaseMenu();

		if(GUILayout.Button("Start Game")) {
			Debug.Log("Start Game button clicked");
			gameManager.startGame();
		}
		
		GUILayout.EndArea();
	}

	public void drawPauseMenu() {
		//Draw Game Menu Here
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 30, Screen.height /2 - 30, 100, 100));
		
		this.drawBaseMenu();
		
		if(GUILayout.Button("Resume")) {
			Debug.Log("Resume button clicked");
			gameManager.resumeGame();
		}
		else if(GUILayout.Button("Reset Round")) {
			Debug.Log ("Resetting Game");
			gameManager.resetGame();
		}
		
		GUILayout.EndArea();
	}

	public void drawHUD() {
		roundTime.text = "Time Left: " + gameManager.getRoundTime().ToString("F0");
	}
	
	private void drawBaseMenu() {
		GUILayout.Label("Startender!");
		GUILayout.Space(10.0f);
	}
}

