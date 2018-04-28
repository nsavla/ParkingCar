using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSwipeLevelSelect : MonoBehaviour {
	[SerializeField] private Animator LoadLevelAnimator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Swipe Up" + SwipeManager.Instance);
		if (SwipeManager.Instance.IsSwiping (SwipeDirection.Up)) {
			Debug.Log("Swipe Up");
			LoadLevelAnimator.Play ("UpSwipe");
		} else if (SwipeManager.Instance.IsSwiping (SwipeDirection.Down)) {
			Debug.Log("Swipe Down");
			LoadLevelAnimator.Play ("DownSwipe");
		}	
	}
}
