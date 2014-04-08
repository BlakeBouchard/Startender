using UnityEngine;
using System.Collections;

public class PaymentGuiDrawer : MonoBehaviour
{
    private PlayerState player;
    public int menuXFromCenter = 150;
    public int menuYFromCenter = 0;
    public float menuWidth = 300f;
    public float menuHeight = 500f;

    private int drinksServed;
    private int tipsEarned;
    private int newStarBucks;
    private int rent;

    private string[] messages = {"You've managed to rack up a bit of debt. You're going to have to work harder, which means your grades will suffer.", "Your grades aren't that great, but at least your rent is all payed up.", "You're going okay in school, but could be better. You've payed all your rent.", "You're going great in school, and your rent is all caught up.", "You're getting your rent in on time and you're doing fantastically in school. You feel great about life."};

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
            //Check for graduation
            if (player.daysLeft <= 0)
            {
                Application.LoadLevel("graduation");
            }

            rent = 0;
            if (player.daysLeft % 7 == 0)
            {
                rent = 50;
            }

            // Get last round stats and save them as local variables for the purpose of the GUI
            drinksServed = player.GetDrinkCount();
            tipsEarned = player.GetTipsEarned();
            newStarBucks = player.GetStarBucks() + tipsEarned - rent;

            player.gpa += Random.Range(-10, 11) * 0.01f;

            if (player.GetStarBucks() < 0)
            {
                player.gpa -= Random.Range(0, 50) * 0.01f;
            }

            if (player.gpa > 4.0)
                player.gpa = 4.0f;

            if (player.gpa < 0)
                player.gpa = 0f;

            // This adds the tips to the total StarBucks and resets the round stats
            player.starBucks += player.tipsEarned - rent;
            player.tipsEarned = 0;
            player.drinksServed = 0;
            player.daysLeft--;
        }
	}

	void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /4 - this.menuYFromCenter, menuWidth, menuHeight));

        if (player)
        {
			GUIStyle style = new GUIStyle();
			style.font = this.font;
            GUILayout.Label("Drinks Served: " + drinksServed);
            GUILayout.Label("Tips: $" + tipsEarned);
            int daysUntilRent = player.daysLeft % 7 + 1;
            GUILayout.Label("Rent Due: " + (rent != 0 ? ("$" + rent) : "in " + daysUntilRent + ((daysUntilRent == 1) ? " day" : " days")));
            if (newStarBucks > 0)
            {
                GUILayout.Label("Starbucks: $" + newStarBucks);
            }
            else
            {
                GUILayout.Label("Starbucks: $0");
                GUILayout.Label("Debt: $" + (-newStarBucks));
            }

            GUILayout.Label("");
        
            GUILayout.Label("Days until graduation: " + player.daysLeft);
            GUILayout.Label("Your GPA: " + player.gpa);

            int messageNum = (newStarBucks < 0) ? 0 : (player.gpa < 1.0) ? 1 : (player.gpa < 2.0) ? 2 : (player.gpa < 3.0) ? 3 : 4;

            GUILayout.Label(messages[messageNum]);
        }

        GUILayout.EndArea();
    }

	// Update is called once per frame
	void Update ()
	{
        
	}
}

