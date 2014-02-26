using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {

    public Transform[] prefabs;
    
    // Use this for initialization
	void Start ()
    {
        foreach (Transform prefab in prefabs)
        {
            Instantiate(prefab);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
