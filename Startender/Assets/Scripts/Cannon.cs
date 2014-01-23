using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    public Transform redBubble;
    public float xForce = 1.0f;
    public float yForce = 1.0f;

    private bool goingUp;

    public static float MIN_CANNON_ANGLE = 300.0f;
    public static float MAX_CANNON_ANGLE = 345.0f;

    public float bubbleOffset = 0.5f;

    public float bubbleSpeed = 10.0f;

    public float rotateSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        
	}

    public void SpawnBubble()
    {
        Transform bubble;
        bubble = Instantiate(redBubble, transform.position, Quaternion.identity) as Transform;
        float bubbleAngle = transform.rotation.eulerAngles.z;
        bubble.gameObject.rigidbody2D.velocity = new Vector2(Mathf.Cos(bubbleAngle) * bubbleSpeed, Mathf.Sin(bubbleAngle) * bubbleSpeed);
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
            Debug.Log("Going Up, rotation is: " + transform.rotation.eulerAngles.z);
        }
        else if (transform.rotation.eulerAngles.z >= MAX_CANNON_ANGLE)
        {
            goingUp = false;
            Debug.Log("Going Down,  rotation is: " + transform.rotation.eulerAngles.z);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnBubble();
        }

        IncrementAngle();

	}
}
