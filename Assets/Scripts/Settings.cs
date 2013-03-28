using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {
	
	//public static Settings instance;

	public static float bagelSpeed = 1f;	//	per second
	public static float bagelSpeedIncreasePerLevel = 0.03f;
	public static float bagelGenerationDelay = 1.1f;
	public static float bagelGenerationDelayDecreasePerLevel = 0.02f;
	
	public static int boxAScore = 10;
	public static int boxBScore = 10;
	public static int boxCScore = 10;
	public static int boxDScore = 10;
	public static int boxEScore = 5;
	public static int missPenalty = -25;
	
	
	void Start () {
		//instance = this;
		animation.Play("Take 001");
	}
	
}
