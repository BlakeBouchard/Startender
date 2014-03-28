using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SliderDrag : MonoBehaviour {

    private Vector3 startPosition;
    private Vector3 maxLeftPosition;
    
    public List<Drink> drinkList;
	public int drinkListIndex;

    // Set this if we want the slider to snap back to its original position on release
    public bool snapsBack = false;

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
        float maxLeftX = startPosition.x * -1;
        maxLeftPosition = new Vector3(maxLeftX, startPosition.y, startPosition.z); 

		this.drinkListIndex = 0;
	}

    public void SetDrinks(List<Drink> drinks) {
        Debug.Log("Adding drinks to drink book.");
        this.drinkList = drinks;
    }

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
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
