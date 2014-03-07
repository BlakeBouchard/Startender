using UnityEngine;
using System.Collections;

public class PaymentGuiDrawer : MonoBehaviour
{
    private PlayerState player;
    public int menuXFromCenter = 0;
    public int menuYFromCenter = 0;
    public float menuWidth = 1;
    public float menuHeight = 1;

	// Use this for initialization
	void Start ()
	{
        player = FindObjectOfType<PlayerState>();
        if (!player)
        {
            Debug.Log("Oh no! Couldn't find player!");
        }
	}

	void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width / 2 - this.menuXFromCenter, Screen.height /2 - this.menuYFromCenter, this.menuWidth, this.menuHeight));

        if (player)
        {
            GUILayout.Label("Drinks Served: " + player.GetDrinkCount());
            GUILayout.Label("Tips: $" + player.GetTipsEarned());
            GUILayout.Label("Starbucks: $" + player.GetStarBucks());
        }
		
		GUILayout.EndArea();
	}

	// Update is called once per frame
	void Update ()
	{

	}
}

