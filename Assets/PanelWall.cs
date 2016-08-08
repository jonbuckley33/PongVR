using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PanelWall : MonoBehaviour {
	public float PanelWidth;
	public float PanelHeight;
	public float PanelDepth;

	void ResetChildren() {
		for (int i = 0; i < 9; i++) {
			var child_transform = transform.GetChild(i);
			child_transform.localScale = new Vector3 (PanelWidth, PanelHeight, PanelDepth);
			float x = ((i % 3) - 1) * PanelWidth;
			float y = (2 - (i / 3)) * PanelHeight;
			child_transform.localPosition = new Vector3 (x, y, 0.0f); 
		}
	}

	void Update() {
		ResetChildren ();
	}

	void Reset() {
		ResetChildren ();
	}

	// Use this for initialization
	void Start () {
		ResetChildren ();
	}
}
