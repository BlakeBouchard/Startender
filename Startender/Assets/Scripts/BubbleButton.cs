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

    void OnTouchDown(Touch touch)
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
    void Update()
    {

    }
}
