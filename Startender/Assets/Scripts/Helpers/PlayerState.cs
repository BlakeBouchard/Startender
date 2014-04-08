using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
	//persistent game stats
	public int starBucks;
	public int rest;
	public float gpa;
	public int hunger;
    public int daysLeft;

	//round specific stats
	public int drinksServed;
	public int tipsEarned;
	public int lastTip;

    public bool rentDue;

	//persistent game costs
	private int baseRent;
	private int baseGroceries;
	private int baseTuition;

    public static int TOTAL_DAYS = 28;

	//base level difficulty
	public int difficulty;
	
	public int hungerThreshold;
	public int failedRentPayments;
	public int failedRentThreshold;
	public float gpaThreshold;

	public string gameFailMessage;

	public float gpaIncreaseChance = 0.4f;

	void Start() {
        DontDestroyOnLoad(this);

		if (PlayerPrefs.GetInt ("HasBegun") == 0) {
			this.starBucks = 40;
			this.tipsEarned = 0;
	        this.lastTip = 0;
			this.rest = 10;
			this.gpa = 3.0f;
            this.daysLeft = TOTAL_DAYS;

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
		if (PlayerPrefs.GetInt ("HasBegun") == 0)
        {
            return 1;
        }
        else
        {
            return this.difficulty;
        }
	}
	
	public void IncrementGPA() {
		if (Random.value > gpaIncreaseChance) {
			this.gpa += 0.3f;
		}
	}

	public void DecrementGPA() {
		this.gpa -= 0.3f;
		if (this.gpa < this.gpaThreshold) {
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

	public void SetStarBucks(int starBucks) {
		this.starBucks = starBucks;
	}

	public void SaveGame() {
		PlayerPrefs.SetInt ("StarBucks", this.starBucks);
		PlayerPrefs.SetInt ("Rest", this.rest);
		PlayerPrefs.SetInt ("Hunger", this.hunger);
		PlayerPrefs.SetInt ("Difficulty", this.difficulty);
		PlayerPrefs.SetFloat ("GPA", this.gpa);
        PlayerPrefs.SetInt("DaysLeft", this.daysLeft);
	}
	
	public void LoadGame() {
		this.starBucks = PlayerPrefs.GetInt ("StarBucks");
		this.rest = PlayerPrefs.GetInt ("Rest");
		this.hunger = PlayerPrefs.GetInt ("Hunger");
		this.difficulty = PlayerPrefs.GetInt ("Difficulty");
		this.gpa = PlayerPrefs.GetFloat ("GPA");
        this.daysLeft = PlayerPrefs.GetInt("DaysLeft");
	}

	public void ResetGame() {
		PlayerPrefs.SetInt ("StarBucks", 40);
		PlayerPrefs.SetInt ("Rest", 10);
		PlayerPrefs.SetInt ("Hunger", 0);
		PlayerPrefs.SetInt ("Difficulty", 1);
		PlayerPrefs.SetInt ("HasBegun", 0);
		PlayerPrefs.SetFloat ("GPA", 3.0f);
        PlayerPrefs.SetInt("DaysLeft", TOTAL_DAYS);
	}

	public void ClearPrefs() {
		PlayerPrefs.DeleteAll ();
	}
}
