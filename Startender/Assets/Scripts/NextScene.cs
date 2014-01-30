using UnityEngine;
using System.Collections;

public class NextScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}

    void OnMouseDown()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
