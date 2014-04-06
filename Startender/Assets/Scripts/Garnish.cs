using UnityEngine;
using System.Collections;

public class Garnish : MonoBehaviour
{
    GameObject gameManager;
	GameManager gameManagerScript;

    public Transform ingredientPrefab;
    private Ingredient attachedIngredient;

	// Use this for initialization
	void Start ()
	{
		gameManager = GameObject.Find("Game Manager");
		gameManagerScript = (GameManager) gameManager.GetComponent(typeof(GameManager));

        Transform ingredientObject = Instantiate(ingredientPrefab) as Transform;
        ingredientObject.name = ingredientPrefab.name;
        ingredientObject.parent = this.transform;
        attachedIngredient = ingredientObject.GetComponent<Ingredient>();

        Debug.Log("New Garnish with Ingredient: " + GetIngredient().name);
	}

	// Update is called once per frame
	void Update ()
	{
		if (gameManagerScript.GetGameState () == GameManager.GameState.RoundOver) {
			Debug.Log("Destroying garnish at end of round");
			this.Die();
		}
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

