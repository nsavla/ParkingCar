using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour {

	private Rigidbody2D myRigidBody ;
	private SpriteRenderer spriteRenderer ;
	private Quaternion calibrationQuaternion ;
	[SerializeField] private float Horizontalspeed;
	[SerializeField] private float Verticalspeed;
	[SerializeField] private Sprite CarMoveUp;
	[SerializeField] private Sprite CarMoveDown;
	[SerializeField] private Sprite CarMoveLeft;
	[SerializeField] private Sprite CarMoveRight;
	[SerializeField] private ParticleSystem CrashParticles;
	[SerializeField] private AudioSource explosion;
	[SerializeField] private GameOverScript gameOverScript;
	[SerializeField] private Animator LoadAnimator;

	// Use this for initialization
	void Start () {		
		LoadAnimator.Play("LevelLoadPanelAnim");
		myRigidBody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		CalibrateAccelerometer ();
		CrashParticles.Stop ();
	}

	// Update is called once per frame
	void Update () {



		Vector3 accelerationRaw = Input.acceleration;
		Vector3 acceleration = FixAcceleration (accelerationRaw);
		Vector3 movement = new Vector3 (acceleration.x, acceleration.y , 0.0f);
		//Debug.Log ("Acceleration = " + acceleration.x + " and " + acceleration.y );

		if ((Mathf.Abs (acceleration.x) - Mathf.Abs (acceleration.y)) > 0.1f) {
			if (acceleration.x > 0) {
				spriteRenderer.sprite = CarMoveLeft;
			} else 
				spriteRenderer.sprite = CarMoveRight;
		}
		else if ((Mathf.Abs (acceleration.y) - Mathf.Abs (acceleration.x)) > 0.1f) {
			if (acceleration.y > 0) {
				spriteRenderer.sprite = CarMoveDown;
			} else 
				spriteRenderer.sprite = CarMoveUp;
		}

		myRigidBody.velocity = new Vector2 ( movement.x * Horizontalspeed, movement.y * Verticalspeed);
		myRigidBody.position = new Vector3 (
			Mathf.Clamp (myRigidBody.position.x, -25, 25),
			Mathf.Clamp (myRigidBody.position.y , -25, 25),
			0.0f
			);
	}

	void CalibrateAccelerometer()
	{
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);

	}

	Vector3 FixAcceleration ( Vector3 acceleration)
	{
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Goal")
			return;


		gameOverScript.TakeCrashDamage ();

		foreach (ContactPoint2D contact in collision.contacts) {
			CrashParticles.Play ();
			GameObject.FindGameObjectWithTag("MainCamera").transform.localPosition = new Vector3 (
				GameObject.FindGameObjectWithTag("MainCamera").transform.localPosition.y + Random.Range(-0.1f,0.1f),
				GameObject.FindGameObjectWithTag("MainCamera").transform.localPosition.y + Random.Range(-0.1f,0.1f),
				GameObject.FindGameObjectWithTag("MainCamera").transform.localPosition.z);
			explosion.Play ();
			StartCoroutine (ParticleWait());
			break;
		}
	}

	IEnumerator ParticleWait()
	{
		yield return new WaitForSeconds (0.1f);
		CrashParticles.Stop ();
		GameObject.FindGameObjectWithTag ("MainCamera").transform.localPosition = new Vector3 (0, 1, -10);
	}
		
}
