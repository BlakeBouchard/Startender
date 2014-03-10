using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
	//so we don't overwrite the player state on scene change
	public bool initialized;

	//persistent game stats
	public int starBucks;
	public int rest;
	public float gpa;
	public int hunger;

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
	
	public int hungerThreshold;
	public int failedRentPayments;
	public int failedRentThreshold;
	public float gpaThreshold;

	public string gameFailMessage;

	public float gpaIncreaseChance = 0.4f;

	void Start() {
        DontDestroyOnLoad(this);

		if(!this.initialized) {
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

			this.failedRentPayments = 0;
			this.failedRentThreshold = 3;
			this.hunger = 0;
			this.hungerThreshold = 3;
			this.gpa = 3.0f;
			this.gpaThreshold = 1.3f;
			this.initialized = true;
		}

	}

	//RPG METHODS
	public Dictionary<string,int> GenerateRoundCosts() {
		Dictionary<string,int> roundCosts = new Dictionary<string,int>();
		
		roundCosts.Add("Rent", this.baseRent);
		roundCosts.Add("Groceries", this.baseGroceries);
		roundCosts.Add("Tuition", this.baseTuition);
		
		return roundCosts;
	}

	private void EndGame() {
		Application.LoadLevel("failScene");
	}

	public int GetBaseRent() {
		return this.baseRent;
	}

	public int GetBaseFood() {
		return this.baseGroceries;
	}

	public int GetBaseTuition() {
		return this.baseTuition;
	}

	public string GetFailMessage() {
		return this.gameFailMessage;
	}
	
	public int GetRest() {
		return this.rest;
	}
	
	public float GetGPA() {
		return this.gpa;
	}

	public int GetFailedRentThreshold() {
		return this.failedRentThreshold;
	}
	
	public int GetDifficulty() {
		return this.difficulty;
	}
	
	public void IncrementGPA() {
		if(Random.value > gpaIncreaseChance) {
			this.gpa += 0.3f;
		}
	}

	public void DecrementGPA() {
		this.gpa -= 0.3f;
		if(this.gpa < this.gpaThreshold) {
			this.gameFailMessage = "You were kicked out of school for poor performance!";
			this.EndGame();
		}
	}
	
	public void IncrementRest(int delta) {
		this.rest += delta;
	}

	public void IncrementFailedRentPayments() {
		this.failedRentPayments += 1;
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

	public void IncrementStarbucks(int delta) {
		this.starBucks += delta;
	}

	public void IncrementHunger() {
		this.hunger += 1;
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
