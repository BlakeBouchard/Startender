﻿using UnityEngine;
using System.Collections;

public class CupMovement : MonoBehaviour {

    private bool clicked;

    private Vector3 previousPosition;

    // Use this for initialization
    void Start ()
    {
        this.clicked = false;
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
