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

	public void setManagers(RPGManager rpgManager) {
		this.rpgManager = rpgManager;
	}

	// Use this for initialization
	void Start ()
	{
		//setup basic drawing params
		this.menuWidth = 240;
		this.menuHeight = 100;
		this.menuXFromCenter = this.menuWidth / 2;
		this.menuYFromCenter = this.menuHeight / 2;

		this.rent = false;
		this.food = false;
		this.tuition = false;
	}

	public Dictionary<string,int> DrawPaymentScreen(int starBucks) {

		Dictionary<string, int> paid = new Dictionary<string, int>();

		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));
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

		GUILayout.EndArea();

		return paid;
	}

	public void DrawBaseMenu() {
		GUILayout.Space(10.0f);
	}
}

