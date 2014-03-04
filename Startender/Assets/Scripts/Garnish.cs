using UnityEngine;
using System.Collections;

public class Garnish : MonoBehaviour
{

	GameObject gameManager;
	GameManager gameManagerScript;

	// Use this for initialization
	void Start ()
	{
		gameManager = GameObject.Find("Game Manager");
		gameManagerScript = (GameManager) gameManager.GetComponent(typeof(GameManager));
	}

	// Update is called once per frame
	void Update ()
	{
		if (gameManagerScript.getGameState () == GameManager.GameState.RoundOver) {
			Debug.Log("Destroying garnish at end of round");
			this.Die();
		}
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

