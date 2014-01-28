using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("New Bubble");
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: Fix garbage collection below
		//this.checkInBounds();
	}

	private void checkInBounds() {

		Vector3 pos = transform.position;
		Debug.Log (pos.y);
		Debug.Log (Screen.width);

		if(pos.x > Screen.width) {
			Destroy(this.gameObject);
		} else if(pos.y < 0) {
			Destroy(this.gameObject);
		}

	}
}
