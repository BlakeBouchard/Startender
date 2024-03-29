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

	public Font font;

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

	}

	public void DrawMainMenu() {
		//Draw Game Menu Here
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));
		this.DrawBaseMenu();

		if (GUILayout.Button("<size=20>Start Game</size>")) {
			Debug.Log("Start Game button clicked");
			gameManager.StartGame();
		}
		
		GUILayout.EndArea();
	}

	public void DrawPauseMenu() {
		//Draw Game Menu Here
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));		this.DrawBaseMenu();
		
		if (GUILayout.Button("<size=20>Resume</size>"))
        {
			Debug.Log("Resuming Game");
			gameManager.ResumeRound();
		}
		else if (GUILayout.Button("<size=20>Reset Round</size>"))
        {
			Debug.Log ("Resetting Round");
			gameManager.ResetRound();
        }
		else if (Debug.isDebugBuild && GUILayout.Button("<size=20>End Round</size>")) 
        {
            Debug.Log("Ending round");
            gameManager.EndRound();
        }
		else if (GUILayout.Button("<size=20>Quit Game</size>"))
        {
            Debug.Log("Quitting game");
            Application.LoadLevel(0);
        }
		
		GUILayout.EndArea();
	}

	public void DrawHUD() {
		roundTime.text = gameManager.GetRoundTime().ToString("F0");

		Drink currentDrink = drinkManager.GetCurrentDrink();
		currentOrder.text = currentDrink.GetDrinkName();

		tipsEarned.text = "Tips: $" + player.GetTipsEarned();

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
		GUI.Box(new Rect(Screen.width - 35, 2, 30, 30), "");
		GUILayout.BeginArea(new Rect(Screen.width - 35, 3, 30, 30));
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.normal.textColor = Color.white;
		
		if (GUILayout.Button("<size=20> | |  </size>", myStyle)) {
			gameManager.PauseGame();
		}
		
		GUILayout.EndArea ();
	}

    private void DrawBaseMenu() {
		GUIStyle style = new GUIStyle();
		style.font = this.font;
		GUILayout.Label("<size=20>Startender!</size>");
	    GUILayout.Space(10.0f);
    }

    public void setDrinkCompletionTime(float value)
    {
        //Setter for the drinkCompletionTime variable
        drinkCompletionTime = value;

        Debug.Log("Set the drink completion time to " + value);
    }
}

