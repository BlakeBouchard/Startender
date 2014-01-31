using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CupIngredients : MonoBehaviour {

    private List<Ingredient> ingredients;
    private DrinkManager drinkManager;

	// Use this for initialization
	void Start ()
    {
        this.ingredients = new List<Ingredient>();
        drinkManager = GameObject.Find("Drink Manager").GetComponent<DrinkManager>();
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger detected from: " + collider.gameObject.name);
        Bubble bubble = collider.gameObject.GetComponent<Bubble>();

        if (bubble != null)
        {
            this.ingredients.Add(bubble.getIngredient());

            Destroy(collider.gameObject);

            Debug.Log("Ingredient added: " + ingredients[0]);
        }
        else if (collider.gameObject.name == "Tray")
        {
            Debug.Log("Tray Touched");

            //TODO: figure out the most elegant way to handle this interaction

            int tip = drinkManager.finishAndTip(this.ingredients);
            GameManager.getPlayer().addTip(tip);
            GameManager.getPlayer().incrementDrinkCount();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
