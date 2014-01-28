using UnityEngine;
using System.Collections;

public class Drink
{
	private Ingredient[] ingredients;
	private string drinkName;
	private float difficulty;
	private Color drinkColor;

	public Drink(string name, float difficulty, Ingredient[] ingredients, Color color) {
		this.ingredients = ingredients;
		this.drinkName = name;
		this.difficulty = difficulty;
		this.drinkColor = color;
	}

	public string getFormattedIngredients() {

		string formatted = "";

		for(int x = 0; x < this.ingredients.Length; x++) {
			formatted += ingredients[x].getName();

			if(x + 1 < this.ingredients.Length) {
				formatted += ", ";
			}
		}

		return formatted;

	}

	public Ingredient[] getIngredients() {
		return this.ingredients;
	}

	public int getIngredientCount() {
		return this.ingredients.Length;
	}

	public string getDrinkName() {
		return this.drinkName;
	}

	public float getDifficulty() {
		return this.difficulty;
	}

	public Color getDrinkColor() {
		return this.drinkColor;
	}
}

