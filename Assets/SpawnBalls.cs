using UnityEngine;
using System.Collections;

public class SpawnBalls : MonoBehaviour {
	public float SphereX;
	public float SphereY;
	public float SphereZ;
	public GameObject ball;

	private float time;
	private float interval = 5.0f;

	// Use this for initialization
	void Start () {
		time = Time.fixedTime;
	}

	void SpawnBall() {
		GameObject sphere = Instantiate (ball);
		sphere.transform.position = new Vector3(SphereX, SphereY, SphereZ);

		sphere.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -0.1f, 150.0f));
	}
	
	// Update is called once per frame
	void Update () {
		if (time + interval < Time.fixedTime) {
			time = Time.fixedTime;

			SpawnBall ();
		}
	}
}
