using UnityEngine;
using System.Collections;

public class Cup : MonoBehaviour {

    private bool clicked;

	// Use this for initialization
	void Start () {
        this.clicked = false;
	}

    void checkTouch()
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
            Destroy(collider.gameObject);
            Debug.Log("Killed bubble");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 1)
        {
            checkTouch();
        }
        else if (Input.GetMouseButton(0))
        {
            checkClick();
        }
	}
}
