using UnityEngine;
using System.Collections;

public class PaymentGuiDrawer : MonoBehaviour
{
    private PlayerState player;
    public int menuXFromCenter = 0;
    public int menuYFromCenter = 0;
    public float menuWidth = 1;
    public float menuHeight = 1;

    private int drinksServed;
    private int tipsEarned;
    private int newStarBucks;

	public Font font;

	// Use this for initialization
	void Start ()
	{
        player = FindObjectOfType<PlayerState>();
        if (!player)
        {
            Debug.Log("Oh no! Couldn't find player!");
        }
        else
        {
            // Get last round stats and save them as local variables for the purpose of the GUI
            drinksServed = player.GetDrinkCount();
            tipsEarned = player.GetTipsEarned();
            newStarBucks = player.GetStarBucks() + tipsEarned;

            // This adds the tips to the total StarBucks and resets the round stats
            player.EndRound();
        }
	}

	void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));

        if (player)
        {
			GUIStyle style = new GUIStyle();
			style.font = this.font;
            GUILayout.Label("Drinks Served: " + drinksServed);
            GUILayout.Label("Tips: $" + tipsEarned);
            GUILayout.Label("Starbucks: $" + newStarBucks);
        }
		
		GUILayout.EndArea();
	}

	// Update is called once per frame
	void Update ()
	{
        
	}
}

