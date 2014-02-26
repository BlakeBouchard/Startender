using System.Collections;
using System.Collections.Generic;

public class PlayerState
{
	//persistent game stats
	private int starBucks;
	private int rest;
	private float gpa;

	//base level difficulty
	private int difficulty;

	//persistent game costs
	private int baseRent;
	private int baseGroceries;
	private int baseTuition;

	//round specific stats
	private int drinksServed;
	private int tipsEarned;

<<<<<<< HEAD
	private Dictionary<string,int> roundCosts;
=======
    //Added by Rebeca. Last tip amount earned.
    private int lastTip;
>>>>>>> master

	public PlayerState() {
		//base stats
		this.difficulty = 1;
		this.starBucks = 40;
		this.tipsEarned = 0;
<<<<<<< HEAD
		this.rest = 10;
		this.gpa = 3.0f;

		//base costs
		baseRent = 10;
		baseGroceries = 5;
		baseTuition = 5;

		roundCosts = new Dictionary<string,int>();
=======
        this.lastTip = 0;
>>>>>>> master
	}

	//GAME ROUND METHODS
	public int getDrinkCount() {
		return this.drinksServed;
	}

	public void incrementDrinkCount() {
		this.drinksServed++;
	}

	public void endRound() {
		this.starBucks += this.tipsEarned;
		this.tipsEarned = 0;
		this.drinksServed = 0;
	}

	public void resetRound() {
		this.tipsEarned = 0;
	}

	public void addTip(int tipValue) {
        //First line added by Rebeca.
        this.lastTip = tipValue;
		this.tipsEarned += tipValue;
	}

    //Added by Rebeca.
    public int getLastTip() {
        return this.lastTip;
    }

	public int getTipsEarned() {
		return this.tipsEarned;
	}

	//RPG METHODS
	public Dictionary<string,int> generateRoundCosts() {
		Dictionary<string,int> roundCosts = new Dictionary<string,int>();

		roundCosts.Add("Rent", this.baseRent);
		roundCosts.Add("Groceries", this.baseGroceries);
		roundCosts.Add("Tuition", this.baseTuition);
		
		return roundCosts;
	}

	public int getStarbucks() {
		return this.starBucks;
	}

	public int getRest() {
		return this.rest;
	}

	public float getGPA() {
		return this.gpa;
	}

	public int getDifficulty() {
		return this.difficulty;
	}

	public void incrementGPA(float delta) {
		this.gpa += delta;
	}

	public void incrementRest(int delta) {
		this.rest += delta;
	}

	public void incrementDifficulty() {
		this.difficulty++;
	}
}
