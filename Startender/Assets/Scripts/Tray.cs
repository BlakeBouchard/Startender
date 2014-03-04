using UnityEngine;
using System.Collections;

public class Tray : MonoBehaviour
{
    private DrinkManager drinkManager;
    private PlayerState player;

	// Use this for initialization
	void Start ()
	{
        this.drinkManager = GameObject.Find("Drink Manager").GetComponent<DrinkManager>();
        this.player = GameObject.Find("Player").GetComponent<PlayerState>();
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Ingredient Collider")
        {

            CupIngredients cupIngredients = collider.GetComponent<CupIngredients>();
            if (cupIngredients.GetIngredientCount() > 0)
            {
                int tip = drinkManager.FinishAndTip(cupIngredients.GetIngredients());
                cupIngredients.ResetIngredients();
                player.AddTip(tip);
                player.IncrementDrinkCount();
                audio.Play();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    /*
        // This should not be necessary now that the tray works
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnTriggerEnter2D(GameObject.Find("Ingredient Collider").collider2D);
        }
        */
	}

}

