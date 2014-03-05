using UnityEngine;
using System.Collections;

public class PaymentGuiDrawer : GUIDrawer
{

	// Use this for initialization
	public override void Start ()
	{
		base.Start();
	}

	public override void FindTextLabels() {
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));
		
		GUILayout.Label("Drinks Served: " + player.GetDrinkCount());
		GUILayout.Label("Tips: $" + player.GetTipsEarned());
		GUILayout.Label("Starbucks: $" + player.GetStarBucks());
		
		if(GUILayout.Button("Next Round")) {
			Debug.Log ("Resetting Round");
			Application.LoadLevel("rpg");
		}
		
		GUILayout.EndArea();
	}

	// Update is called once per frame
	void Update ()
	{

	}
}

