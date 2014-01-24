using UnityEngine;
using System.Collections;

public class Drink : MonoBehaviour
{
	private Ingredient[] ingredients;
	private string name;
	private float difficulty;
	private Color drinkColor;

	public Drink(string name, float difficulty, Ingredient[] ingredients, Color color)
	{
		this.ingredients = ingredients;
		this.name = name;
		this.difficulty = difficulty;
		this.drinkColor = color;
	}

	public Ingredient[] getIngredients() {
		return this.ingredients;
	}

	public int getIngredientCount() {
		return this.ingredients.GetLength;
	}

	public string getName() {
		return this.name;
	}

	public float getDifficulty() {
		return this.difficulty;
	}

	public Color getDrinkColor() {
		return this.drinkColor;
	}
}

