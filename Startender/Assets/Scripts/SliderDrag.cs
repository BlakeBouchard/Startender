using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SliderDrag : MonoBehaviour {

    private Vector3 startPosition;
    private Vector3 maxLeftPosition;
	public GameObject box;
    
    public List<Drink> drinkList;
	public int drinkListPointer;

	public float drinkBookPadding;

    // Set this if we want the slider to snap back to its original position on release
    public bool snapsBack = false;

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
        float maxLeftX = startPosition.x * -1;
        maxLeftPosition = new Vector3(maxLeftX, startPosition.y, startPosition.z); 

		this.drinkBookPadding = 3.0f;
		this.drinkListPointer = 0;

        //feed drinks to Drink Book
        GameObject dmo = GameObject.Find("Drink Manager");
        DrinkManager dm = (DrinkManager) dmo.GetComponent(typeof(DrinkManager));
        this.drinkList = dm.GetDrinks();

		this.box = GameObject.Find("Slider Box");

	}

    // public void SetDrinks(List<Drink> drinks) {
    //     Debug.Log("Adding drinks to drink book.");
    //     this.drinkList = drinks;
    // }

    // This function should send the slider back to its starting position 
    void SnapBack ()
    {
        this.transform.position = startPosition;
    }

    // This code should handle moving the slider from left to right
    // The position should be sent in world coordinates, ie. converted from camera coordinates
    void MoveSliderHorizontally (Vector3 newPosition)
    {
        //if we are going off the left hand side, lock
        if (newPosition.x <= this.maxLeftPosition.x)
        {
            this.transform.position = this.maxLeftPosition;
        }
        //if we are going off the right hand side, lock
        else if(newPosition.x >= this.startPosition.x)
        {
            this.transform.position = this.startPosition;
        }
        //otherwise slide freely 
        else {
            this.transform.position = new Vector3(newPosition.x, this.startPosition.y, this.startPosition.z);
        }
    }

    // Called when user touches a collider attached to this object
    void OnTouchDrag (Touch touch)
    {
        MoveSliderHorizontally(Camera.main.ScreenToWorldPoint(touch.position));
    }

    // Called when user lifts their finger from a collider attached to this object
    void OnTouchUp (Touch touch)
    {
        OnMouseUp();
    }

    // Called when user clicks mouse on a collider attached to this object
    void OnMouseDown ()
    {
        Debug.Log("Clicked slider button");
    }

    // Called when user clicks a collider attached to this object
    void OnMouseDrag ()
    {
        MoveSliderHorizontally(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    // Called when user releases the mouse button while dragging a collider attached to this object
    void OnMouseUp ()
    {
        if (snapsBack)
        {
            SnapBack();
        }
    }

	void OnGUI() {

		float x = this.box.renderer.bounds.min.x;
		x = 0.5f;
		float y = this.box.renderer.bounds.min.y;
		y =0.5f;
		float width = this.box.renderer.bounds.size.x;
		float height = this.box.renderer.bounds.size.y;

		Drink drink = this.drinkList[this.drinkListPointer];

		Rect rect = new Rect(x, y, width, height);

		//left, top, width, height
		GUILayout.BeginArea(rect);

		if(GUILayout.Button("Prev")) {
			if(this.drinkListPointer == 0) {
				this.drinkListPointer = this.drinkList.Count - 1;
			}
			else {
				this.drinkListPointer--;
			}
		}
		if(GUILayout.Button("Next")) {
			if(this.drinkListPointer == this.drinkList.Count - 1) {
				this.drinkListPointer = 0;
			}
			else {
				this.drinkListPointer++;
			}
		}
		GUILayout.Label(drink.GetDrinkName());

		//foreach(Ingredient ingredient in drink.GetIngredients()) {
		//	GUILayout.Label(ingredient.name);
		//}

		GUILayout.EndArea();

	}

	// Update is called once per frame
	void Update ()
    {
	    
	}
}
