using UnityEngine;
using System.Collections;

public class Silo : MonoBehaviour {
	
	public float xForce = 1.0f;
	public float yForce = 1.0f;
	
	private bool goingUp;
	
	public static float MIN_CANNON_ANGLE = 320.0f;
	public static float MAX_CANNON_ANGLE = 345.0f;
	
	public float startMissleSpeed = 12.5f;
	public float missleMass = 20.0f;
	public float missleDrag = 0.2f;
	public float gravityScale = 10.0f;
	
	public bool activated = false;
	public bool coolDown = false;
	public float coolDownTimer = 10.0f;
	public float coolDownTime = 0.0f;
	public float fireDelayTimer = 0.0f;
	public float fireDelayTime = 2.0f;
	

	// Use this for initialization
	void Start () {
		this.resetFireDelay();
	}
	
	public void fireGarnish()
	{
		Missle missleObj = this.GetComponent<Missle>();

		Transform missle = Instantiate(missleObj, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), Quaternion.identity) as Transform;
		missle.gameObject.rigidbody2D.velocity = new Vector2(0.0f, this.startMissleSpeed);
		missle.gameObject.rigidbody2D.mass = missleMass;
		missle.gameObject.rigidbody2D.gravityScale = gravityScale;
		audio.Play();
	}
	
	private void resetFireDelay() {
		this.fireDelayTimer = this.fireDelayTime;
	}

	public void onMouseDown() {

		Debug.Log("Silo Clicked");

		if(!coolDown && !activated) {
			Debug.Log("Firing Garnish");
			this.activated = true;
			this.resetFireDelay();
		}

	}

	private void startCoolDown() {
		this.resetFireDelay();

		this.coolDown = true;
		this.coolDownTime = this.coolDownTimer;
	}
	
	private void fireIfReady() {
		
		//if we've reached the delay, fire
		if(this.fireDelayTimer <= 0.0f) {
			this.fireGarnish();
			this.startCoolDown();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.activated) {
			this.fireDelayTimer -= Time.deltaTime;
			this.fireIfReady();
		}

		if(this.coolDown) {
			this.coolDownTimer -= Time.deltaTime;

			if(this.coolDownTimer <= 0.0f) {
				this.coolDown = false;
			}
		}
	}
}