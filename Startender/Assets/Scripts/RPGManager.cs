using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPGManager : MonoBehaviour
{
	private PlayerState player;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindObjectOfType<PlayerState>();
	}

	// Update is called once per frame
	void Update()
	{

	}

}
