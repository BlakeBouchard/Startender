using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

	GameObject gameManager;
	GameManager gameManagerScript;

    public Transform ingredientPrefab;
    private Ingredient attachedIngredient;

    // Use this for initialization
    void Start()
    {
        Debug.Log("New Bubble with Ingredient: " + GetIngredient().name);

		gameManager = GameObject.Find("Game Manager");
		gameManagerScript = (GameManager) gameManager.GetComponent(typeof(GameManager));

        Transform ingredientObject = Instantiate(ingredientPrefab) as Transform;
        ingredientObject.name = ingredientPrefab.name;
        ingredientObject.parent = this.transform;
        attachedIngredient = ingredientObject.GetComponent<Ingredient>();
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

	public void Die()
    {
		Destroy (gameObject);
	}

}
