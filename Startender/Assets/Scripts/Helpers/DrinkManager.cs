using System;
using System.Collections.Generic;
using UnityEngine;

public class DrinkManager : MonoBehaviour {
	
	// need to call populate drinks list before anything
	public List<Drink> drinkList;
	public Drink currentDrink;

    //Added by Rebeca. The previous drink name, saved for feedback.
    private String prevDrinkName = "";

	private int maxTip = 20;

    void Start()
    {
        this.drinkList = CreateDrinkList();
    }

    //Added by Rebeca.
    public String GetPrevDrinkName()
    {
        return this.prevDrinkName;
    }

	public Drink GetCurrentDrink()
    {
		if (this.currentDrink == null)
        {
			this.SetNextDrink();
		}

		return this.currentDrink;
	}

    public List<Drink> GetDrinks()
    {
        return this.drinkList;
    }

    public List<Drink> CreateDrinkList()
    {
        LevelSettings levelSettings = GameObject.Find("Level Settings").GetComponent<LevelSettings>();
        List<Transform> drinkObjects;

        if (levelSettings != null)
        {
            drinkObjects = levelSettings.GetDrinkDefinitions();
        }
        else
        {
            drinkObjects = new List<Transform>(this.GetComponentsInChildren<Transform>());
        }
        
        List<Drink> drinkList = new List<Drink>();

        foreach (Transform drinkObject in drinkObjects)
        {
            drinkObject.transform.parent = this.transform;
            Drink drink = drinkObject.GetComponent<Drink>();
            drinkList.Add(drink);
            Debug.Log(drink.name + ": " + drink.GetFormattedIngredients());
        }
        return drinkList;
    }

	// selects random drink from the drink array list
	public void SetNextDrink() 
    {
        //Added by Rebeca.
        if (this.currentDrink == null)
        {
            this.prevDrinkName = "";
        }
        else
        {
            this.prevDrinkName = this.currentDrink.name;
        }

		int randomNumber = GetRandomNumber(drinkList.Count);
		this.currentDrink = this.drinkList[randomNumber];
	}
	
	// picks random number between 0 and the integer given (includes 0 but not max)
	public int GetRandomNumber(int max)
    {
		System.Random random = new System.Random();
		int randomNumber = random.Next(max);

		//return a 0 inclusive number
		return randomNumber;
	}

	public int FinishAndTip(List<Ingredient> ingredients)
    {
        //Added by Rebeca. Used to fade out the drink feedback GUI.
        GameObject.Find("GUIDrawer").SendMessage("setDrinkCompletionTime", Time.time);

        return this.GetTipAmount(this.MadeSuccessfully(ingredients));
	}
	
	// figures out tip amount
	// need to cut off after 2 decimal places
	// maybe pick random number between -3/3? then add that to the total?
	// making sure it is over 0!
	public int GetTipAmount(bool drinkSuccess)
    {
        if (!drinkSuccess)
        {
            return -1;
        }

        System.Random rand = new System.Random();
        int tip = (int) Math.Round(this.currentDrink.GetDifficulty() * rand.Next(this.maxTip));

        SetNextDrink();

        return tip;
	}

    public bool MadeSuccessfully(List<Ingredient> ingredientsInCup)
    {
        return MadeSuccessfully(ingredientsInCup, false);
    }

    //Checks if the drink was made successfully or not. Returns true if it was, false if it wasn't.
    //If orderMatters is true, the ingredients must be added to the cup in a specific order.
	public bool MadeSuccessfully(List<Ingredient> ingredientsInCup, Boolean orderMatters)
    {
        if (this.currentDrink != null)
        {
            int actualIngredientCount = this.currentDrink.GetIngredientCount();

            if (ingredientsInCup.Count != actualIngredientCount)
            {
                Debug.Log("Wrong Ingredient Count!");
                return false;
            }

            Ingredient[] actualIngredients = this.currentDrink.GetIngredients();

            List<Ingredient> remainingIngredients = new List<Ingredient>();
            remainingIngredients.AddRange(ingredientsInCup);

            // Check if there is a matching drink ingredient for all actual ingredients, in order if applicable.
            for (int i = 0; i < actualIngredientCount; i++)
            {
                Boolean ingredientsMatch = true;
                if (orderMatters)
                {
                    if (actualIngredients[i].name != ingredientsInCup[i].name)
                    {
                        ingredientsMatch = false;
                    }
                }
                else
                {
                    //Assume there is no match (guilty until proven innocent).
                    ingredientsMatch = false;

                    Ingredient match = null;

                    //loop through to see if there is a match that hasn't been taken.
                    for (int j = 0; j < remainingIngredients.Count && !ingredientsMatch; j++)
                    {
                        if (remainingIngredients[j].name == actualIngredients[i].name)
                        {
                            ingredientsMatch = true;
                            match = remainingIngredients[j];
                        }
                    }

                    //If there was a match, remove it from the remaining ingredients list.
                    if (match != null)
                    {
                        remainingIngredients.Remove(match);
                    }
                }

                //There was failure somewhere, ingredients didn't match what they should have been.
                if (!ingredientsMatch)
                {
                    Debug.Log("Missing Drink Ingredient: " + actualIngredients[i].name);
                    return false;
                }

            }
            Debug.Log(currentDrink.name + " made successfully!");
            return true;
        }

        return false;
	}
}
