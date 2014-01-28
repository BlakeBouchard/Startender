using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private enum GameState { Playing, Paused, Menu, RoundOver }
	private GameState gameState;
	private float roundTime;

	private GUIDrawer guiDrawer;
	private DrinkManager drinkManager;

	public GameManager() {

		this.drinkManager = new DrinkManager();

		this.gameState = GameState.Menu;
		this.roundTime = 90.0f;
	}

	public void startGame() {

		Debug.Log("Starting game");
		Time.timeScale = 1;
		
//		DontDestroyOnLoad(gamestate.Instance);
//		gamestate.Instance.startState();
		//      gamestate.Instance.resetRoundState();

		this.gameState = GameState.Playing;
	}

	public void pauseGame() {
		Time.timeScale = 0;
	}

	public void endRound() {
		Time.timeScale = 0;
		this.gameState = GameState.RoundOver;
	}

	public void resumeRound() {
		Time.timeScale = 1;
		this.gameState = GameState.Playing;
	}

	public void resetGame() {
		this.resetRoundTime();
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
		Time.timeScale = 0;

		//Get HUD Manager
		GameObject gui = GameObject.Find("GUIDrawer");
		this.guiDrawer = (GUIDrawer) gui.GetComponent(typeof(GUIDrawer));
		Debug.DebugBreak();
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
	
}

