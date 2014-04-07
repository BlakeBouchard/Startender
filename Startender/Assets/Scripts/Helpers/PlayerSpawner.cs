using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    public Transform playerPrefab;

	// Use this for initialization
	void Start()
    {
        SpawnPlayer();
	}

    private PlayerState SpawnPlayer()
    {
        GameObject playerObject = GameObject.Find("Player");
        
        // This should happen if the player has never started a save game before
        if (!playerObject)
        {
            Debug.Log("No player object found, creating one");
            Transform playerTransform = Instantiate(playerPrefab) as Transform;
            playerTransform.name = playerPrefab.name;
            playerObject = playerTransform.gameObject;
        }

        PlayerState playerState = playerObject.GetComponent<PlayerState>();

		playerState.LoadGame();
		Debug.Log ("Game Loaded");
        return playerState;
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}

}
