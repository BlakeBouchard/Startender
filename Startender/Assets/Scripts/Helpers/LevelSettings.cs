using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelSettings : MonoBehaviour {

    // This should contain all available ingredient buttons
    public Transform[] allIngredientButtons;
    
    // This should point to a file in the Text directory called "AllDrinks.txt" in the Inspector
    public TextAsset allDrinks;
    
    Dictionary<string, List<string>> drinkList;

	// Use this for initialization
	void Start()
    {
        this.drinkList = PopulateDrinkList();

        GenerateLevel(drinkList);
        SendDrinksToManager();
	}

    private Dictionary<string, List<string>> PopulateDrinkList()
    {
        Dictionary<string, List<string>> tempDrinkList = new Dictionary<string, List<string>>();

        // Get an array consisting of each non-empty string, delimited by newlines
        string[] drinkLines = allDrinks.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
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

    /** 
     * Should generate a number of random ingredients based on the current difficulty level
     * 
     * Then choose a proportionate number of drinks from the list of drinks
     */
    private void GenerateLevel(Dictionary<string, List<string>> drinkList)
    {
        PlayerState player = GetComponent<PlayerState>();
        int difficulty = player.GetDifficulty();

    }

    private void SendDrinksToManager()
    {
        foreach (KeyValuePair<string, List<string>> drink in drinkList)
        {

        }
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}

}
