using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour
{
	//persistent game stats
	private int starBucks;

	//round specific stats
	private int tipsEarned;

	public PlayerState() {
		this.starBucks = 40;
		this.tipsEarned = 0;
	}
	
	// Sets the instance to null when the application quits
	public void OnApplicationQuit() {
		GameManager.destroyPlayer();
	}

	public void endRound() {
		this.starBucks += this.tipsEarned;
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
