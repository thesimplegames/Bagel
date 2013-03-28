using UnityEngine;
using System.Collections;

public class DebugTools : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1)) {
			Debug.Log(Camera.main.ScreenPointToRay(Input.mousePosition).origin);
		}
	}
}
