using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Paused, Menu, RoundOver }
    public GameState gameState;
    
    public float maxRoundTime = 90.0f;
    private float roundTime;

    private PlayerState player;

    private GUIDrawer guiDrawer;
    private DrinkManager drinkManager;

    // Use this for initialization
    void Start()
    {
        this.drinkManager = this.GetComponentInChildren<DrinkManager>();
        this.gameState = GameState.Menu;
        this.roundTime = maxRoundTime;
        this.player = GameObject.FindObjectOfType<PlayerState>();

        Time.timeScale = 0;

        //Get HUD Manager
        GameObject gui = GameObject.Find("GUIDrawer");
        this.guiDrawer = (GUIDrawer)gui.GetComponent(typeof(GUIDrawer));
        this.guiDrawer.SetManagers(this, this.drinkManager);
    }

    public void StartGame() {

	Application.LoadLevel("mainscene");

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
	    Application.LoadLevel("rpg");
    }

    public void ResumeRound() {
	    Time.timeScale = 1;
	    this.gameState = GameState.Playing;
    }

    public void ResetRound() {
	    this.ResetRoundTime();
	    player.ResetRound();
    
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
}

