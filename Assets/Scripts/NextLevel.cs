﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class NextLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadNextLevel()
	{
		//Debug.Log ("Build Index =" + SceneManager.GetActiveScene().buildIndex );
		if (SceneManager.GetActiveScene ().buildIndex != SceneManager.sceneCount -1)
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		else 
			SceneManager.LoadScene(0);
	}
}
