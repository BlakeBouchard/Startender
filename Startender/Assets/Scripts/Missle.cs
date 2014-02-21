using UnityEngine;
using System.Collections;

public class Missle : MonoBehaviour
{
	public float accelerationTime = 2.0f;
	public float accelerationRemaining = 2.0f;
	public float accelerationRateInUnits = 10.0f;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if(accelerationRemaining > 0.0f) {
			accelerationRemaining -= Time.deltaTime;
		}

		if(accelerationRemaining > 0.0f) {
			rigidbody.velocity = rigidbody.velocity + rigidbody.velocity.normalized * this.accelerationRateInUnits * Time.deltaTime;
		}
	}
	
	public Garnish getGarnish() {
		return this.GetComponentInChildren<Garnish>();
	}
}

