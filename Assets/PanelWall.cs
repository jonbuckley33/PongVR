using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PanelWall : MonoBehaviour {
	public float PanelWidth;
	public float PanelHeight;
	public float PanelDepth;

	public GameObject Panel;

	void ResetChildren() {
		Transform panels = transform.Find ("Panels");
		if (panels == null) {
			GameObject new_panel = new GameObject();
			new_panel.transform.name = "Panels";
			new_panel.transform.parent = this.transform;
			panels = new_panel.transform;
		}

		for (int i = 0; i < 9; i++) {
			if (panels.transform.childCount <= i) {
				GameObject new_panel = Instantiate(Panel);
				new_panel.transform.name = i.ToString();
				new_panel.transform.parent = panels.transform;
			}
			Transform ith_panel = panels.transform.GetChild (i);
			ith_panel.localScale = new Vector3 (PanelWidth, PanelHeight, PanelDepth);
			float x = ((i % 3) - 1) * PanelWidth;
			float y = (2 - (i / 3)) * PanelHeight;
			ith_panel.localPosition = new Vector3 (x, y, 0.0f); 
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
