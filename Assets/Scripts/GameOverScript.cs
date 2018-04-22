using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

	[SerializeField] 
	private GameObject player;
	[SerializeField] 
	private GameObject GameOverPanel;
	[SerializeField]
	private Text Timer;
	[SerializeField]
	private Text GameOverText;
	[SerializeField]
	private Sprite carSprite;
	[SerializeField]
	private Button NextLevelButton ;
	private int TimerValue;
	private bool isGameOver = false;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag ("Player");
		GameOverPanel.SetActive (false);
		TimerValue = (int)(Time.time + 15);
		NextLevelButton.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		int TimeScore = TimerValue - (int)(Time.time);
		Timer.text = TimeScore.ToString ();
		if (TimeScore <= 0 &&  !isGameOver) {
			Timer.text = "0";
			GameOverText.text = "Time Out";
			GameOverPanel.SetActive (true);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			player.GetComponent<SpriteRenderer> ().enabled = false;
			player.SetActive (false);
			GameOverText.text = "YOU WIN";
			GameOverPanel.SetActive (true);
			isGameOver = true;
			NextLevelButton.enabled = true;
			GameObject.FindGameObjectWithTag ("MainCamera").transform.localPosition = new Vector3 (0, 1, -10);
		}
	}

	public void MainMenu()
	{
		SceneManager.LoadScene (0);
	}

	public void Restart()
	{
		if (player != null) {
			player.SetActive( true);
			player.GetComponent<SpriteRenderer> ().enabled = true;
			player.transform.position = new Vector3 (-3, -6, 0);
			player.GetComponent<SpriteRenderer> ().sprite = carSprite;
		}
		
		isGameOver = true;
		GameOverPanel.SetActive (false);
		TimerValue = (int)(Time.time + 15);
	}

}
