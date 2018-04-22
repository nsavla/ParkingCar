using System.Collections;
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
		if(SceneManager.GetActiveScene().buildIndex != 3)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		else 
			SceneManager.LoadScene(0);
	}
}
