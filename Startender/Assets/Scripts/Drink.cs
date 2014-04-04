using UnityEngine;
using System.Collections;

public class Drink : MonoBehaviour
{
	public Ingredient[] ingredients;
	public float difficulty;
	public Color drinkColor;

    void Start()
    {
        ingredients = GetComponentsInChildren<Ingredient>();
		Debug.Log (this.name + " Drink::ingredients: " + ingredients.Length);
    }

	public string GetFormattedIngredients()
    {
		string formatted = "";

		for (int x = 0; x < this.ingredients.Length; x++)
        {
			formatted += ingredients[x].name;

			if (x + 1 < this.ingredients.Length)
            {
				formatted += ", ";
			}
		}

		return formatted;
	}

	public Ingredient[] GetIngredients()
    {
		return this.ingredients;
	}

	public int GetIngredientCount()
    {
		return this.ingredients.Length;
	}

    public string GetDrinkName()
    {
        return this.name;
    }

    public float GetDifficulty()
    {
        return this.difficulty;
    }

}

