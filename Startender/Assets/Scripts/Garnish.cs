using UnityEngine;
using System.Collections;

public class Garnish : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
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

