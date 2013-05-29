using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {
	
	//public static Settings instance;

	public static float bagelSpeed = 1.1f;	//	per second
	public static float bagelSpeedIncreasePerLevel = 0.04f;
	public static float bagelGenerationDelay = 1.8f;
	public static float bagelGenerationDelayDecreasePerLevel = 0.02f;
	
	public static int boxAScore = 10;
	public static int boxBScore = 10;
	public static int boxCScore = 10;
	public static int boxDScore = 10;
	public static int boxEScore = 5;
	public static int missPenalty = -25;
	
	public static int bagelsToLevel = 4;
	public static float timeToLevel = 3f;
	public static int levelsToAddLife = 5;
	public static int[] levelToTypes = new int[] {5,10,15}; //[0] - to 3 types, [1] to 4 ...
	
	
	public static int maxLevel = 50;
	
	
	void Start () {
		//instance = this;
		//animation.Play("Take 001");
	}
	
}
