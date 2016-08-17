using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class AppearOnGaze : MonoBehaviour, IGvrGazeResponder {
	private Vector3 startingPosition;
//	private Rigidbody rigidbody;

	void Start() {
//		startingPosition = transform.localPosition;
		SetGazedAt(false);
		//rigidbody = transform.gameObject.GetComponent<Rigidbody> ();
	}

	public void SetGazedAt(bool gazedAt) {
		if (gazedAt) {
			GetComponent<Renderer>().material.color = new Color (0.5f, 0.5f, 0.5f, .4f);
			transform.gameObject.GetComponent<BoxCollider>().isTrigger = false;
			//transform.gameObject.layer = 8;
		} else {
			GetComponent<Renderer>().material.color = new Color (0.0f, 0.5f, 0.5f, .05f);
//			transform.gameObject.layer = 9;
			transform.gameObject.GetComponent<BoxCollider>().isTrigger = true;
		}
	}

	public void Reset() {
//		transform.localPosition = startingPosition;
	}

	#region IGvrGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter() {
		SetGazedAt(true);
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		SetGazedAt(false);
	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger() {
		//		TeleportRandomly();
	}

	#endregion
}
