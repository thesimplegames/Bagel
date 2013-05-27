using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public static GameController instance;
	
	private List<GameObject> bagels = new List<GameObject>();
	
	private Transform selectedBagel;
	
	private GameObject bagelPrefab;
	
	private float lastGenerationTime = -10;
	private float level = 1;
	private float bagelsCount = 0;
	private int bagelCathed = 0;
	private float missedBagels = 0;
	
	private float yDragLevel = 2;
	
	public int score = 0;
	
	private bool isPaused = false;
	
	private float showNextLevel = 0;
	
	
	
	// Use this for initialization
	void Start () {
		instance = this;
		bagelPrefab = Instantiate(Resources.Load("Prefabs/Bagel") as GameObject) as GameObject;
		bagelPrefab.transform.position = new Vector3(100, 0, 0);
		bagelPrefab.name = "BagelPrefab";
		bagelPrefab.tag = "Untagged";
		bagelPrefab.rigidbody.isKinematic = true;
	}
	
	private void IncreaseLevel() {
		
		if (level == Settings.maxLevel) level --;
		level++;
		bagelCathed = 0;
		bagelsCount = 0;
		bagels.Clear();
		foreach(GameObject bagel in GameObject.FindGameObjectsWithTag("Bagel")) {
			Destroy(bagel);
		}
		showNextLevel = 1f;
	}
	
	private int AvailableBagelTypes() {
	
		int result = 2;
		for (int i = 0; i < Settings.levelToTypes.Length; i++) if (level >= Settings.levelToTypes[i]) result = i+3;
		return result;
	}
	
	void BagelInBox (string boxAndBagelName) {	//	boxAndBagelName[0] - box first letter, boxAndBagelName[1] - bagel first letter, 
		//Debug.Log(Time.time.ToString() + " :: " + boxAndBagelName);
		if (boxAndBagelName[0] == boxAndBagelName[1]) {
				
			bagelCathed++;
			
			switch (boxAndBagelName[0]) {
				case '1': score += Settings.boxAScore; break;
				case '2': score += Settings.boxBScore; break;
				case '3': score += Settings.boxCScore; break;
				case '4': score += Settings.boxDScore; break;
				case '5': score += Settings.boxEScore; break;
				case 'M': {
					score += Settings.missPenalty; 
					bagelCathed--;
					break;
				}
			}
			if (bagelCathed>=Settings.bagelsToLevel) IncreaseLevel();
		} else {
			score += Settings.missPenalty;
			missedBagels++;
		}
	}
	
	void DestroyBagel (GameObject dBagel) {
		bagels.Remove(dBagel);
		Destroy(dBagel);
	}
	
	void CreateBagel () {
		GameObject bagel = Instantiate(bagelPrefab) as GameObject;
		bagel.tag = "Bagel";
		bagel.transform.position = new Vector3(Random.value - 0.5f, 0, 2);
		bagel.rigidbody.isKinematic = false;
		int bagelType = Random.Range(1,AvailableBagelTypes()+1);
		bagel.renderer.material.mainTexture = Resources.Load("Png/" + bagelType.ToString() + "Bagel") as Texture;
		bagel.name = bagelType.ToString() + "Bagel" + (++bagelsCount).ToString();
		bagels.Add(bagel);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) {
			if (Time.time > lastGenerationTime + Settings.bagelGenerationDelay + (level - 1) * Settings.bagelGenerationDelayDecreasePerLevel) {
				lastGenerationTime = Time.time;
				CreateBagel();
			}
			
			MoveBagels();
			DragBagel();
		}
	}
	
	private Vector3 offSet;       
	
	void DragBagel () {
	    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);   
		
	    if (Input.GetButtonDown("Fire1")) {     
	        if (!selectedBagel) {     
	            RaycastHit hit;
	            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {        
					if (hit.transform.gameObject.tag == "Bagel") {
		                selectedBagel = hit.transform;    
		                offSet = selectedBagel.position - ray.origin; 
					}
	            }
	        }
	    } 
		
		if (Input.GetButtonUp("Fire1")) {
			if (selectedBagel) {
				selectedBagel = null;
			}
		}
		
	    if (selectedBagel) {
	        selectedBagel.position = new Vector3(ray.origin.x + offSet.x, yDragLevel, ray.origin.z + offSet.z);     // Only move the obj on a 2D plane.
	    }			
	}
	
	void MoveBagels () {
		for (int i = 0; i < bagels.Count; i++) {
			if (bagels[i].collider.enabled && bagels[i].transform.position.y > -0.5f) {
				Vector3 pos = bagels[i].transform.position;
				if (Mathf.Abs(pos.x) < 1.3f/2)
					bagels[i].transform.position = new Vector3(pos.x, pos.y, pos.z - (Settings.bagelSpeed + (level - 1) * Settings.bagelSpeedIncreasePerLevel) * Time.deltaTime);
			}
		}
	}
	
	void OnGUI () {
		GUI.Button(new Rect(0, 0, 100, 100), "Score:\n" + score.ToString());
		GUI.Button(new Rect(Screen.width - 100, 0, 100, 100), "Missed:\n" + missedBagels.ToString());
		if (GUI.Button(new Rect(0,Screen.height - 100, 100, 100), "Level:\n" + level.ToString())) {level++; }
		if (showNextLevel<=0) return;
		showNextLevel-=Time.deltaTime;
		GUI.Button( new Rect(Screen.width/4,Screen.height/2 - 200,Screen.width/2,200),"Level " + level);
		
	}
	
}
