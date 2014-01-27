using UnityEngine;
using System.Collections;

public class GUIDrawer : MonoBehaviour
{
	private GameManager gameManager;

	public GUIDrawer() {
	}

	public void setGameManager(GameManager gameManager) {
		this.gameManager = gameManager;
	}

	public void drawMenu() {
		//Draw Game Menu Here
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 30, Screen.height /2 - 30, 100, 100));
		
		if(GUILayout.Button("Start Game")) {
			Debug.Log("Start Game button clicked");
			gameManager.startGame();
		}
		
		GUILayout.EndArea();
	}
}

