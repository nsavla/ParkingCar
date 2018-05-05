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
	[SerializeField]
	private List<GameObject> HeartImages;
	private int HeartImagesLength;
	private int TimerValue;
	private bool isGameOver = false;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag ("Player");
		GameOverPanel.SetActive (false);
		TimerValue = (int)(Time.time + 15);
		HeartImagesLength = HeartImages.Count;
		NextLevelButton.gameObject.SetActive (false);
		//Debug.Log ("NextLevelButton = " + NextLevelButton);
	}

	// Update is called once per frame
	void Update () {
		if (player.activeSelf == false && SceneManager.GetActiveScene().buildIndex != 4) {
			TimerValue = (int)(Time.time + 15);	
		}

		if (!isGameOver) {
			int TimeScore = TimerValue - (int)(Time.time);
			//Debug.Log ("Timevalue = " + TimerValue + " And gameover: " + isGameOver + " and " + Time.time);
			Timer.text = TimeScore <= 0 ? "0" : TimeScore.ToString ();
			if (TimeScore <= 0) {
				EndGame ("Time Out");
				NextLevelButton.gameObject.SetActive (false);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			EndGame ("YOU WIN");
			NextLevelButton.gameObject.SetActive (true);
		}
	}

	public void EndGame(string Message)
	{
		GameObject.FindGameObjectWithTag ("MainCamera").transform.localPosition = new Vector3 (0, 1, -10);
		GameOverText.text = Message;
		GameOverPanel.SetActive (true);
		isGameOver = true;
		player.GetComponent<SpriteRenderer> ().enabled = false;
		player.SetActive (false);

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
		
		isGameOver = false;
		GameOverPanel.SetActive (false);
		foreach (GameObject Heart in HeartImages)
			Heart.SetActive (true);
		HeartImagesLength = HeartImages.Count;
		TimerValue = (int)(Time.time + 15);

	}

	public void TakeCrashDamage()
	{
		if (HeartImagesLength > 0) {
			(HeartImages [HeartImagesLength - 1]).SetActive (false);
			HeartImagesLength = HeartImagesLength - 1;
		} 

		if( HeartImagesLength == 0)
		{
			StartCoroutine (ParticleWait());
		}
	}

	IEnumerator ParticleWait()
	{
		yield return new WaitForSeconds (0.1f);
		EndGame ("NO LIVES LEFT");
		NextLevelButton.gameObject.SetActive (false);
	}

	public void SetHeartImageLength()
	{
		HeartImagesLength = HeartImages.Count;
	}

}
