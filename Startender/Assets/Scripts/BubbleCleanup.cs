using UnityEngine;
using System.Collections;

public class BubbleCleanup : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("GET TO THA TRIGGER!!!");
		Bubble bubble = other.GetComponent<Bubble> ();
		if (bubble) {
			bubble.Die();
		}
	}
}
