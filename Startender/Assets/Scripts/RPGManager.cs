using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPGManager : MonoBehaviour
{
	private PlayerState player;
	private RPGuiDrawer gui;
	private Dictionary<string, int> roundCosts;

	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<PlayerState>();
		gui = GameObject.FindObjectOfType<RPGuiDrawer>();
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

	void OnGUI() {
		this.roundCosts = this.gui.DrawPaymentScreen(player.GetStarbucks());
	}

}

