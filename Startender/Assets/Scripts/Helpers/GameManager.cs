using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private enum GameState { Playing, Paused, Menu }
	private GameState gameState;
	private GUIDrawer guiDrawer;

	//camera pausing vars
	private int cullingMask;
	private CameraClearFlags clearFlags;

	public GameManager() {
		this.gameState = GameState.Menu;
		this.guiDrawer = GetComponent<GUIDrawer>();
		this.guiDrawer.setGameManager(this);

		this.pauseGame();
	}

	void OnGUI() {

		switch(this.gameState) {
			case GameState.Playing:
				return;
			case GameState.Menu:
				this.guiDrawer.drawMenu();
				break;
		}

	}

	public void startGame() {

		print("Starting game");
		
//		DontDestroyOnLoad(gamestate.Instance);
//		gamestate.Instance.startState();

		this.gameState = GameState.Playing;
	}

	public void pauseGame() {
		this.cullingMask = this.camera.cullingMask;
		this.clearFlags = this.camera.clearFlags;

		//Pauses/Stops the game camera
		this.camera.clearFlags = CameraClearFlags.Nothing;
		this.camera.cullingMask = 1 << 0;

		this.gameState = GameState.Paused;
	}

	public void resumeGame() {

		this.camera.cullingMask = this.cullingMask;
		this.camera.clearFlags = this.clearFlags;

		this.gameState = GameState.Playing;
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}
}

