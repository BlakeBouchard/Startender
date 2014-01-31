using UnityEngine;
using System.Collections;

public class Tray : MonoBehaviour
{
    private DrinkManager drinkManager;

	// Use this for initialization
	void Start ()
	{
        drinkManager = GameObject.Find("Drink Manager").GetComponent<DrinkManager>();
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Cup")
        {

            CupIngredients cupIngredients = collider.GetComponent<CupIngredients>();

            int tip = drinkManager.finishAndTip(cupIngredients.GetIngredients());
            cupIngredients.ResetIngredients();
            GameManager.getPlayer().addTip(tip);
            GameManager.getPlayer().incrementDrinkCount();
            audio.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    // Saying the hell with it
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnTriggerEnter2D(GameObject.Find("Cup").collider2D);
        }
	}

}

