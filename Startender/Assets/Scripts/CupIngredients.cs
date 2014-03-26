using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CupIngredients : MonoBehaviour {

    //Ingredients in cup, in order. Added by Rebeca Rey. Necessary to remember order.
    List<Ingredient> ingredientList = new List<Ingredient>();

	// Use this for initialization
	void Start ()
    {
        
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bubble")
        {
            OnTriggerEnter2D(collision.collider);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Bubble")
        {
            Debug.Log("Trigger detected from: " + collider.gameObject.name);
            Bubble bubble = collider.gameObject.GetComponent<Bubble>();

            if (bubble != null && bubble.GetIngredient() != null)
            {
                //Add ingredient to list organized.
                ingredientList.Add(bubble.GetIngredient());
                bubble.GetIngredient().transform.parent = this.transform;

                Destroy(collider.gameObject);
                audio.Play();
            
			}
            else
            {
				Garnish garnish = collider.gameObject.GetComponent<Garnish>();

				if (garnish != null && garnish.GetIngredient() != null)
				{
					//Add ingredient to list organized.
					ingredientList.Add(garnish.GetIngredient());
					garnish.GetIngredient().transform.parent = this.transform;
					
					Destroy(collider.gameObject);
					audio.Play();
				}
			}



		}
        else if (collider.tag == "Garnish")
		{
			Debug.Log("Trigger detected from: " + collider.gameObject.name);
			Garnish garnish = collider.gameObject.GetComponent<Garnish>();
			
			if (garnish != null && garnish.GetIngredient() != null)
			{
				//Add ingredient to list organized.
				ingredientList.Add(garnish.GetIngredient());
				garnish.GetIngredient().transform.parent = this.transform;

				audio.Play();
			}
		}
    }

    public void ResetIngredients()
    {
        foreach (Ingredient child in GetComponentsInChildren<Ingredient>())
        {
            Destroy(child.gameObject);
        }
        //Reset the list.
        ingredientList.Clear();
    }

    public List<Ingredient> GetIngredients()
    {
        foreach (Ingredient ingredient in ingredientList)
        {
            Debug.Log(ingredient.name);
        }
        return ingredientList;
    }

    public int GetIngredientCount()
    {
        return ingredientList.Count;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
