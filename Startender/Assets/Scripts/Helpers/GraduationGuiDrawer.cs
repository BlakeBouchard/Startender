using UnityEngine;
using System.Collections;

public class GraduationGuiDrawer : MonoBehaviour
{
    private PlayerState player;
    public int menuXFromCenter = 150;
    public int menuYFromCenter = 0;
    public float menuWidth = 300f;
    public float menuHeight = 500f;

    private int finalStarBucks;

    private string[] messages = { "You didn't do so well. Not only do you have debt to work off, but you got low marks in school.", "You've ended off with some debt to work off, but at least you did well in school.", "You didn't do amazingly in school, but you're glad you're done.", "You did okay in school, and you're happy you've finished.", "Your marks are pretty high and your're glad you finished!", "You did amazingly in school, and you're extatic that you've finished on such a high note."};

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

            finalStarBucks = player.starBucks;

            // Reset the player for the next game.
            player.starBucks = 0;
            player.tipsEarned = 0;
            player.drinksServed = 0;
            player.daysLeft = PlayerState.TOTAL_DAYS;
        }
	}

	void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /4 - this.menuYFromCenter, menuWidth, menuHeight));

        if (player)
        {
			GUIStyle style = new GUIStyle();
			style.font = this.font;
            GUILayout.Label("Congratulations! You have managed to work your way through school and graduate!");
            GUILayout.Label("Your final GPA: " + player.gpa);
            if(finalStarBucks < 0)
            {
                GUILayout.Label("Your Final Starbucks: $" + finalStarBucks);
            }
            else
            {
                GUILayout.Label("Your Final Starbucks: $0");
                GUILayout.Label("Debt: $" + (-finalStarBucks));
            }

            GUILayout.Label("");
        
            int messageNum = (finalStarBucks < 0) ? (player.gpa < 2.0 ? 0 : 1) : (player.gpa < 1.0) ? 2 : (player.gpa < 2.0) ? 3 : (player.gpa < 3.0) ? 4 : 5;

            GUILayout.Label(messages[messageNum]);
        }

        GUILayout.EndArea();
    }

	// Update is called once per frame
	void Update ()
	{
        
	}
}

