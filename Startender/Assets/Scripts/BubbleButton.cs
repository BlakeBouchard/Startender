using UnityEngine;
using System.Collections;

public class BubbleButton : MonoBehaviour {

	public Transform bubble;
    GameObject cannon;
    Cannon cannonScript;

	// Use this for initialization
	void Start ()
    {
        cannon = GameObject.Find("CannonBarrel");
        cannonScript = (Cannon) cannon.GetComponent(typeof(Cannon));
	}

    void OnTouchDown()
    {
        Debug.Log("Touched Bubble Button");
        cannonScript.loadBubble(bubble);
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked Bubble Button");
        cannonScript.loadBubble(bubble);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount >= 1)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 wp = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector2 touchPos = new Vector2(wp.x, wp.y);
                    if (collider2D == Physics2D.OverlapPoint(touchPos))
                    {
                        this.OnTouchDown();
                    }
                }
            }
        }
		
	}
}
