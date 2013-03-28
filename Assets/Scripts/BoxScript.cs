using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxScript : MonoBehaviour {
	
	private List<GameObject> bagels = new List<GameObject>();
	
	void OnCollisionEnter (Collision col) {
		string sendString = gameObject.name[0].ToString() + col.transform.gameObject.name[0].ToString();
		if (name == "MissPlane") {
			GameController.instance.SendMessage("BagelInBox", sendString);
			GameController.instance.SendMessage("DestroyBagel", col.transform.gameObject);
			return;
		}
		
		if (!bagels.Contains(col.transform.gameObject)) {
			GameController.instance.SendMessage("BagelInBox", sendString);
			bagels.Add(col.transform.gameObject);
		}
		
		if (bagels.Count > 5) {
			GameController.instance.SendMessage("DestroyBagel", bagels[0]);
			bagels.RemoveAt(0);
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
