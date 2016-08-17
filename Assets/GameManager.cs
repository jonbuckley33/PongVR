using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public PongBall Ball;
	public Transform SpawnPoint;
	public int NumPlayers;
	public List<Transform> PlayerWalls;
	public float Force;
	public float SpeedUpForce;

	public GameObject UI;
	public Light light;

	private Vector3[] player_directions;
	private int player_turn = 0;
	private int num_hits = 0;

	private List<int> scores;

	private PongBall active_ball;


	// Use this for initialization
	void Start () {
		// Set up n-players scores.
		scores = new List<int> ();
		for (int i = 0; i < NumPlayers; i++) {
			scores.Add (0);
		}

		player_directions = new Vector3 [NumPlayers];
		// Set up vectors from spawn point to walls.
		for (int i = 0; i < NumPlayers; i++) {
			if (i > PlayerWalls.Count) {
				break;
			}
			var vector_from_spawn_to_wall = (PlayerWalls [i].position - SpawnPoint.position);
			vector_from_spawn_to_wall.Normalize ();
			player_directions[i] = vector_from_spawn_to_wall;
		}

		ResetBall ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void SpawnBall() {
		PongBall new_ball = (PongBall) Instantiate(Ball, SpawnPoint.position, SpawnPoint.rotation);
		print ("putting ball at 4, 4, 4");

		new_ball.GetComponent<PongBall>().RegisterBallMissedPaddleHandler (BallMissedPaddleHandler);
		new_ball.GetComponent<PongBall> ().RegisterBallHitPaddleHandler (BallHitPaddleHandler);

		active_ball = new_ball;
	}

	void ResetBall() {
		num_hits = 0;
		if (active_ball == null) {
			SpawnBall ();
		}

		active_ball.transform.position = SpawnPoint.position;
		active_ball.GetComponent<Rigidbody>().AddForce (player_directions[player_turn] * Force);
	}

	void SpeedUpBall() {
		Rigidbody ball_rigid_body = active_ball.GetComponent<Rigidbody> ();
		ball_rigid_body.AddForce((ball_rigid_body.velocity.normalized + Random.insideUnitSphere) * SpeedUpForce, ForceMode.Impulse);
	}

	void BallHitPaddleHandler(int player_who_hit) {
		num_hits++;
		if (num_hits % 2 == 0) {
			light.color = new Color (0.7f, 0.5f, 0.0f);
		} else {
			light.color = new Color (0.0f, 0.5f, 0.7f);
		}

		SpeedUpBall ();
	}

	void BallMissedPaddleHandler(int player_who_scored) {
		print ("Ball missed paddle handler");
		scores [player_who_scored]++;

		light.color = new Color (0.7f, 0.0f, 0.0f);

		// Advance player turn.
		player_turn++;
		player_turn %= NumPlayers;

		ResetBall ();
	}
}
