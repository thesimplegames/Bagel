using UnityEngine;
using System.Collections;

public class Tools : MonoBehaviour {

	public static Rect MRect (Rect bounds) {
		float scale = 960f / Screen.height;
		return new Rect(bounds.x * scale, bounds.y * scale, bounds.width * scale, bounds.height * scale);
	}
	/*
	public static Vector2 MouseToWorldXY(Vector2 mouseXY) {
		//	TODO refactor this
		float width = 1.85;
		float height = width / 2f * 3;
		ret
	}
	*/
}
