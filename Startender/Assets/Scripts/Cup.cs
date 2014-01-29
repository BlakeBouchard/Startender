using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Cup : MonoBehaviour {

    private bool clicked;
	private List<Ingredient> ingredients;

	public Cup() {
		this.ingredients = new List<Ingredient>();
	}

	// Use this for initialization
	void Start () {
        this.clicked = false;
	}

    private void checkSimpleTouch()
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

	private void doubleTouch() {

		//get our two touch positions
		Vector3 leftTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		Vector3 rightTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);

		//determine if left is actually left
		if(leftTouch.x > rightTouch.x) {
			Vector3 temp = leftTouch;
			leftTouch = rightTouch;
			rightTouch = temp;
		}

		//TODO: figure out the god damn math to pivot/rotate the cup based on double touch turning

	}

    private void checkClick()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition = new Vector2(worldPoint.x, worldPoint.y);
        
		if(!clicked && Input.GetMouseButtonDown(0) && collider2D == Physics2D.OverlapPoint(mousePosition))
        {
            Debug.Log("Clicked Cup");
            clicked = true;
		} 
		else if(clicked && Input.GetMouseButtonUp(0)) {
			clicked = false;
			Debug.Log("Let go of cup");
		}

		if(clicked) {
			Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    		newPosition.z = 0;
    		transform.position = newPosition;
		}

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger detected from: " + collider.gameObject.name);
        if (collider.gameObject.name == "RedBubble(Clone)")
        {
			Bubble bubble = (Bubble) collider.gameObject.GetComponent(typeof(Bubble));
			this.ingredients.Add(bubble.getIngredient());

            Destroy(collider.gameObject);
            Debug.Log("Killed bubble");
        }
		else if(collider.gameObject.name == "Tray")
		{
			//TODO: figure out the most elegant way to handle this interaction
			GameObject dm = GameObject.Find("DrinkManager");
			DrinkManager drinkManager = (DrinkManager) dm.GetComponent(typeof(DrinkManager));

			int tip = drinkManager.finishAndTip(this.ingredients);
			GameManager.getPlayer().addTip(tip);

		}
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.touchCount == 1)
        {
            checkSimpleTouch();
        }
		else if(clicked && Input.touchCount == 2) {
			doubleTouch();
		}
        else if (Input.GetMouseButton(0))
        {
            checkClick();
        }
	}
}
