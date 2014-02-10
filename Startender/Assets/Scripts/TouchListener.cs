using UnityEngine;
using System.Collections;

public class TouchListener : MonoBehaviour {

    // If this is true, a touch event will fire on all colliders underneath a particular touch
    public bool multiHit = false;

    // If this is true, each finger on the screen will be capable of touching.
    public bool multiTouch = true;

	// Use this for initialization
	void Start () {
	
	}

    void CheckTouch(Touch touch)
    {
        // Touches are measured in camera coordinates, not world position, so we need to convert first
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);

        // Now we need to detect if the converted world coordinates for the touch overlap with any colliders
        if (multiHit)
        {
            Collider2D[] colliders = Physics2D.OverlapPointAll(worldPoint);
            foreach (Collider2D collider in colliders)
            {
                DelegateTouchEvent(collider, touch);
            }
        }
        else
        {
            Collider2D collider = Physics2D.OverlapPoint(worldPoint);
            DelegateTouchEvent(collider, touch);
        }
    }

    void DelegateTouchEvent(Collider2D collider, Touch touch) {
        switch (touch.phase)
        {
            case TouchPhase.Began :
                collider.BroadcastMessage("OnTouchDown", touch);
                break;
            case TouchPhase.Moved :
                collider.BroadcastMessage("OnTouchDrag", touch);
                break;
            case TouchPhase.Canceled :
            case TouchPhase.Ended :
                collider.BroadcastMessage("OnTouchUp", touch);
                break;
            case TouchPhase.Stationary :
                collider.BroadcastMessage("OnTouchStay", touch);
                break;
        }
    }
	
	// Update is called once per frame
    void Update()
    {
        if (multiTouch)
        {
            // Let's cycle through all of the touches on the field right now and do something with them
            foreach (Touch touch in Input.touches)
            {
                CheckTouch(touch);
            }
        }
        else
        {
            // Let's just check the first touch in the list, ignore the rest
            CheckTouch(Input.GetTouch(0));
        }
    }
}
