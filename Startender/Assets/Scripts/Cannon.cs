using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    public Transform redBubble;
    public float xForce = 1.0f;
    public float yForce = 1.0f;

    private bool goingUp;

    public static float MIN_CANNON_ANGLE = 315.0f;
    public static float MAX_CANNON_ANGLE = 345.0f;

    public float bubbleSpeed;
    public float rotateSpeed;
	public float bubbleMass;
	public float bubbleDrag;
	public float gravityScale;

	public float fireDelayTimer;
	public float fireDelayTime;
	public float reloadTime;
	public int loadedBubbles;
	public bool cannonLoaded;

	// Use this for initialization
	void Start () {
		bubbleMass = 15.0f;
		bubbleSpeed = 10.0f;
		rotateSpeed = 30.0f;
		bubbleDrag = 0.15f;
		gravityScale = 10.0f;

		cannonLoaded = false;
		loadedBubbles = 0;
		reloadTime = 0.8f;
		fireDelayTime = 2.0f;
		this.resetFireDelay();
	}

    public void fireBubble()
    {
        Transform bubble = Instantiate(redBubble, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), Quaternion.identity) as Transform;
        float bubbleAngle = (transform.rotation.eulerAngles.z - 270) * Mathf.Deg2Rad;
        bubble.gameObject.rigidbody2D.velocity = new Vector2(Mathf.Cos(bubbleAngle) * bubbleSpeed, Mathf.Sin(bubbleAngle) * bubbleSpeed);
		bubble.gameObject.rigidbody2D.drag = bubbleDrag;
		bubble.gameObject.rigidbody2D.mass = bubbleMass;
		bubble.gameObject.rigidbody2D.gravityScale = gravityScale;
    }

    void IncrementAngle()
    {
        if (goingUp)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
        }

        if (transform.rotation.eulerAngles.z <= MIN_CANNON_ANGLE)
        {
            goingUp = true;
        }
        else if (transform.rotation.eulerAngles.z >= MAX_CANNON_ANGLE)
        {
            goingUp = false;
        }
    }

	private void resetFireDelay() {
		this.fireDelayTimer = this.fireDelayTime;
	}

	public void loadBubble() {

		//if we're about to fire a shot, reset so they come out close to each other
		if(this.cannonLoaded) {
			this.resetFireDelay();
		}

		this.loadedBubbles++;
		this.cannonLoaded = true;
	}

	private void fireIfReady() {

		//if we've reached the delay, fire
		if(this.fireDelayTimer <= 0.0f) {
			this.fireBubble();
			this.loadedBubbles--;
		
			//if we still have shots left, "reload"
			if(this.loadedBubbles > 0) {
				this.fireDelayTimer = 0.0f + this.reloadTime;
			}
			else {
				this.cannonLoaded = false;
				this.resetFireDelay();
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A)) {
            this.loadBubble();
        }

		if(this.cannonLoaded) {
			this.fireDelayTimer -= Time.deltaTime;
			this.fireIfReady();
		}

        IncrementAngle();

	}
}
