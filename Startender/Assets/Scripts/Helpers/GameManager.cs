using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private enum GameState { Playing, Paused, Menu, RoundOver }
	private GameState gameState;
	public float roundTime;

	private static PlayerState player;

	private GUIDrawer guiDrawer;
	private DrinkManager drinkManager;

	public void startGame() {

		Debug.Log("Starting game");
		Time.timeScale = 1;

		this.gameState = GameState.Playing;
	}

	public void pauseGame() {
		Time.timeScale = 0;
		this.gameState = GameState.Paused;
	}

	public void endRound() {
		Time.timeScale = 0;
		this.gameState = GameState.RoundOver;
	}

	public void resumeRound() {
		Time.timeScale = 1;
		this.gameState = GameState.Playing;
	}

	public void resetRound() {
		this.resetRoundTime();
		GameManager.getPlayer().resetRound();
	
		//restart the game
		Time.timeScale = 1;
		this.gameState = GameState.Playing;
	}

	private void resetRoundTime() {
		this.roundTime = 90.0f;
	}

	public float getRoundTime() {
		return this.roundTime;
	}
	
	// Use this for initialization
	void Start()
	{
        this.drinkManager = this.GetComponentInChildren<DrinkManager>();
        this.gameState = GameState.Menu;
        this.roundTime = 90.0f;

		Time.timeScale = 0;

		//Get HUD Manager
		GameObject gui = GameObject.Find("GUIDrawer");
		this.guiDrawer = (GUIDrawer) gui.GetComponent(typeof(GUIDrawer));
		this.guiDrawer.setManagers(this, this.drinkManager);
	
	}

	// Update is called once per frame
	void Update()
	{
		if(this.gameState == GameState.Playing) {
			this.roundTime -= Time.deltaTime;

			if(this.roundTime <= 0.0f) {
				this.endRound ();
			}

			if(Input.GetKeyDown(KeyCode.Escape)) {
				this.pauseGame();
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

	public static PlayerState getPlayer() {
		if(GameManager.player == null) {
			GameManager.player = new PlayerState();
		}

		return GameManager.player;
	}
	
	public static void destroyPlayer() {
		GameManager.player = null;
	}
	
}

