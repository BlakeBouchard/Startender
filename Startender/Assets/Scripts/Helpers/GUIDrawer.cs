using UnityEngine;
using System.Collections;

public class GUIDrawer : MonoBehaviour
{
	private GameManager gameManager;
	private DrinkManager drinkManager;
    public PlayerState player;

	private GUIText roundTime;
	private GUIText currentOrder;
	private GUIText tipsEarned;
	private GUIText drinksServed;

	public int menuXFromCenter = 240;
	public int menuYFromCenter = 100;
	public int menuWidth;
	public int menuHeight;

	public void SetManagers(GameManager gameManager, DrinkManager drinkManager, PlayerState player) {
		this.gameManager = gameManager;
		this.drinkManager = drinkManager;
        this.player = player;
	}

	public virtual void Start() {

		//setup basic drawing params
		this.menuXFromCenter = this.menuWidth / 2;
		this.menuYFromCenter = this.menuHeight / 2;

		this.FindTextLabels();
	}

	public virtual void FindTextLabels() {

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

	public void DrawMainMenu() {
		//Draw Game Menu Here
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));

		this.DrawBaseMenu();

		if(GUILayout.Button("Start Game")) {
			Debug.Log("Start Game button clicked");
			gameManager.StartGame();
		}
		
		GUILayout.EndArea();
	}

	public void DrawPauseMenu() {
		//Draw Game Menu Here
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));

		this.DrawBaseMenu();
		
		if(GUILayout.Button("Resume")) {
			Debug.Log("Resuming Game");
			gameManager.ResumeRound();
		}
		else if(GUILayout.Button("Reset Round")) {
			Debug.Log ("Resetting Round");
			gameManager.ResetRound();
		}
		
		GUILayout.EndArea();
	}

	public void DrawHUD() {
		roundTime.text = "Time Left: " + gameManager.GetRoundTime().ToString("F0");

		Drink currentDrink = drinkManager.GetCurrentDrink();
		currentOrder.text = "Order: " + currentDrink.GetDrinkName() + " - " + currentDrink.GetFormattedIngredients();

		tipsEarned.text = "Tips: $" + player.GetTipsEarned();
		drinksServed.text = "Drinks Served: " + player.GetDrinkCount();

	}

    //Added by Rebeca.
    public void DrawDrinkFeedback()
    {
        //Currently at the bottom right but we can move it.
        GUILayout.BeginArea(new Rect(Screen.width - this.menuWidth, Screen.height - this.menuHeight, this.menuWidth, this.menuHeight));

        GUILayout.Label("Finished Drink: " + drinkManager.GetPrevDrinkName());
        int tip = player.GetLastTip();
        GUILayout.Label("Tip: $" + tip);
        //TODO: we should pull in a random phrase from a text file.
        GUILayout.Label("Feedback: " + (tip == 0 ? "" : (tip > 0 ? "Ah, that really hit the sun spot." : "What? That wasn't what I ordered!") ));

        GUILayout.EndArea();
    }
	
    private void DrawBaseMenu() {
	    GUILayout.Label("Startender!");
	    GUILayout.Space(10.0f);
    }
}

