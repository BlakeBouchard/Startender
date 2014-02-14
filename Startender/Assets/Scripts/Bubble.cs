using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
        Debug.Log("New Bubble with Ingredient: " + getIngredient().name);
    }
    
    // Update is called once per frame
    void Update ()
    {
        //TODO: Fix garbage collection below
        //this.checkInBounds();
    }

    private void checkInBounds() {
        Vector3 pos = transform.position;
        if (pos.x > Screen.width) {
            Destroy(this.gameObject);
        } else if(pos.y < 0) {
            Destroy(this.gameObject);
        }
    }

    public Ingredient getIngredient() 
    {
        return GetComponentInChildren<Ingredient>();
    }

	public void Die()
    {
		Destroy (gameObject);
	}

}
