using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cup : MonoBehaviour {

    private bool clicked;
    private List<Ingredient> ingredients;

    private Vector3 previousPosition;

    // Use this for initialization
    void Start ()
    {
        this.clicked = false;
        this.ingredients = new List<Ingredient>();
    }

    private void CheckSimpleTouch()
    {
        Touch touch = Input.GetTouch(0);
        Debug.Log (touch);

        if (!clicked && touch.phase == TouchPhase.Began)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (collider2D == Physics2D.OverlapPoint(touchPos))
            {
                Debug.Log("Touched Cup");
                clicked = true;
            }
        }
        else if (clicked && touch.phase == TouchPhase.Moved)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(touch.position);
            newPosition.z = 0;
            transform.position = newPosition;
        }
        else if (clicked && touch.phase == TouchPhase.Ended)
        {
            Debug.Log("Let go of cup");
            clicked = false;
        }
    }

    private void DoubleTouch()
    {

        //get our two touch positions
        Vector3 leftTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        Vector3 rightTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);

        //determine if left is actually left
        if (leftTouch.x > rightTouch.x) {
            Vector3 temp = leftTouch;
            leftTouch = rightTouch;
            rightTouch = temp;
        }

        //TODO: figure out the god damn math to pivot/rotate the cup based on double touch turning

    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked Cup");
        previousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 deltaPosition = currentPosition - previousPosition;
        transform.position += deltaPosition;
        previousPosition = currentPosition;
    }

    private void OnMouseUp()
    {
        Debug.Log("Let go of cup");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger detected from: " + collider.gameObject.name);
        Bubble bubble = collider.gameObject.GetComponent<Bubble>();

        if (bubble != null)
        {
            this.ingredients.Add(bubble.getIngredient());

            Destroy(collider.gameObject);
            Debug.Log("Killed bubble");
        }
        else if (collider.gameObject.name == "Tray")
        {
            Debug.Log("Tray Touched");

            //TODO: figure out the most elegant way to handle this interaction
            GameObject dm = GameObject.Find("DrinkManager");
            DrinkManager drinkManager = (DrinkManager) dm.GetComponent(typeof(DrinkManager));

            int tip = drinkManager.finishAndTip(this.ingredients);
            GameManager.getPlayer().addTip(tip);
            GameManager.getPlayer().incrementDrinkCount();
        }
    }
    
    // Update is called once per frame
    void Update () {

        if (Input.touchCount == 1)
        {
            CheckSimpleTouch();
        }
        else if (clicked && Input.touchCount == 2)
        {
            DoubleTouch();
        }

    }
}
