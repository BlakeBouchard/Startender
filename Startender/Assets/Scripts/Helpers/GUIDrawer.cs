using UnityEngine;
using System.Collections;

public class GUIDrawer : MonoBehaviour
{
	private GameManager gameManager;
	private DrinkManager drinkManager;

	private GUIText roundTime;
	private GUIText currentOrder;
	private GUIText tipsEarned;
	private GUIText drinksServed;

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
		this.tipsEarned.text = "";

		GameObject drinkCount = GameObject.Find ("DrinkCount");
		this.drinksServed = (GUIText) drinkCount.GetComponent(typeof(GUIText));
		this.drinksServed.text = "";
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
			Debug.Log("Resuming Game");
			gameManager.resumeRound();
		}
		else if(GUILayout.Button("Reset Round")) {
			Debug.Log ("Resetting Round");
			gameManager.resetRound();
		}
		
		GUILayout.EndArea();
	}

	public void drawHUD() {
		roundTime.text = "Time Left: " + gameManager.getRoundTime().ToString("F0");

		Drink currentDrink = drinkManager.getCurrentDrink();
		currentOrder.text = "Order: " + currentDrink.getDrinkName() + " - " + currentDrink.getFormattedIngredients();

		PlayerState player = GameManager.getPlayer();

		tipsEarned.text = "Tips: $" + player.getTipsEarned();
		drinksServed.text = "Drinks Served: " + player.getDrinkCount();

	}

	public void drawRoundStats() {
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));

		GUILayout.Label("Drinks Served: " + GameManager.getPlayer().getDrinkCount());
		GUILayout.Label("Tips: $" + GameManager.getPlayer().getTipsEarned());
		GUILayout.Label("Starbucks: $" + GameManager.getPlayer().getStarbucks());

		if(GUILayout.Button("Next Round")) {
			Debug.Log ("Resetting Round");
			GameManager.getPlayer().endRound();
			gameManager.resetRound();
		}

		GUILayout.EndArea();
	}

    //Added by Rebeca.
    public void drawDrinkFeedback()
    {
        //Currently at the bottom right but we can move it.
        GUILayout.BeginArea(new Rect(Screen.width - this.menuWidth, Screen.height - this.menuHeight, this.menuWidth, this.menuHeight));

        GUILayout.Label("Finished Drink: " + drinkManager.getPrevDrinkName());
        int tip = GameManager.getPlayer().getLastTip();
        GUILayout.Label("Tip: $" + tip);
        //TODO: we should pull in a random phrase from a text file.
        GUILayout.Label("Feedback: " + (tip == 0 ? "" : (tip > 0 ? "Ah, that really hit the sun spot." : "What? That wasn't what I ordered!") ));

        GUILayout.EndArea();
    }

	private void drawBaseMenu() {
		GUILayout.Label("Startender!");
		GUILayout.Space(10.0f);
	}
}

