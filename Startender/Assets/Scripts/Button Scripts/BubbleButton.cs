using UnityEngine;
using System.Collections;

public class BubbleButton : MonoBehaviour {

	public Transform bubblePrefab;
    GameObject cannon;
    Cannon cannonScript;
	GameObject gameManager;
	GameManager gameManagerScript;

	// Use this for initialization
	void Start ()
    {
        cannon = GameObject.Find("CannonBarrel");
        cannonScript = (Cannon) cannon.GetComponent(typeof(Cannon));

		gameManager = GameObject.Find("Game Manager");
		gameManagerScript = (GameManager) gameManager.GetComponent(typeof(GameManager));
	}

    void OnTouchDown(Touch touch)
    {
        Debug.Log("Touched Bubble Button");
		if (gameManagerScript.GetGameState () == GameManager.GameState.Playing)
        {
			cannonScript.LoadBubble(this.bubblePrefab);
		}
    }

    void OnMouseDown()
    {
        // Should not fire when touching, screw you Unity Remote
		// Stops cannon from adding bubble if the game is not playing
		if (Input.touchCount == 0 && gameManagerScript.GetGameState () == GameManager.GameState.Playing)
        {
            Debug.Log("Clicked Bubble Button");
            cannonScript.LoadBubble(this.bubblePrefab);
        }
    }

    public void SetBubbleTransform(Transform bubbleTransform)
    {
        this.bubblePrefab = bubbleTransform;
    }
	
	// Update is called once per frame
    void Update()
    {

    }
}
