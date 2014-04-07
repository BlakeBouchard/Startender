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

    //Added by Rebeca. Used to calculate the "fade" or alpha of the feedback gui.
    private float drinkCompletionTime = -2.0f;

	public int menuXFromCenter;
	public int menuYFromCenter;
    public int menuWidth = 240;
    public int menuHeight = 100;

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

		if (GUILayout.Button("Start Game")) {
			Debug.Log("Start Game button clicked");
			gameManager.StartGame();
		}
		
		GUILayout.EndArea();
	}

	public void DrawPauseMenu() {
		//Draw Game Menu Here
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));

		this.DrawBaseMenu();
		
		if (GUILayout.Button("Resume"))
        {
			Debug.Log("Resuming Game");
			gameManager.ResumeRound();
		}
		else if (GUILayout.Button("Reset Round"))
        {
			Debug.Log ("Resetting Round");
			gameManager.ResetRound();
        }
        else if (Debug.isDebugBuild && GUILayout.Button("End Round")) 
        {
            Debug.Log("Ending round");
            gameManager.EndRound();
        }
        else if (GUILayout.Button("Quit Game"))
        {
            Debug.Log("Quitting game");
            Application.LoadLevel(0);
        }
		
		GUILayout.EndArea();
	}

	public void DrawHUD() {
		roundTime.text = "Time Left: " + gameManager.GetRoundTime().ToString("F0");

		Drink currentDrink = drinkManager.GetCurrentDrink();
		currentOrder.text = "Order: " + currentDrink.GetDrinkName();

		tipsEarned.text = "Tips: $" + player.GetTipsEarned();
		drinksServed.text = "Drinks Served: " + player.GetDrinkCount();

	}

    //Added by Rebeca.
    public void DrawDrinkFeedback()
    {
        //Currently just above the bottom right but we can move it.
        GUILayout.BeginArea(new Rect(Screen.width - this.menuWidth, (Screen.height * 0.9f) - this.menuHeight, this.menuWidth, this.menuHeight));

        GUIStyle fadedText = new GUIStyle();
        fadedText.normal.textColor = new Color(255, 255, 255, 0);

        if (Time.time - drinkCompletionTime < 2)
        {
            fadedText.normal.textColor = new Color(255, 255, 255, (1 - (Time.time - drinkCompletionTime) / 2));
        }

        GUILayout.Label("Finished Drink: " + drinkManager.GetPrevDrinkName(), fadedText);
        int tip = player.GetLastTip();
        GUILayout.Label("Tip: $" + tip, fadedText);
        //TODO: we should pull in a random phrase from a text file.
        GUILayout.Label("Feedback: " + (tip > 0 ? "Ah, that really hit the sun spot." : "What? That wasn't what I ordered!"), fadedText);

        GUILayout.EndArea();
    }

	public void DrawPauseButton()
	{
		GUI.Box(new Rect(Screen.width - 25, 2, 20, 20), "");
		GUILayout.BeginArea(new Rect(Screen.width - 25, 3, 20, 30));
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.normal.textColor = Color.white;
		
		if (GUILayout.Button(" | |  ", myStyle)) {
			gameManager.PauseGame();
		}
		
		GUILayout.EndArea ();
	}

    private void DrawBaseMenu() {
	    GUILayout.Label("Startender!");
	    GUILayout.Space(10.0f);
    }

    public void setDrinkCompletionTime(float value)
    {
        //Setter for the drinkCompletionTime variable
        drinkCompletionTime = value;

        Debug.Log("Set the drink completion time to " + value);
    }
}

