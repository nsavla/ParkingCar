using System.Collections;
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
	[SerializeField] private GameObject ObjectToBeSwiped;

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

			touchPosition.z = 10.0f;
			Vector3 tempInputVector = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f);

			if (ObjectToBeSwiped.activeSelf) {
				if (checkIfSwipeGameObject (ObjectToBeSwiped, Camera.main.ScreenToWorldPoint (touchPosition), Camera.main.ScreenToWorldPoint (tempInputVector))) {
					ObjectToBeSwiped.SetActive (false);
				}
			}

			//Debug.Log ("BEAR pOSITION : " + ObjectToBeSwiped.transform.position.x + " , " + ObjectToBeSwiped.transform.position.y);
			//Debug.Log ("Touch POSITION : " + Camera.main.ScreenToWorldPoint( touchPosition ));
			//Debug.Log ("Input mouse POSITION : " + Camera.main.ScreenToWorldPoint (tempInputVector));
			//Debug.Log ("Swiped Gameobject = " + checkIfSwipeGameObject (ObjectToBeSwiped,Camera.main.ScreenToWorldPoint (touchPosition),Camera.main.ScreenToWorldPoint (tempInputVector)));
		}
	}

	public bool IsSwiping(SwipeDirection direction)
	{
		if (this.Direction == direction) 
			return true;
		else
			return false;
	}

	private bool checkIfSwipeGameObject(GameObject ObjectToBeSwiped, Vector3 InitialTouch, Vector3 FinalTouch)
	{
		if (ObjectToBeSwiped == null)
			return false;

		if(isBetween(ObjectToBeSwiped.transform.position.x , InitialTouch.x , FinalTouch.x))
		{
			if(isBetween(ObjectToBeSwiped.transform.position.y , InitialTouch.y , FinalTouch.y))
			{
						return true;
			}
		}
		return false;
	}

	private bool isBetween(float valueCheck, float valueFirst, float valueSecond)
	{
		if (valueFirst < valueCheck && valueCheck < valueSecond)
			return true;
		else if (valueSecond < valueCheck && valueCheck < valueFirst)
			return true;
		else
			return false;
	}
}
