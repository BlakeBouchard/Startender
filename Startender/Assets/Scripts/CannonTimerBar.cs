using UnityEngine;
using System.Collections;

public class CannonTimerBar : MonoBehaviour {

    public float maxScale = 0.2620136f;

	// Set the length of the bar.
	void SetLength (float percent) {

	    float length = percent * maxScale;
        // Debug.Log("Percent: " + percent + ", Length: " + length);
        gameObject.transform.localScale = new Vector3(length, maxScale, 0);
	}
}
