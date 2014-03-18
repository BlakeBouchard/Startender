using UnityEngine;
using System.Collections;

public class QuitListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Application.loadedLevel == 0)
            {
                Application.Quit();
            }
            else
            {
                Application.LoadLevel(0);
            }
        }
	}
}
