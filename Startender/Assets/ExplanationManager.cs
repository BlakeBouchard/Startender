using UnityEngine;
using System.Collections;

public class ExplanationManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad > 0.3f && Time.timeSinceLevelLoad < 0.4f)
        {
            GameObject.Find("explanation_1").renderer.enabled = true;
        }

        if (Time.timeSinceLevelLoad > 7.8f && Time.timeSinceLevelLoad < 7.9f)
        {
            GameObject.Find("explanation_2").renderer.enabled = true;
        }

        if (Time.timeSinceLevelLoad > 15.8f && Time.timeSinceLevelLoad < 15.9f)
        {
            GameObject.Find("explanation_3").renderer.enabled = true;
        }

        if (Time.timeSinceLevelLoad > 23.8f && Time.timeSinceLevelLoad < 23.9f)
        {
            GameObject.Find("explanation_4").renderer.enabled = true;
        }

        if (Time.timeSinceLevelLoad > 31.8f && Time.timeSinceLevelLoad < 31.9f)
        {
            GameObject.Find("explanation_5").renderer.enabled = true;
        }

        if (Time.timeSinceLevelLoad > 39.5f)
        {
            // Load whichever scene comes after the Explanation scene in the Unity Project Build Settings
            Application.LoadLevel(Application.loadedLevel + 1);
        }
	}
}
