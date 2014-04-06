using UnityEngine;
using System.Collections;

public class Garnish : MonoBehaviour
{
    public Transform ingredientPrefab;
    private Ingredient attachedIngredient;

	// Use this for initialization
	void Start ()
	{
        Transform ingredientObject = Instantiate(ingredientPrefab) as Transform;
        ingredientObject.name = ingredientPrefab.name;
        ingredientObject.parent = this.transform;
        attachedIngredient = ingredientObject.GetComponent<Ingredient>();

        Debug.Log("New Garnish with Ingredient: " + GetIngredient().name);
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	public Ingredient GetIngredient() 
	{
		return attachedIngredient;
	}

	public void Die()
	{
		Destroy (gameObject);
	}

    internal Transform GetIngredientPrefab()
    {
        return ingredientPrefab;
    }
}

