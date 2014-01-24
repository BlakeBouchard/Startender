// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;

public class DrinkManager {
	
	// need to call populate drinks list before anything
	private ArrayList<Drink> drinkList;

	public DrinkManager() {
		this.drinkList =  = new ArrayList<Drink>();
	}
	
	// selects random drink from the drink array list
	public Drink randomDrink()
	{
		int randomNumber = getRandomNumber(allDrinks.size());
		Drink drink = allDrinks.get(randomNumber);
		return drink;
	}
	
	// picks random number between 0 and the integer given (includes 0 but not max)
	public int getRandomNumber(int max)
	{
		Random random = new Random();
		int randomNumber = random.nextInt(max);
		return randomNumber;
	}
	
	// figures out tip amount
	// need to cut off after 2 decimal places
	// maybe pick random number between -3/3? then add that to the total?
	// making sure it is over 0!
	public float getTipAmount(Drink recipe, Drink inCup)
	{
		
		float finalTip = 0;
		float correctIngredients = numberCorrectIngredients(recipe, inCup);
		
		if(correctIngredients == 0)
		{
			finalTip = 1;
		}
		else
		{
			float multiplier = correctIngredients / recipe.ingredients.size();
			finalTip = recipe.tipBase * multiplier;
		}
		
		return finalTip;
	}
	
	float roundTip(Float tip)
	{
		BigDecimal roundedTip = new BigDecimal(tip);
		BigDecimal cutted = roundedTip.setScale(2, BigDecimal.ROUND_DOWN);
		tip = cutted.floatValue();
		return tip;
	}
	
	public boolean madeSuccessfully(Drink inIngredients, Drink inCup)
	{
		// if the number of correct ingredients equals number of ingredients in the recipe AND
		// if there are the same amount in each drink
		if(numberCorrectIngredients(inIngredients, inCup) == inIngredients.ingredients.size() &&
		   inIngredients.ingredients.size() == inCup.ingredients.size())
		{
			return true;
		}
		else
		{
			return false;
		}
		
	}
	
	// this will take in the drink object and the object in the cup
	// will compare ingredients and output total number of correct ingredients
	public int numberCorrectIngredients(Drink inIngredients, Drink inCup)
	{
		int correctIngredients = 0;
		
		for (int i = 0; i < inIngredients.ingredients.size(); i++)
		{
			for (int j = 0; j < inCup.ingredients.size(); j++)
			{
				if (inIngredients.ingredients.get(i).equals(inCup.ingredients.get(j)))
				{
					correctIngredients++;
				}
			}
		}
		
		return correctIngredients;
	}
	
	// For testing
	// name, difficulty, tip, ingredients
	private void populateDrinksList()
	{
		Color red = new Color(Color.RED);
		Color green = new Color(Color.GREEN);
		Color blue = new Color(Color.BLUE);
		Color yellow = new Color(Color.YELLOW);
		Color white = new Color(Color.WHITE);
		
		ArrayList<String> vw = new ArrayList<String>();
		vw.add("Vodka");
		vw.add("Water");
		vw.add("Lime");
		Drink vodkaWater = new Drink("Vodka Water",0,vw,green);
		allDrinks.add(vodkaWater);
		
		ArrayList<String> rc = new ArrayList<String>();
		rc.add("Rum");
		rc.add("Coke");
		rc.add("Ice");
		Drink rumCoke = new Drink("Rum & Coke",2,rc,blue);
		allDrinks.add(rumCoke);
		
		ArrayList<String> mtc = new ArrayList<String>();
		mtc.add("Gin");
		mtc.add("Lemon Juice");
		mtc.add("Club Soda");
		Drink tomCollins = new Drink("Major Tom Collins",4,mtc,yellow);
		allDrinks.add(tomCollins);
		
		ArrayList<String> wr = new ArrayList<String>();
		wr.add("Vodka");
		wr.add("Kahlua");
		wr.add("Cream");
		Drink whiteRussian = new Drink("White Russian Space Shuttle",7,wr,white);
		allDrinks.add(whiteRussian);
		
		ArrayList<String> qr = new ArrayList<String>();
		qr.add("Clamato");
		qr.add("Sun Sauce");
		qr.add("Vodka");
		Drink quasar = new Drink("Quasar",9,qr,red);
		allDrinks.add(quasar);
		
	}
	
}
