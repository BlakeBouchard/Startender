using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelSettings : MonoBehaviour {

    // This should contain all available ingredient buttons
    public Transform[] allBubbles;
    public Transform[] allGarnishSilos;
    
    // This should point to a file in the Text directory called "AllDrinks.txt" in the Inspector
    public TextAsset allDrinks;

    // This should point to a prefab with the Drink script attached to it
    public Transform blankDrinkPrefab;
    public Transform blankBubbleButtonPrefab;
    
    Dictionary<string, List<string>> drinkList;

    // This Dictionary stores each bubble with the key being the name of the ingredient attached to it
    Dictionary<string, Transform> bubbleList;
    Dictionary<string, Transform> garnishList;
    
    // This value controls how far apart the buttons should be and how much bigger than their bubbles they should be
    public float buttonSpacing = 1;
    public float buttonScale = 2;

    private int playerDifficulty;

	// Use this for initialization
	void Start()
    {
        PlayerState player = GameObject.Find("Player").GetComponent<PlayerState>();
        this.playerDifficulty = player.GetDifficulty();

        this.drinkList = PopulateDrinkList();
        this.bubbleList = PopulateBubbleList();
        this.garnishList = PopulateGarnishList();
	}

    public List<Transform> GetDrinkDefinitions()
    {
        List<string> selectedIngredients = GenerateRandomBubbleButtons();
        return GenerateRandomDrinksFromIngredients(selectedIngredients);
    }

    private Dictionary<string, List<string>> PopulateDrinkList()
    {
        Dictionary<string, List<string>> tempDrinkList = new Dictionary<string, List<string>>();

        // Get an array consisting of each non-empty string, delimited by newlines
        string[] drinkLines = this.allDrinks.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < drinkLines.Length; i++)
        {
            // Check if line is a comment first
            if (drinkLines[i].StartsWith("#"))
            {
                // This line is a comment, go to next iteration (line)
                continue;
            }

            // Check to make sure the line contains a colon
            if (!drinkLines[i].Contains(":"))
            {
                // Every line that is not a blank line should contain a colon
                Debug.Log("WARNING! Malformed line at [" + i + "], does not contain a colon (':')");
                continue;
            }

            /** 
             * Divide line into two strings:
             * 
             * 1. Drink name
             * 2. Comma-delimited ingredients
             */
            string[] nameAndIngredients = drinkLines[i].Split(':');
            if (nameAndIngredients.Length != 2)
            {
                Debug.Log("WARNING! Malformed line at [" + i + "], should only have one colon");
                continue;
            }

            string drinkName = nameAndIngredients[0].Trim();
            string[] untrimmedIngredients = nameAndIngredients[1].Split(',');
            List<string> drinkIngredients = new List<string>();

            // Remove leading and trailing whitespace from each ingredient definition
            foreach (string untrimmedIngredient in untrimmedIngredients)
            {
                drinkIngredients.Add(untrimmedIngredient.Trim());
            }

            Debug.Log("Successfully added line " + i + ", " + drinkName);
            tempDrinkList.Add(drinkName, drinkIngredients);
        }

        return tempDrinkList;
    }

    private Dictionary<string, Transform> PopulateBubbleList()
    {
        Dictionary<string, Transform> tempBubbleList = new Dictionary<string, Transform>();
        
        foreach (Transform bubble in this.allBubbles)
        {
            Transform ingredient = bubble.GetComponent<Bubble>().GetIngredientPrefab();
            tempBubbleList.Add(ingredient.name, bubble);
        }

        return tempBubbleList;
    }

    private Dictionary<string, Transform> PopulateGarnishList()
    {
        Dictionary<string, Transform> tempGarnishList = new Dictionary<string, Transform>();

        foreach (Transform garnish in this.allGarnishSilos)
        {
            Transform ingredient = garnish.GetComponent<Garnish>().GetIngredientPrefab();
            tempGarnishList.Add(ingredient.name, garnish);
        }

        return tempGarnishList;
    }

    private Transform CreateDrinkObject(string drinkName)
    {
        // Check to make sure the drink list actually contains the drink in question
        if (!drinkList.ContainsKey(drinkName))
        {
            Debug.Log("Error! Drink List does not contain drink called: " + drinkName);
            return null;
        }

        // Create the drink object from a prefab
        Transform drink = Instantiate(blankDrinkPrefab) as Transform;
        
        // Grab the list of the ingredient names from the drink dictionary
        List<string> drinkIngredients = drinkList[drinkName];

        // Go through the ingredient list and instantiate each ingredient as an object
        foreach (string ingredientName in drinkIngredients)
        {
            if (!this.bubbleList.ContainsKey(ingredientName))
            {
                Debug.Log("Error! Ingredient List does not contain an ingredient called: " + ingredientName);
                return null;
            }
            Transform ingredient = Instantiate(this.bubbleList[ingredientName]) as Transform;
            ingredient.parent = drink;
        }
        
        return drink;
    }

    private List<string> GenerateRandomBubbleButtons()
    {
        List<string> randomIngredientNames = new List<string>();

        while (randomIngredientNames.Count < this.playerDifficulty + 1)
        {
            // Pick a random unique ingredient from the dictionary
            int randomNumber = UnityEngine.Random.Range(0, bubbleList.Count);
            string selectedBubble = bubbleList.ElementAt(randomNumber).Key;

            if (!randomIngredientNames.Contains(selectedBubble))
            {
                // Selected bubble hasn't been added yet, so add it to list
                randomIngredientNames.Add(selectedBubble);

                // Get the correct position offset from the number of bubbles 
                float positionOffset = (randomIngredientNames.Count - 1) * this.buttonSpacing;
                Vector2 buttonPosition = new Vector2(this.transform.position.x, this.transform.position.y - positionOffset);
                
                // Send position value and chosen bubble to the appropriate function
                CreateButtonFromBubble(bubbleList[selectedBubble], buttonPosition);
            }
        }

        return randomIngredientNames;
    }

    private Transform CreateButtonFromBubble(Transform bubble, Vector2 position)
    {
        // Create the button object from the blank button prefab
        Transform bubbleButton = Instantiate(blankBubbleButtonPrefab, position, Quaternion.identity) as Transform;
        bubbleButton.name = bubble.name + " Button";

        // Set the sprite of the new button to the sprite of the bubble
        Sprite bubbleSprite = bubble.GetComponent<SpriteRenderer>().sprite;
        bubbleButton.GetComponent<SpriteRenderer>().sprite = bubbleSprite;

        // Set the x/y scale of the button to be the same as the bubble multiplied by a set value
        Vector3 newScale = bubble.transform.localScale;
        newScale.x *= buttonScale;
        newScale.y *= buttonScale;
        bubbleButton.transform.localScale = newScale;

        // Set the radius of the button's Circle Collider to be the same as the bubble
        CircleCollider2D bubbleCollider = bubble.GetComponent<CircleCollider2D>();
        bubbleButton.GetComponent<CircleCollider2D>().radius = bubbleCollider.radius;

        // Set the generated bubble associated with this button to the selected bubble
        BubbleButton buttonScript = bubbleButton.GetComponent<BubbleButton>();
        buttonScript.SetBubbleTransform(bubble);

        return bubbleButton;
    }

    private Transform CreateIngredient(string ingredientName)
    {
        Transform ingredient;
        // Check to see if ingredient is a garnish
        if (ingredientName.StartsWith("*"))
        {
            // Ingredient is a garnish
            string garnishName = ingredientName.TrimStart(new char[] { '*' });
            Transform garnishPrefab = garnishList[garnishName];
            Garnish garnishScript = garnishPrefab.GetComponent<Garnish>();

            // Instantiate a copy of the ingredient attached to the garnish associated with ingredientName
            ingredient = Instantiate(garnishScript.GetIngredientPrefab()) as Transform;
            ingredient.name = garnishName;
        }
        else
        {
            // Get the bubble script from the bubble prefab associated with the name of the ingredient
            Transform bubblePrefab = bubbleList[ingredientName];
            Bubble bubbleScript = bubblePrefab.GetComponent<Bubble>();

            // Instantiate a copy of the ingredient attached to the bubble associated with ingredientName
            ingredient = Instantiate(bubbleScript.GetIngredientPrefab()) as Transform;
            ingredient.name = ingredientName;
        }

        return ingredient;
    }
    
    private Transform CreateDrinkObject(string drinkName, List<string> drinkIngredients)
    {
        Transform drink = Instantiate(blankDrinkPrefab) as Transform;
        drink.name = drinkName;

        foreach (string ingredientName in drinkIngredients)
        {
            // Check the list of bubbles for the ingredient in question or if it is a garnish
            if (!bubbleList.ContainsKey(ingredientName) && !ingredientName.StartsWith("*"))
            {
                // Ingredient both does not exist in the bubble list and does not have a "*" indicating it is not a garnish
                Debug.Log("Error! Ingredient called \"" + ingredientName + "\"does not exist, skipping " + drinkName);
                return null;
            }
            Transform ingredient = CreateIngredient(ingredientName);
            ingredient.parent = drink;
        }

        return drink;
    }

    public List<Transform> GenerateRandomDrinksFromIngredients(List<string> selectedIngredientNames)
    {
        int numberOfDrinks = playerDifficulty + 2;
        List<string> drinksWithSelectedIngredients = new List<string>();
        List<Transform> selectedDrinks = new List<Transform>();
        
        // Iterate through the list of drinks and get rid of the ones that aren't solely comprised of ingredients with the selected names or garnishes
        foreach (KeyValuePair<string, List<string>> drinkRecipe in drinkList)
        {
            bool drinkContainsSelectedIngredients = true;
            foreach (string ingredientName in drinkRecipe.Value)
            {
                // Check to see if each ingredient in the recipe has a bubble button, or it is a garnish
                if (!selectedIngredientNames.Contains(ingredientName) && !ingredientName.StartsWith("*"))
                {
                    drinkContainsSelectedIngredients = false;
                    break;
                }
            }

            // Check to see if the bool is still true after checking all ingredients in the list
            if (drinkContainsSelectedIngredients && !drinksWithSelectedIngredients.Contains(drinkRecipe.Key))
            {
                // Create the drink object using the CreateDrinkObject function and add it to the list of drinks
                drinksWithSelectedIngredients.Add(drinkRecipe.Key);
            }
        }
        
        while (selectedDrinks.Count < numberOfDrinks && drinksWithSelectedIngredients.Count != 0)
        {
            // Get a random drink from the list
            string randomDrinkName = drinksWithSelectedIngredients.ElementAt(UnityEngine.Random.Range(0, drinksWithSelectedIngredients.Count));

            Transform drinkObject = CreateDrinkObject(randomDrinkName, drinkList[randomDrinkName]);

            if (drinkObject == null)
            {
                // If CreateDrinkFromList returns a null, it means that creating the drink failed, so skip this one and keep going
                continue;
            }
            else
            {
                selectedDrinks.Add(drinkObject);
                drinksWithSelectedIngredients.Remove(randomDrinkName);
            }
        }

        return selectedDrinks;
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}
}
