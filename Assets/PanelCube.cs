using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PanelCube : MonoBehaviour {
	public float CubeWidth;
	public float PanelDepth;

	public int NumPanelsPerSide;

	public GameObject panel;
	public GameObject goal;

	void ResetGoals() {
		if (transform.Find ("goal-left") == null) {
			GameObject goal_left = Instantiate(goal);
			goal_left.transform.name = "goal-left";
			goal_left.transform.parent = this.transform;

			goal_left.transform.localScale = new Vector3 (CubeWidth, CubeWidth, PanelDepth);
			goal_left.transform.Rotate (new Vector3 (0.0f, 90.0f, 0.0f));
			goal_left.transform.localPosition = new Vector3 (0.0f, CubeWidth / 2, CubeWidth / 2);
		}

		if (transform.Find ("goal-right") == null) {
			GameObject goal_right = Instantiate(goal);
			goal_right.transform.name = "goal-right";
			goal_right.transform.parent = this.transform;

			goal_right.transform.localScale = new Vector3 (CubeWidth, CubeWidth, PanelDepth);
			goal_right.transform.localPosition = new Vector3 (CubeWidth / 2, CubeWidth / 2, 0.0f);
		}

		if (transform.Find ("goal-bottom") == null) {
			GameObject goal_bottom = Instantiate(goal);
			goal_bottom.transform.name = "goal-bottom";
			goal_bottom.transform.parent = this.transform;

			goal_bottom.transform.localScale = new Vector3 (CubeWidth, CubeWidth, PanelDepth);
			goal_bottom.transform.Rotate (new Vector3 (90.0f, 0.0f, 0.0f));
			goal_bottom.transform.localPosition = new Vector3 (CubeWidth / 2, 0.0f, CubeWidth / 2);
		}
	}

	void ResetChildren() {
		float sqrt_num_panels = Mathf.Sqrt (NumPanelsPerSide);
		float panel_width = CubeWidth / sqrt_num_panels;
	
		int c = 0;

		var panels_container = transform.Find ("panels");
		if (panels_container == null) {
			GameObject panels_child = new GameObject ();
			panels_child.transform.name = "panels";
			panels_child.transform.parent = this.transform;

			panels_container = panels_child.transform;
		}

		// 3 Walls
		for (int j = 0; j < 3; j++) {
			for (int i = 0; i < NumPanelsPerSide; i++) {
				Transform child_transform;
				if (panels_container.childCount > c) {
					child_transform = panels_container.GetChild(c);
				} else {
					// Make a new panel and child it to the cube.
					var new_child = Instantiate (panel);
					new_child.transform.parent = panels_container.transform;
					child_transform = new_child.transform;

					if (j == 0) {
						child_transform.Rotate (new Vector3 (0.0f, 90.0f, 0.0f));
					} else if (j == 1) {
						child_transform.Rotate(new Vector3 (90.0f, 0.0f, 0.0f));
					}
				}
				child_transform.localScale = new Vector3 (panel_width, panel_width, PanelDepth);
				float x = ((i % sqrt_num_panels) + (panel_width / 2)) * panel_width;
				float y = (((int) (i / sqrt_num_panels)) + (panel_width / 2)) * panel_width;
				if (j == 0) {
					// Fix x coordinate. 
					child_transform.localPosition = new Vector3 (CubeWidth, x, y);
				} else if (j == 1) {
					// Fix y coordinate.
					child_transform.localPosition = new Vector3 (x, CubeWidth, y);
				} else if (j == 2) {
					// Fix z coordinate.
					child_transform.localPosition = new Vector3 (x, y, CubeWidth);
				}

				c++;
			}
		}
	}

	void Update() {
		ResetGoals ();
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
