using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

    public Transform ingredientPrefab;
    public Transform buttonPrefab;
    private Ingredient attachedIngredient;

    // Use this for initialization
    void Start()
    {
        Transform ingredientObject = Instantiate(ingredientPrefab) as Transform;
        ingredientObject.name = ingredientPrefab.name;
        ingredientObject.parent = this.transform;
        attachedIngredient = ingredientObject.GetComponent<Ingredient>();

        Debug.Log("New Bubble with Ingredient: " + GetIngredient().name);
    }

    // Update is called once per frame
    void Update()
    {
		
    }

    public Ingredient GetIngredient() 
    {
        return attachedIngredient;
    }

    public Transform GetIngredientPrefab()
    {
        return ingredientPrefab;
    }

    public Transform GetButtonPrefab()
    {
        return buttonPrefab;
    }

	public void Die()
    {
		Destroy (gameObject);
	}

}
