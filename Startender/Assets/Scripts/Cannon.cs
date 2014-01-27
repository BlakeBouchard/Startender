using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    public Transform redBubble;
    public float xForce = 1.0f;
    public float yForce = 1.0f;

    private bool goingUp;

    public static float MIN_CANNON_ANGLE = 300.0f;
    public static float MAX_CANNON_ANGLE = 345.0f;

    public float bubbleSpeed = 10.0f;

    public float rotateSpeed = 2.0f;

	// Use this for initialization
	void Start () {
        
	}

    public void SpawnBubble()
    {
        Transform bubble = Instantiate(redBubble, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), Quaternion.identity) as Transform;
        float bubbleAngle = (transform.rotation.eulerAngles.z - 270) * Mathf.Deg2Rad;
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
        }
        else if (transform.rotation.eulerAngles.z >= MAX_CANNON_ANGLE)
        {
            goingUp = false;
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
