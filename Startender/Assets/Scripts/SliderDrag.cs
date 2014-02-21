using UnityEngine;
using System.Collections;

public class SliderDrag : MonoBehaviour {

    private Vector3 startPosition;
    public bool rightToLeft = true;
    
    // Set this if we want the slider to snap back to its original position on release
    public bool snapsBack = false;

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
	}

    // This function should send the slider back to its starting position 
    void SnapBack ()
    {
        transform.position = startPosition;
    }

    // This code should handle moving the slider from left to right
    // The position should be sent in world coordinates, ie. converted from camera coordinates
    void MoveSliderHorizontally (Vector3 newPosition)
    {
        if ((rightToLeft && newPosition.x < startPosition.x) || (!rightToLeft && newPosition.x > startPosition.x))
        {
            transform.position = new Vector3(newPosition.x, startPosition.y, startPosition.z);
        }
        else
        {
            transform.position = startPosition;
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
