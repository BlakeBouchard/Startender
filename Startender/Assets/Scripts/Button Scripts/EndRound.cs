using UnityEngine;
using System.Collections;

public class EndRound : MonoBehaviour {

    private PlayerState player;
    
    // Use this for initialization
	void Start ()
    {
        player = FindObjectOfType<PlayerState>();
	}

    void OnTouchDown(Touch touch)
    {
        player.EndRound();
    }

    void OnMouseDown()
    {
        player.EndRound();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
