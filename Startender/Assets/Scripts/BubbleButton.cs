﻿using UnityEngine;
using System.Collections;

public class BubbleButton : MonoBehaviour {

	public Transform bubble;
    GameObject cannon;
    Cannon cannonScript;

	// Use this for initialization
	void Start () {
        cannon = GameObject.Find("Cannon");
        cannonScript = (Cannon) cannon.GetComponent(typeof(Cannon));
	}

    void OnMouseDown()
    {
        Debug.Log("Clicked Bubble Button");
        cannonScript.loadBubble(bubble);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (collider2D == Physics2D.OverlapPoint(touchPos))
            {
                Debug.Log("Touched Bubble Button");
                cannonScript.loadBubble(bubble);
            }
        }
		
	}
}
