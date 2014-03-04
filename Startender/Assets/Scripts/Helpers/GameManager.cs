using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public enum GameState { Playing, Paused, Menu, RoundOver }
	public GameState gameState;
	public float roundTime;

	private static PlayerState player;

	private GUIDrawer guiDrawer;
	private DrinkManager drinkManager;

    // Use this for initialization
    void Start()
    {
        this.drinkManager = this.GetComponentInChildren<DrinkManager>();
        this.gameState = GameState.Menu;
        this.roundTime = 90.0f;

        Time.timeScale = 0;

        //Get HUD Manager
        GameObject gui = GameObject.Find("GUIDrawer");
        this.guiDrawer = (GUIDrawer)gui.GetComponent(typeof(GUIDrawer));
        this.guiDrawer.setManagers(this, this.drinkManager);
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	public void StartGame() {

		Debug.Log("Starting game");
		Time.timeScale = 1;

		this.gameState = GameState.Playing;
	}

	public void PauseGame() {
		Time.timeScale = 0;
		this.gameState = GameState.Paused;
	}

	public void EndRound() {
		Time.timeScale = 0;
		this.gameState = GameState.RoundOver;
	}

	public void ResumeRound() {
		Time.timeScale = 1;
		this.gameState = GameState.Playing;
	}

	public void ResetRound() {
		this.ResetRoundTime();
		GameManager.GetPlayer().ResetRound();
	
		//restart the game
		Time.timeScale = 1;
		this.gameState = GameState.Playing;
	}

	private void ResetRoundTime() {
		this.roundTime = 90.0f;
	}

	public float GetRoundTime() {
		return this.roundTime;
	}

	public GameState GetGameState() {
		return gameState;
	}

	// Update is called once per frame
	void Update()
	{
		if(this.gameState == GameState.Playing) {
			this.roundTime -= Time.deltaTime;

			if(this.roundTime <= 0.0f) {
				this.EndRound ();
			}

			if(Input.GetKeyDown(KeyCode.Escape)) {
				this.PauseGame();
			}
		}
	}
	
	void OnGUI() {
		switch(this.gameState) {
		case GameState.Playing:
			this.guiDrawer.drawHUD();
                //Added by Rebeca.
            this.guiDrawer.drawDrinkFeedback();
			return;
		case GameState.Menu:
			this.guiDrawer.drawMainMenu();
			break;
		case GameState.Paused:
			this.guiDrawer.drawPauseMenu();
			break;
		case GameState.RoundOver:
			this.guiDrawer.drawRoundStats();
			break;
		}
	}

	public static PlayerState GetPlayer() {
		if(GameManager.player == null) {
			GameManager.player = new PlayerState();
		}

		return GameManager.player;
	}
	
	public static void DestroyPlayer() {
		GameManager.player = null;
	}
	
}

