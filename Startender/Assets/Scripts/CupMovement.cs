using UnityEngine;
using System.Collections;

public class CupMovement : MonoBehaviour {

    private Vector3 previousPosition;

    public float rotateThreshold = 0.03f;

	GameObject gameManager;
	GameManager gameManagerScript;

    // Use this for initialization
    void Start ()
    {
		gameManager = GameObject.Find("Game Manager");
		gameManagerScript = (GameManager) gameManager.GetComponent(typeof(GameManager));
	}
	
	private void MoveCup(Vector3 startPoint, Vector3 endPoint)
    {
		if (gameManagerScript.GetGameState () == GameManager.GameState.Playing) {
			Vector3 deltaPosition = endPoint - startPoint;

			// Get rotation
			if (Mathf.Abs (deltaPosition.x) > rotateThreshold && Mathf.Abs (deltaPosition.y) > rotateThreshold) {
					float angle = Mathf.Rad2Deg * Mathf.Atan2 (deltaPosition.y, deltaPosition.x);
					transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle - 90));
			}

			float posHeight = Camera.main.orthographicSize; 		// Top
			float negHeight = posHeight * -1f;						// Bottom
			float posWidth = posHeight * Camera.main.aspect;		// Right
			float negWidth = posWidth * -1f;						// Left

			// Set position
			if (endPoint.x < posWidth &&
				endPoint.x > negWidth &&
				endPoint.y < posHeight &&
				endPoint.y > negHeight) {
					transform.position += deltaPosition;
			}
		}
    }

    void OnTouchDown(Touch touch)
    {
        previousPosition = Camera.main.ScreenToWorldPoint(touch.position);
    }

    void OnTouchDrag(Touch touch)
    {
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(touch.position);
        if (currentPosition != previousPosition)
        {
            MoveCup(previousPosition, currentPosition);
            previousPosition = currentPosition;
        }
    }

    private void OnMouseDown()
    {
        previousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (currentPosition != previousPosition)
        {
            MoveCup(previousPosition, currentPosition);
            previousPosition = currentPosition;
        }
    }
    
    // Update is called once per frame
    void Update () {

    }

    /*
    private void DoubleTouch()
    {

        //get our two touch positions
        Vector3 leftTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        Vector3 rightTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);

        //determine if left is actually left
        if (leftTouch.x > rightTouch.x) {
            Vector3 temp = leftTouch;
            leftTouch = rightTouch;
            rightTouch = temp;
        }

        //TODO: figure out the god damn math to pivot/rotate the cup based on double touch turning

    }
    */
}
