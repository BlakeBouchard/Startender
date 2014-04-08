using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPGuiDrawer : MonoBehaviour
{
	public int menuXFromCenter;
	public int menuYFromCenter;
	public int menuWidth;
	public int menuHeight;

	public bool rent;
	public bool food;
	public bool tuition;

	public RPGManager rpgManager;

	public Font font;

	public void setManagers(RPGManager rpgManager) {
		this.rpgManager = rpgManager;
	}

	// Use this for initialization
	void Start ()
	{
		//setup basic drawing params
		this.menuWidth = 240;
		this.menuHeight = 200;
		this.menuXFromCenter = this.menuWidth / 2;
		this.menuYFromCenter = this.menuHeight / 2;

		this.rent = false;
		this.food = false;
		this.tuition = false;
	}

	public void DrawPaymentScreen(int starBucks) {

		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));
		GUIStyle style = new GUIStyle();
		style.font = this.font;

		this.DrawBaseMenu();

		int remainingStarBucks = starBucks;
		int rentCost = rpgManager.RentCost();
		int foodCost = rpgManager.FoodCost();
		int tuitionCost = rpgManager.TuitionCost();

		if(this.rent) {
			remainingStarBucks -= rentCost;
		}

		if(this.food) {
			remainingStarBucks -= foodCost;
		}

		if(this.tuition) {
			remainingStarBucks -= tuitionCost;
		}

		GUILayout.Label("Starbucks: $" + remainingStarBucks);

		this.rent = GUILayout.Toggle(this.rent, "Rent: " + rentCost);
		this.food = GUILayout.Toggle(this.food, "Food: " + foodCost);
		this.tuition = GUILayout.Toggle(this.tuition, "School: " + tuitionCost);

		if (GUILayout.Button("Pay and Continue")) {
			Debug.Log("Pay Button Clicked");
			rpgManager.UpdateBaseStats(remainingStarBucks, this.rent, this.food, this.tuition);
            Debug.Log("Game Saved");
            PlayerState player = GameObject.Find("Player").GetComponent<PlayerState>();
            player.SaveGame();
			Application.LoadLevel(2);
		}

		GUILayout.EndArea();
	}

	public void DrawBaseMenu() {
		GUILayout.Space(10.0f);
	}
}

