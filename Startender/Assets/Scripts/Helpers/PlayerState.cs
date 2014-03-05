using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
	//persistent game stats
	private int starBucks;
	private int rest;
	private float gpa;

	//round specific stats
	private int drinksServed;
	private int tipsEarned;
	private int lastTip;

	//persistent game costs
	private int baseRent;
	private int baseGroceries;
	private int baseTuition;

	//base level difficulty
	private int difficulty;

	private Dictionary<string,int> roundCosts;

	void Start() {
        DontDestroyOnLoad(this);
		this.starBucks = 40;
		this.tipsEarned = 0;
        this.lastTip = 0;
		this.rest = 10;
		this.gpa = 3.0f;

		//base stats
		this.difficulty = 1;
		
		//base costs
		this.baseRent = 10;
		this.baseGroceries = 5;
		this.baseTuition = 5;

		this.roundCosts = new Dictionary<string,int>();
	}

	//RPG METHODS
	public Dictionary<string,int> GenerateRoundCosts() {
		Dictionary<string,int> roundCosts = new Dictionary<string,int>();
		
		roundCosts.Add("Rent", this.baseRent);
		roundCosts.Add("Groceries", this.baseGroceries);
		roundCosts.Add("Tuition", this.baseTuition);
		
		return roundCosts;
	}
	
	public int GetStarbucks() {
		return this.starBucks;
	}
	
	public int GetRest() {
		return this.rest;
	}
	
	public float GetGPA() {
		return this.gpa;
	}
	
	public int GetDifficulty() {
		return this.difficulty;
	}
	
	public void IncrementGPA(float delta) {
		this.gpa += delta;
	}
	
	public void IncrementRest(int delta) {
		this.rest += delta;
	}
	
	public void IncrementDifficulty() {
		this.difficulty++;
	}

	public int GetDrinkCount() {
		return this.drinksServed;
	}

	public void IncrementDrinkCount() {
		this.drinksServed++;
	}

	public void EndRound() {
		this.starBucks += this.tipsEarned;
		this.tipsEarned = 0;
		this.drinksServed = 0;
	}

	public void ResetRound() {
		this.tipsEarned = 0;
	}

	public void AddTip(int tipValue) {
        //First line added by Rebeca.
        this.lastTip = tipValue;
		this.tipsEarned += tipValue;
	}

    public int GetLastTip() {
        return this.lastTip;
    }

	public int GetTipsEarned() {
		return this.tipsEarned;
	}
	
	public int GetStarBucks() {
		return this.starBucks;
	}
}
