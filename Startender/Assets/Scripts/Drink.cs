using UnityEngine;
using System.Collections;

public class Drink
{
	private Ingredient[] ingredients;
	private string drinkName;
	private float difficulty;
	private Color drinkColor;

	public Drink(string name, float difficulty, Ingredient[] ingredients, Color color)
	{
		this.ingredients = ingredients;
		this.drinkName = name;
		this.difficulty = difficulty;
		this.drinkColor = color;

		Debug.Log ("Drink Created!");
		Debug.Log(this);
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

