﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirection
{
	None = 0,
	Left = 1,
	Right = 2,
	Up = 4,
	Down = 8
}


public class SwipeManager : MonoBehaviour {

	private static SwipeManager instance;
	public static SwipeManager Instance{get{return instance;}}

	public SwipeDirection Direction { get; set;}

	private Vector3 touchPosition;
	private float swipeResistanceX = 30.0f;
	private float swipeResistanceY = 30.0f;

	// Use this for initialization
	private void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		Direction = SwipeDirection.None;

		if (Input.GetMouseButtonDown (0)) {
			touchPosition = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp (0)) {
			Vector2 deltaSwipe = touchPosition - Input.mousePosition;
			if (Mathf.Abs (deltaSwipe.x) > swipeResistanceX) {
				//Swipe on X axis
				Direction |= (deltaSwipe.x < 0) ? SwipeDirection.Right : SwipeDirection.Left;
			}

			if (Mathf.Abs (deltaSwipe.y) > swipeResistanceY) {
				//Swipe on Y axis
				Direction |= (deltaSwipe.y < 0) ? SwipeDirection.Up : SwipeDirection.Down;
			}
		}
	}

	public bool IsSwiping(SwipeDirection direction)
	{
		if (this.Direction == direction) 
			return true;
		else
			return false;
	}
}
