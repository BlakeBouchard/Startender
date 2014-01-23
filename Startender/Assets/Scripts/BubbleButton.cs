using UnityEngine;
using System.Collections;

public class BubbleButton : MonoBehaviour {

    // SHE'S SO FAAAAT
    // BUBUBUBUBUBUBUBUBBUBUBBLE-BUTTON

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 1)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (collider2D == Physics2D.OverlapPoint(touchPos))
            {
                Debug.Log("Touched");
            }
        }
	}
}
