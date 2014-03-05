using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

	GameObject gameManager;
	GameManager gameManagerScript;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("New Bubble with Ingredient: " + GetIngredient().name);

		gameManager = GameObject.Find("Game Manager");
		gameManagerScript = (GameManager) gameManager.GetComponent(typeof(GameManager));
    }
    
    // Update is called once per frame
    void Update ()
    {
		if (gameManagerScript.GetGameState () == GameManager.GameState.RoundOver) {
			Debug.Log("Destroying bubbles at end of round");
			this.Die();
		}
    }

    void OnCollisionEnter2D(Collision2D collision)
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
