using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Paused, Menu, RoundOver }
    public GameState gameState;
    
    public float maxRoundTime = 90.0f;
    private float roundTime;

    public Transform playerPrefab;
    private PlayerState player;

    private GUIDrawer guiDrawer;
    private DrinkManager drinkManager;

    // Use this for initialization
    void Start()
    {

        this.drinkManager = this.GetComponentInChildren<DrinkManager>();
        this.gameState = GameState.Menu;
        this.roundTime = maxRoundTime;
        this.player = SpawnPlayer();

        Time.timeScale = 0;

        //Get HUD Manager
        GameObject gui = GameObject.Find("GUIDrawer");
        this.guiDrawer = (GUIDrawer)gui.GetComponent(typeof(GUIDrawer));
        this.guiDrawer.SetManagers(this, this.drinkManager, this.player);
    }

    private PlayerState SpawnPlayer()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (!playerObject)
        {
            Debug.Log("No player object found, creating one");
            Transform playerTransform = Instantiate(playerPrefab) as Transform;
            playerTransform.name = playerPrefab.name;
            playerObject = playerTransform.gameObject;
        }

        return playerObject.GetComponent<PlayerState>();
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
	    //this.gameState = GameState.RoundOver;
		Application.LoadLevel("endOfRound");
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
	    this.gameState = GameState.Menu;
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
				this.guiDrawer.DrawHUD();
				this.guiDrawer.DrawDrinkFeedback();
				return;
		    case GameState.Menu:
				this.guiDrawer.DrawMainMenu();
				break;
		    case GameState.Paused:
				this.guiDrawer.DrawPauseMenu();
				break;
		}
    }
}

