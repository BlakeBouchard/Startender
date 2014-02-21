using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
        Debug.Log("New Bubble with Ingredient: " + GetIngredient().name);
    }
    
    // Update is called once per frame
    void Update ()
    {
        
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
