using UnityEngine;
using System.Collections;

public class PongBall : MonoBehaviour {
	public float RandomForce;

	public delegate void BallMissedPaddleHandler(int player_who_scored);
	BallMissedPaddleHandler ball_missed_paddle_handler;

	// Use this for initialization
	void Start () {
	}
		
	public void RegisterBallMissedPaddleHandler(BallMissedPaddleHandler handler) {
		ball_missed_paddle_handler = handler;
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Finish") {
			ball_missed_paddle_handler (0);
		} else if (col.gameObject.tag == "Arena") {
			// Add a random force.
			var random_force = Random.insideUnitSphere * RandomForce;
			print ("Random force:" + random_force);
			GetComponent<Rigidbody>().AddForce(random_force);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
