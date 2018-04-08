using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

	[SerializeField] 
	private GameObject GameOverPanel;
	[SerializeField]
	private Text Timer;
	[SerializeField]
	private Text GameOverText;
	[SerializeField]
	private Sprite carSprite;
	private int TimerValue;

	// Use this for initialization
	void Start () {
		GameOverPanel.SetActive (false);
		TimerValue = (int)(Time.time + 15);
	}

	// Update is called once per frame
	void Update () {
		int TimeScore = TimerValue - (int)(Time.time);
		Timer.text = TimeScore.ToString ();
		if (TimeScore <= 0) {
			GameOverText.text = "Time Out";
			GameOverPanel.SetActive (true);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			GameOverText.text = "YOU WIN";
			GameOverPanel.SetActive (true);
		}
	}

	public void MainMenu()
	{
		SceneManager.LoadScene (0);
	}

	public void Restart()
	{
		Debug.Log ("Restart");
		GameObject.FindGameObjectWithTag ("Player").transform.position = new Vector3 (-3, -6, 0);
		GameObject.FindGameObjectWithTag ("Player").GetComponent<SpriteRenderer> ().sprite = carSprite;
		GameOverPanel.SetActive (false);
		TimerValue = (int)(Time.time + 15);
	}

}
