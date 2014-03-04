using System.Collections;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
	//persistent game stats
	private int starBucks;

	//round specific stats
	private int drinksServed;
	private int tipsEarned;

    //Added by Rebeca. Last tip amount earned.
    private int lastTip;

	void Start() {
        DontDestroyOnLoad(this);
		this.starBucks = 40;
		this.tipsEarned = 0;
        this.lastTip = 0;
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

    //Added by Rebeca.
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
