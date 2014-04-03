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
    
    Dictionary<string, List<string>> drinkList;

    // This Dictionary stores each bubble with the key being the name of the ingredient attached to it
    Dictionary<string, Transform> bubbleList;

	// Use this for initialization
	void Start()
    {
        this.drinkList = PopulateDrinkList();
        this.bubbleList = PopulateBubbleList();

        GenerateRandomBubbleButtons();
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
        
        foreach (Transform bubble in allBubbles)
        {
            tempBubbleList.Add(bubble.GetComponentInChildren<Ingredient>().name, bubble);
        }

        return tempBubbleList;
    }

    private Transform CreateDrinkObject(string drinkName)
    {
        if (!drinkList.ContainsKey(drinkName))
        {
            Debug.Log("Error! Drink List does not contain drink called: " + drinkName);
            return null;
        }

        Transform drink = Instantiate(blankDrinkPrefab) as Transform;
        List<string> drinkIngredients = drinkList[drinkName];

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

    private void GenerateRandomBubbleButtons()
    {
        PlayerState player = GetComponent<PlayerState>();
        int difficulty = player.GetDifficulty();

        List<Transform> randomBubbles = new List<Transform>();

        while (randomBubbles.Count < difficulty + 1)
        {
            // Pick a random unique ingredient from the dictionary
            int randomNumber = UnityEngine.Random.Range(0, bubbleList.Count);
            Transform selectedBubble = bubbleList.ElementAt(randomNumber).Value;

            if (!randomBubbles.Contains(selectedBubble))
            {
                // Selected bubble hasn't been added yet, so add it to list
                randomBubbles.Add(selectedBubble);
                CreateButtonFromBubble(selectedBubble);
            }
        }
    }

    private Transform CreateButtonFromBubble(Transform bubble)
    {
        return null;
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}

}
