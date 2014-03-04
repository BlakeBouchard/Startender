using UnityEngine;
using System.Collections;

public class NextScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}

    void OnTouchDown(Touch touch)
    {
        OnMouseDown();
    }

    // Called when mouse clicks a collider attached to this object
    void OnMouseDown()
    {
        // Go forward one level
        Application.LoadLevel(Application.loadedLevel + 1);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
