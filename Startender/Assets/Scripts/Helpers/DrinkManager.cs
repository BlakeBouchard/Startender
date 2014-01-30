using System;
using System.Collections.Generic;
using UnityEngine;

public class DrinkManager : MonoBehaviour {
	
	// need to call populate drinks list before anything
	private List<Drink> drinkList;
	private Drink currentDrink;

	private int maxTip;

	public DrinkManager() {
		this.drinkList = new List<Drink>();

		//sample drink
		Ingredient[] quasar = new Ingredient[2];
		quasar[0] = new Vodka();
		quasar[1] = new SpaceClam();

		this.drinkList.Add(new Drink("Quasar", 0.3f, quasar, Color.red));

		//sets the biggest possible tip a player can get
		this.maxTip = 20;
	}

	public Drink getCurrentDrink() {
		if(this.currentDrink == null) {
			this.setNextDrink();
		}

		return this.currentDrink;
	}

	// selects random drink from the drink array list
	public void setNextDrink() {
		int randomNumber = getRandomNumber(drinkList.Count);
		this.currentDrink = this.drinkList[randomNumber];
	}
	
	// picks random number between 0 and the integer given (includes 0 but not max)
	public int getRandomNumber(int max) {
		System.Random random = new System.Random();
		int randomNumber = random.Next(max);

		//return a 0 inclusive number
		return randomNumber;
	}

	public int finishAndTip(List<Ingredient> ingredients) {

		return this.getTipAmount(this.madeSuccessfully(ingredients));

	}
	
	// figures out tip amount
	// need to cut off after 2 decimal places
	// maybe pick random number between -3/3? then add that to the total?
	// making sure it is over 0!
	public int getTipAmount(bool drinkSuccess) {
		if(!drinkSuccess){
		 	return -1;
		}

		System.Random rand = new System.Random();
		return (int) Math.Round(this.currentDrink.getDifficulty() * rand.Next(this.maxTip));
	}
	
	public bool madeSuccessfully(List<Ingredient> ingredients) {

		int actualIngredientCount = this.currentDrink.getIngredientCount();

		if (ingredients.Count != actualIngredientCount) {
			Debug.Log("Wrong Ingredient Count!");
			return false;
		}

		//check if there is a matching drink ingredient for all actual ingredients
		foreach(Ingredient actualIngredient in this.currentDrink.getIngredients()) {
			bool match = false;

			foreach(Ingredient drinkIngredient in ingredients) {
				if(typeof(drinkIngredient) == typeof(actualIngredient)) {
					match = true;
				}
			}

			//missing an ingredient type
			if(!match) {
				Debug.Log("Missing Drink Ingredient: " + actualIngredient.getName());
				return false;
			}

		}

		return true;
		
	}

	
//	// For testing
//	// name, difficulty, tip, ingredients
//	private void populateDrinksList()
//	{
//		Color red = new Color(Color.RED);
//		Color green = new Color(Color.GREEN);
//		Color blue = new Color(Color.BLUE);
//		Color yellow = new Color(Color.YELLOW);
//		Color white = new Color(Color.WHITE);
//		
//		ArrayList<String> vw = new ArrayList<String>();
//		vw.add("Vodka");
//		vw.add("Water");
//		vw.add("Lime");
//		Drink vodkaWater = new Drink("Vodka Water",0,vw,green);
//		allDrinks.add(vodkaWater);
//		
//		ArrayList<String> rc = new ArrayList<String>();
//		rc.add("Rum");
//		rc.add("Coke");
//		rc.add("Ice");
//		Drink rumCoke = new Drink("Rum & Coke",2,rc,blue);
//		allDrinks.add(rumCoke);
//		
//		ArrayList<String> mtc = new ArrayList<String>();
//		mtc.add("Gin");
//		mtc.add("Lemon Juice");
//		mtc.add("Club Soda");
//		Drink tomCollins = new Drink("Major Tom Collins",4,mtc,yellow);
//		allDrinks.add(tomCollins);
//		
//		ArrayList<String> wr = new ArrayList<String>();
//		wr.add("Vodka");
//		wr.add("Kahlua");
//		wr.add("Cream");
//		Drink whiteRussian = new Drink("White Russian Space Shuttle",7,wr,white);
//		allDrinks.add(whiteRussian);
//		
//		ArrayList<String> qr = new ArrayList<String>();
//		qr.add("Clamato");
//		qr.add("Sun Sauce");
//		qr.add("Vodka");
//		Drink quasar = new Drink("Quasar",9,qr,red);
//		allDrinks.add(quasar);
//		
//	}
	
}
