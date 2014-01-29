using System;
using UnityEngine;

public class Ingredient
{
	private string name;
	private bool liquid;
	private Color drinkColor;
	
	public Ingredient(string name, bool liquid, Color color){
		this.name = name;
		this.liquid = liquid;
		this.drinkColor = color;
	}

	public string getName() {
		return this.name;
	}

	public bool isLiquid() {
		return this.liquid;
	}

	public Color getDrinkColor() {
		return this.drinkColor;
	}
}
