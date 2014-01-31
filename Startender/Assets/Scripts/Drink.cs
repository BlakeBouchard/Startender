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
    }

	public string getFormattedIngredients()
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

	public Ingredient[] getIngredients()
    {
		return this.ingredients;
	}

	public int getIngredientCount()
    {
		return this.ingredients.Length;
	}

    public string getDrinkName()
    {
        return name;
    }

    public float getDifficulty()
    {
        return this.difficulty;
    }

}

