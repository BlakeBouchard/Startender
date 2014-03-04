using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cannon : MonoBehaviour {
	
    public float xForce = 1.0f;
    public float yForce = 1.0f;

    private bool goingUp;

    public static float MIN_CANNON_ANGLE = 320.0f;
    public static float MAX_CANNON_ANGLE = 345.0f;

    public float bubbleSpeed = 12.5f;
    public float rotateSpeed = 30.0f;
	public float bubbleMass = 15.0f;
	public float bubbleDrag = 0.2f;
	public float gravityScale = 10.0f;

	public float fireDelayTimer;
	public float fireDelayTime = 2.0f;
	public float reloadTime = 0.8f;

	private Queue<Transform> bubbleQueue;

	GameObject gameManager;
	GameManager gameManagerScript;

	// Use this for initialization
	void Start () {
		this.resetFireDelay();

		bubbleQueue = new Queue<Transform>();

		gameManager = GameObject.Find("Game Manager");
		gameManagerScript = (GameManager) gameManager.GetComponent(typeof(GameManager));
	}

    public void fireBubble(Transform bubbleObj)
    {
        Transform bubble = Instantiate(bubbleObj, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), Quaternion.identity) as Transform;
        float bubbleAngle = (transform.rotation.eulerAngles.z - 270) * Mathf.Deg2Rad;
        bubble.gameObject.rigidbody2D.velocity = new Vector2(Mathf.Cos(bubbleAngle) * bubbleSpeed, Mathf.Sin(bubbleAngle) * bubbleSpeed);
		bubble.gameObject.rigidbody2D.drag = bubbleDrag;
		bubble.gameObject.rigidbody2D.mass = bubbleMass;
		bubble.gameObject.rigidbody2D.gravityScale = gravityScale;
        audio.Play();
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

	public void loadBubble(Transform bubble) {

		//if we're about to fire a shot, reset so they come out close to each other
		if(this.bubbleQueue.Count > 0) {
			this.resetFireDelay();
		}

		this.bubbleQueue.Enqueue(bubble);
	}

	private void fireIfReady() {

		//if we've reached the delay, fire
		if(this.fireDelayTimer <= 0.0f) {
			this.fireBubble(bubbleQueue.Dequeue());
		
			//if we still have shots left, "reload"
			if(this.bubbleQueue.Count > 0) {
				this.fireDelayTimer = 0.0f + this.reloadTime;
			}
			else {
				this.resetFireDelay();
			}
		}

	}
	
	// Update is called once per frame
	void Update () {

		if(this.bubbleQueue.Count > 0) {
			this.fireDelayTimer -= Time.deltaTime;
			this.fireIfReady();
		}

        IncrementAngle();

		if (gameManagerScript.GetGameState () == GameManager.GameState.RoundOver
		    && this.bubbleQueue.Count > 0) {
			Debug.Log("Clearing bubble queue");
			this.bubbleQueue.Clear();	
		}

	}
}
