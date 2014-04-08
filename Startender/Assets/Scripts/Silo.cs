using UnityEngine;
using System.Collections;


public class Silo : MonoBehaviour {

	public Transform garnish;

	public float xForce = 1.0f;
	public float yForce = 1.0f;
	
	private bool goingUp;
	
	public float startMissleSpeed = 10.5f;
	public float missleMass = 20.0f;
	public float missleDrag = 0.2f;
	public float gravityScale = 10.0f;
	
	public bool activated = false;
	public bool coolDown = false;
	public bool lidOpen = false;

	
	public float coolDownTime = 8.0f;
	public float coolDownTimer = 8.0f;
	public float fireDelayTimer = 2.0f;
	public float fireDelayTime = 2.0f;

	public GameObject lid;

	public AudioClip sound;

	// Use this for initialization
	void Start () {}
	
	public void FireGarnish()
	{
		Transform missle = Instantiate(garnish, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), Quaternion.identity) as Transform;
		missle.gameObject.rigidbody2D.velocity = new Vector2(0.0f, this.startMissleSpeed);
		missle.gameObject.rigidbody2D.mass = missleMass;
		missle.gameObject.rigidbody2D.gravityScale = gravityScale;
		this.audio.clip = this.sound;
		this.audio.Play();
	}
	
	private void ResetFireDelay() {
		this.fireDelayTimer = this.fireDelayTime;
	}

	private void ResetCoolDownTimer() {
		this.coolDownTimer = this.coolDownTime;
	}

	public void OnMouseDown() {

		Debug.Log("Silo Clicked");

		if(!coolDown && !activated) {
			Debug.Log("Firing Garnish");
			this.activated = true;
		}

	}

	private void StartCoolDown() {
		this.ResetFireDelay();

		this.coolDown = true;
		this.activated = false;
		this.lidOpen = false;
	}

	private void RotateLid() {

		float maxAngle = 190.0f;

		if(lidOpen) {
			//float rotAngle = maxAngle / this.fireDelayTime;
			//lid.transform.Rotate(0, 0, rotAngle * Time.deltaTime);
		} else {
			float rotAngle = maxAngle / this.coolDownTime;
			Debug.Log(rotAngle);
			float rotAmount = rotAngle * -1.0f * Time.deltaTime;
			Debug.Log(rotAmount);
//			lid.transform.Rotate(0, 0, rotAmount);
		}

	}
	
	private void FireIfReady() {
		
		//if we've reached the delay, fire
		if(this.fireDelayTimer <= 0.0f) {
			this.FireGarnish();
			this.StartCoolDown();
		} else {
			//this.rotateLid();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.activated) {
			this.fireDelayTimer -= Time.deltaTime;
			this.FireIfReady();
		}

		if(this.coolDown) {
			this.coolDownTimer -= Time.deltaTime;
			//this.rotateLid();

			if(this.coolDownTimer <= 0.0f) {
				this.coolDown = false;
				this.ResetCoolDownTimer();
			}
		}
	}
}