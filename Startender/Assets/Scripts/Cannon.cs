using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cannon : MonoBehaviour {
	
    public float xForce = 1.0f;
    public float yForce = 1.0f;

    private bool goingUp;

    public float MIN_CANNON_ANGLE = 320.0f;
    public float MAX_CANNON_ANGLE = 345.0f;
    private Quaternion startRotation;

    public float bubbleSpeed = 12.5f;
    public float rotateSpeed = 30.0f;
	public float bubbleMass = 15.0f;
	public float bubbleDrag = 0.2f;
	public float gravityScale = 10.0f;

	public float fireDelayTimer;
	public float fireDelayTime = 2.0f;
	public float reloadTime = 0.8f;

	private Queue<Transform> bubbleQueue;

	GameManager gameManager;

	// Use this for initialization
	void Start ()
    {
		this.ResetFireDelay();
        this.startRotation = transform.rotation;

		bubbleQueue = new Queue<Transform>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
	}

    public void FireBubble(Transform bubblePrefab)
    {
        Transform bubble = Instantiate(bubblePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), Quaternion.identity) as Transform;
        float bubbleAngle = (transform.rotation.eulerAngles.z - 270) * Mathf.Deg2Rad;
        bubble.rigidbody2D.velocity = new Vector2(Mathf.Cos(bubbleAngle) * bubbleSpeed, Mathf.Sin(bubbleAngle) * bubbleSpeed);
		bubble.rigidbody2D.drag = bubbleDrag;
		bubble.rigidbody2D.mass = bubbleMass;
		bubble.rigidbody2D.gravityScale = gravityScale;
        bubble.name = bubblePrefab.name + " Bubble";
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

    public void EmptyCannon() {
        this.bubbleQueue.Clear();
    }

    public void ResetAngle()
    {
        transform.rotation = startRotation;
    }

	private void ResetFireDelay()
    {
		this.fireDelayTimer = this.fireDelayTime;
	}

	public void LoadBubble(Transform bubble)
    {
		// if we're about to fire a shot, reset so they come out close to each other
		if (this.bubbleQueue.Count > 0) {
			this.ResetFireDelay();
		}

		this.bubbleQueue.Enqueue(bubble);
	}

	private void FireIfReady() {

		// if we've reached the delay, fire
		if (this.fireDelayTimer <= 0.0f)
        {
			this.FireBubble(bubbleQueue.Dequeue());
		
			// if we still have shots left, "reload"
			if (this.bubbleQueue.Count > 0)
            {
				this.fireDelayTimer = 0.0f + this.reloadTime;
			}
			else
            {
				this.ResetFireDelay();
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
    {

		if (this.bubbleQueue.Count > 0)
        {
			this.fireDelayTimer -= Time.deltaTime;
			this.FireIfReady();
		}

        switch (gameManager.GetGameState())
        {
            case GameManager.GameState.Playing :
                IncrementAngle();
                break;
            case GameManager.GameState.Menu :
                ResetAngle();
                break;
		}


        //Set the length of the timer bar.
        float percentLength = this.fireDelayTimer/this.fireDelayTime;
        if (percentLength > 0.99)
        {
            percentLength = 0;
        }
        GameObject.Find("CannonTimerBar").SendMessage("SetLength", percentLength);

	}
}
