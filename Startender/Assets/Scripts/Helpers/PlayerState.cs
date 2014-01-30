using System.Collections;

public class PlayerState
{
	//persistent game stats
	private int starBucks;

	//round specific stats
	private int drinksServed;
	private int tipsEarned;

	public PlayerState() {
		this.starBucks = 40;
		this.tipsEarned = 0;
	}

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
		this.tipsEarned += tipValue;
	}

	public int getTipsEarned() {
		return this.tipsEarned;
	}
	
	public int getStarbucks() {
		return this.starBucks;
	}
}
