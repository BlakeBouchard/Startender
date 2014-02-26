using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

    public Transform cannonPrefab;
    public Transform cupPrefab;
    public Transform trayPrefab;
    
    // Use this for initialization
	void Start ()
    {
        InstantiateCannon();
        InstantiateCup();
        InstantiateTray();
	}

    private void InstantiateCannon()
    {
        Instantiate(cannonPrefab);
    }

    private void InstantiateCup()
    {
        Instantiate(cupPrefab);
    }

    private void InstantiateTray()
    {
        Instantiate(trayPrefab);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
