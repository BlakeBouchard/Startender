using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private enum GameState { Playing, Paused, Menu }
	private GameState gameState;
	private GUIDrawer guiDrawer;
	private float roundTime;

	public GameManager() {
		this.gameState = GameState.Menu;
		this.roundTime = 90.0f;
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
		}

	}

	public void startGame() {

		print("Starting game");

		Time.timeScale = 1;
		
//		DontDestroyOnLoad(gamestate.Instance);
//		gamestate.Instance.startState();
		//      gamestate.Instance.resetRoundState();

		this.gameState = GameState.Playing;
	}

	public void pauseGame() {
		Time.timeScale = 0;
		this.gameState = GameState.Paused;
	}

	public void resumeGame() {
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
	void Start ()
	{
		GameObject gui = GameObject.Find("GUIDrawer");
		this.guiDrawer = (GUIDrawer) gui.GetComponent(typeof(GUIDrawer));
		this.guiDrawer.setGameManager(this);

		Time.timeScale = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		if(this.gameState != GameState.Paused) {
			this.roundTime -= Time.deltaTime;
		}

		if(Input.GetKeyDown(KeyCode.Escape)) {
			this.pauseGame();
		}
	}


}

