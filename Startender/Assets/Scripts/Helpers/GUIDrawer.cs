using UnityEngine;
using System.Collections;

public class GUIDrawer : MonoBehaviour
{
	private GameManager gameManager;
	private DrinkManager drinkManager;

	private GUIText roundTime;
	private GUIText currentOrder;
	private GUIText tipsEarned;

	public int menuXFromCenter;
	public int menuYFromCenter;
	public int menuWidth;
	public int menuHeight;

	public GUIDrawer() {

	}

	public void setManagers(GameManager gameManager, DrinkManager drinkManager) {
		this.gameManager = gameManager;
		this.drinkManager = drinkManager;
	}

	void Start() {

		//setup basic drawing params
		this.menuWidth = 240;
		this.menuHeight = 100;
		this.menuXFromCenter = this.menuWidth / 2;
		this.menuYFromCenter = this.menuHeight / 2;

		//Prep HUD
		GameObject roundTime = GameObject.Find("RoundTime");
		this.roundTime = (GUIText) roundTime.GetComponent(typeof(GUIText));
		this.roundTime.text = "";

		GameObject currentOrder = GameObject.Find("CurrentOrder");
		this.currentOrder = (GUIText) currentOrder.GetComponent(typeof(GUIText));
		this.currentOrder.text = "";

		GameObject tipsEarned = GameObject.Find ("TipsEarned");
		this.tipsEarned = (GUIText) tipsEarned.GetComponent(typeof(GUIText));
		this.tipsEarned.text = "Tips: $0";
	}

	public void drawMainMenu() {
		//Draw Game Menu Here
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));

		this.drawBaseMenu();

		if(GUILayout.Button("Start Game")) {
			Debug.Log("Start Game button clicked");
			gameManager.startGame();
		}
		
		GUILayout.EndArea();
	}

	public void drawPauseMenu() {
		//Draw Game Menu Here
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));

		
		this.drawBaseMenu();
		
		if(GUILayout.Button("Resume")) {
			Debug.Log("Resume button clicked");
			gameManager.resumeRound();
		}
		else if(GUILayout.Button("Reset Round")) {
			Debug.Log ("Resetting Game");
			gameManager.resetGame();
		}
		
		GUILayout.EndArea();
	}

	public void drawHUD() {
		roundTime.text = "Time Left: " + gameManager.getRoundTime().ToString("F0");

		Drink currentDrink = drinkManager.getCurrentDrink();
		currentOrder.text = "Order: " + currentDrink.getDrinkName() + " - " + currentDrink.getFormattedIngredients();

		tipsEarned.text = "Tips: $" + GameManager.getPlayer().getTipsEarned();

	}

	public void drawRoundStats() {
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));
		GUILayout.EndArea();
	}
	
	private void drawBaseMenu() {
		GUILayout.Label("Startender!");
		GUILayout.Space(10.0f);
	}
}

