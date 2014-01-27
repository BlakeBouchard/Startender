using UnityEngine;
using System.Collections;

public class Cup : MonoBehaviour {

    private bool isDragging;

	// Use this for initialization
	void Start () {
        this.isDragging = false;
	}

    void checkTouch()
    {
        Touch touch = Input.GetTouch(0);
		Debug.Log (touch);
        if (touch.phase == TouchPhase.Began)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (collider2D == Physics2D.OverlapPoint(touchPos))
            {
                Debug.Log("Touched Cup");
                isDragging = true;
            }
        }
        else if (isDragging && touch.phase == TouchPhase.Moved)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(touch.position);
            newPosition.z = 0;
            transform.position = newPosition;
        }
        else if (isDragging && touch.phase == TouchPhase.Ended)
        {
            Debug.Log("Let go of cup");
            isDragging = false;
        }
    }

    private void checkClick()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition = new Vector2(worldPoint.x, worldPoint.y);
        if (collider2D == Physics2D.OverlapPoint(mousePosition))
        {
            Debug.Log("Touched Cup");
            isDragging = true;
        }
        else if (isDragging && Input.GetMouseButton(0))
        {
			Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    		newPosition.z = 0;
    		transform.position = newPosition;
        }
        else if (isDragging && Input.GetMouseButtonUp(0))
        {
            Debug.Log("Let go of cup");
            isDragging = false;
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
