using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CupIngredients : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Trigger detected from: " + collision.gameObject.name);
        Bubble bubble = collision.gameObject.GetComponent<Bubble>();

        if (bubble != null)
        {
            bubble.getIngredient().transform.parent = transform;

            Destroy(collision.gameObject);
            audio.Play();
        }
    }

    public void ResetIngredients()
    {
        foreach (Ingredient child in GetComponentsInChildren<Ingredient>())
        {
            Destroy(child.gameObject);
        }
    }

    public List<Ingredient> GetIngredients()
    {
        return new List<Ingredient>(GetComponentsInChildren<Ingredient>());
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
