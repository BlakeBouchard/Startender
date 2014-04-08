using UnityEngine;
using System.Collections;

public class FailSceneManager : MonoBehaviour {

	private PlayerState player;

	public int menuXFromCenter;
	public int menuYFromCenter;
	public int menuWidth;
	public int menuHeight;

	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<PlayerState>();

		//setup basic drawing params
		this.menuWidth = 240;
		this.menuHeight = 200;
		this.menuXFromCenter = this.menuWidth / 2;
		this.menuYFromCenter = this.menuHeight / 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));

		GUILayout.Label(player.GetFailMessage());
		if(GUILayout.Button("<size=30>Back To Main Menu</size>")) {
			Debug.Log("Loading main menu");
			Application.LoadLevel("titleScreen");
		}

		GUILayout.EndArea();
	}
}
