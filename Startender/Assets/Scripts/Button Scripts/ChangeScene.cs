﻿using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

    public string scene = null;

	// Use this for initialization
	void Start ()
    {
	
	}

    // Called when a collider attached to this object is touched
    void OnTouchDown(Touch touch)
    {
        OnMouseDown();
    }

    // Called when mouse is clicked on a collider attached to this object
    void OnMouseDown()
    {
        if (scene != null)
        {
            Application.LoadLevel(scene);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}