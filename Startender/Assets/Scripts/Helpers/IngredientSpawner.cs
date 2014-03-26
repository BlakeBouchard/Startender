using UnityEngine;
using System.Collections;

public class IngredientSpawner : MonoBehaviour {

    // The list of ingredients that need to exist in the scene
    // based on the drinks attached to the Drink Manager
    Ingredient[] ingredients;

    // The list of all BubbleButtons
    BubbleButton[] bubbleButtons;

	// Use this for initialization
	void Start()
    {
        // Need this to determine what inredients buttons are needed
        DrinkManager drinkManager = GetComponent<DrinkManager>();
        ingredients = PopulateIngredientsList(drinkManager);
	}

    // Go through all of the Drinks attached to the Drink Manager
    // and return a list of all ingredients required
    Ingredient[] PopulateIngredientsList(DrinkManager drinkManager)
    {
        Ingredient[] allIngredients = drinkManager.GetComponentsInChildren<Ingredient>();
        //ArrayList<Ingredient> uniqueIngredients = new ArrayList<Ingredient>();

        foreach (Ingredient ingredient in allIngredients)
        {
            // foreach (Ingredient uniqueIngredient in uniqueIngredients)
            {

            }
        }

        return allIngredients;
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}
}
