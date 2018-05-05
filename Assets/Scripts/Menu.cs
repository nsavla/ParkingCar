using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BtnStart()
	{
		SceneManager.LoadScene (1);
	}

	public void BtnPlayLevel2()
	{
		SceneManager.LoadScene (2);
	}


	public void BtnPlayLevel3()
	{
		SceneManager.LoadScene (3);
	}

	public void BtnPlayLevel4()
	{
		SceneManager.LoadScene (4);
	}


	public void BtnQuit()
	{
		Application.Quit ();
	}
}
