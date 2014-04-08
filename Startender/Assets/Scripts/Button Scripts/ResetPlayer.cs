using UnityEngine;
using System.Collections;

public class ResetPlayer : MonoBehaviour {

	// Use this for initialization
	void Start()
    {
	
	}

    void OnMouseDown()
    {
        ResetPlayerState();
    }

    void OnTouchDown(Touch touch)
    {
        ResetPlayerState();
    }

    void ResetPlayerState()
    {
        PlayerState player = GameObject.Find("Player").GetComponent<PlayerState>();
        player.ClearPrefs();
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}
}
