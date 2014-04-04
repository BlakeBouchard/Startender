using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

	GameObject gameManager;
	GameManager gameManagerScript;

    public Transform ingredientPrefab;
    public Ingredient attachedIngredient;

    // Use this for initialization
    void Start()
    {
        Debug.Log("New Bubble with Ingredient: " + GetIngredient().name);

		gameManager = GameObject.Find("Game Manager");
		gameManagerScript = (GameManager) gameManager.GetComponent(typeof(GameManager));

        Transform ingredientObject = Instantiate(ingredientPrefab) as Transform;
        ingredientObject.name = ingredientPrefab.name;
        ingredientObject.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
		
    }

    public Ingredient GetIngredient() 
    {
        return GetComponentInChildren<Ingredient>();
    }

	public void Die()
    {
		Destroy (gameObject);
	}

}
