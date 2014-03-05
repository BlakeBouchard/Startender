using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPGuiDrawer : MonoBehaviour
{
	public int menuXFromCenter;
	public int menuYFromCenter;
	public int menuWidth;
	public int menuHeight;

	public GameManager gameManager;

	public void setManagers(GameManager gameManager) {
		this.gameManager = gameManager;
	}

	// Use this for initialization
	void Start ()
	{
		//setup basic drawing params
		this.menuWidth = 240;
		this.menuHeight = 100;
		this.menuXFromCenter = this.menuWidth / 2;
		this.menuYFromCenter = this.menuHeight / 2;

	}

	public Dictionary<string,int> drawPaymentScreen(Dictionary<string, int> roundCosts, int starBucks) {

		Dictionary<string, int> paid = new Dictionary<string, int>();

		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));
		this.drawBaseMenu();
		GUILayout.EndArea();

		return paid;
	}

}

