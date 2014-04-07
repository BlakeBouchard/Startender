using UnityEngine;
using System.Collections;

public class CupMovement : MonoBehaviour {

    private Vector2 previousPosition1;
	private Vector2 previousPosition2;
	private int lastTouchCount;

    public float rotateThreshold = 0.03f;
	public float borderPadding = 010f;

	public Bounds bounds;

	GameObject gameManager;
	GameManager gameManagerScript;

    // Use this for initialization
    void Start ()
    {
		gameManager = GameObject.Find("Game Manager");
		gameManagerScript = (GameManager) gameManager.GetComponent(typeof(GameManager));

		GameObject go = GameObject.Find("Bar");
		this.bounds = go.renderer.bounds;
		this.lastTouchCount = 0;
	}
	
	private void MoveCup(Vector2 endPoint)
    {
		if (gameManagerScript.GetGameState () == GameManager.GameState.Playing) {
			Vector3 deltaPosition = endPoint - this.previousPosition1;

			if(Input.touchCount >= 2) {

				Vector2 pos2 = Input.GetTouch(1).position;
				Vector2 delta2 = this.previousPosition2 - pos2;
				Vector2 touchDelta = endPoint - delta2;

				//only multi-touch rotate if we have a second touch frame of reference
				if(lastTouchCount >= 2) {
					float angle = Mathf.Rad2Deg * Mathf.Atan2 (touchDelta.y, touchDelta.x);
					transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle - 90));
				}

				this.previousPosition2 = pos2;

			}
			//otherwise peform Ghetto-Rotation
			else {
				// Get rotation
				if (Mathf.Abs (deltaPosition.x) > rotateThreshold && Mathf.Abs (deltaPosition.y) > rotateThreshold) {
					float angle = Mathf.Rad2Deg * Mathf.Atan2 (deltaPosition.y, deltaPosition.x);
					transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle - 90));
				}
			}

			float posHeight = Camera.main.orthographicSize; 		// Top
			float negHeight = posHeight * -1f;						// Bottom
			float posWidth = posHeight * Camera.main.aspect;		// Right
			float negWidth = posWidth * -1f;						// Left

			Bounds cup = this.renderer.bounds;

			// Set position
			if (endPoint.x + cup.extents.x < posWidth &&
				endPoint.x - cup.extents.x  > negWidth &&
				endPoint.y + cup.extents.y < posHeight &&
				endPoint.y - cup.extents.x > negHeight) {
				transform.position += deltaPosition;
			}

			this.lastTouchCount = Input.touchCount;
		}
    }

	public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle) {
		return angle * ( point - pivot) + pivot;
	}

    void OnTouchDown(Touch touch)
    {
        this.previousPosition1 = Camera.main.ScreenToWorldPoint(touch.position);
    }

    void OnTouchDrag(Touch touch)
    {
        Vector3 camera = Camera.main.ScreenToWorldPoint(touch.position);
		Vector2 currentPosition = new Vector2(camera.x, camera.y);
        if (currentPosition != this.previousPosition1)
        {
            MoveCup(currentPosition);
            this.previousPosition1 = currentPosition;
        }
    }

    private void OnMouseDown()
    {
        previousPosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector3 camera = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 currentPosition = new Vector2(camera.x, camera.y);
        if (currentPosition != this.previousPosition1)
        {
            MoveCup(currentPosition);
            this.previousPosition1 = currentPosition;
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
