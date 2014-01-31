using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

    private Ingredient ingredient;

    // Use this for initialization
    void Start () {
        this.ingredient = this.GetComponentInChildren<Ingredient>();
        Debug.Log("New Bubble with Ingredient: " + ingredient.name);
    }
    
    // Update is called once per frame
    void Update () {
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

    public Ingredient getIngredient() {
        return this.ingredient;
    }
}
