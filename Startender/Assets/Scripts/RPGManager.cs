using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPGManager : MonoBehaviour
{
	private PlayerState player;
	private RPGuiDrawer gui;

	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<PlayerState>();
		gui = GameObject.FindObjectOfType<RPGuiDrawer>();
		gui.setManagers(this);
	}

	public int FoodCost() {
		return player.GetDifficulty() * player.GetBaseFood();
	}

	public int RentCost() {
		return player.GetDifficulty() * player.GetBaseRent();
	}

	public int TuitionCost() {
		return player.GetDifficulty() * player.GetBaseTuition();
	}

	public void updateBaseStats(int starBucks, bool rent, bool food, bool tuition) {
		
		player.SetStarBucks(starBucks);

		if(!rent) {
			player.IncrementFailedRentPayments();
		}
		if(!food) {
			player.IncrementHunger();
		}
		if(!tuition) {
			player.DecrementGPA();
		} else {
			player.IncrementGPA();
		}
	}

	void OnGUI() {
		this.gui.DrawPaymentScreen(player.GetStarBucks());
	}

}

