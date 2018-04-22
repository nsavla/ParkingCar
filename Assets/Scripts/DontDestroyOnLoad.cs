using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

	[SerializeField]
	private AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		
		DontDestroyOnLoad (this.gameObject);	
		if (audioSource.isPlaying) {
			if(GameObject.FindGameObjectsWithTag ("BackgroundAudio").Length > 1)
			{
				Destroy(this.gameObject);
			}
		}

	}


}
