using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private enum GameState { Playing, Paused, Menu }
	private GameState gameState;

	public GameManager() {
		this.gameState = GameState.Menu;
	}

	void OnGUI() {

		if(this.gameState == GameState.Menu && GUI.Button(new Rect (30, 30, 150, 30), "Start Game")) {
			Debug.Log("Start Game button clicked");
			startGame();
		}
	}

	private void startGame() {

		print("Starting game");
		
//		DontDestroyOnLoad(gamestate.Instance);
//		gamestate.Instance.startState();
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

