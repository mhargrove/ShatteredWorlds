using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class LevelSelector : MonoBehaviour {

	public GameObject gameController;
	public GameObject time1_1 = GameObject.Find ("Time1_1");
	public GameObject time1_2 = GameObject.Find ("Time1_2");
	public GameObject time1_3 = GameObject.Find ("Time1_3");
	
	public GameObject time2_1 = GameObject.Find ("Time2_1");
	public GameObject time2_2 = GameObject.Find ("Time2_2");
	public GameObject time2_3 = GameObject.Find ("Time2_3");

	public List<string> level1BestTimes = new List<string> ();
	public List<string> level2BestTimes = new List<string> ();
	// Use this for initialization
	void Start () {

		//grab the current data for last times
		for (int i = 0; i < 3; i++) {
			string key1 = "Time1_" + i.ToString();
			string key2 = "Time2_" + i.ToString();
			level1BestTimes.Add(PlayerPrefs.GetString(key1)); 
			level2BestTimes.Add(PlayerPrefs.GetString(key2)); 
			Debug.Log (level1BestTimes[i]);
			Debug.Log (level2BestTimes[i]);
		}

		gameController = GameObject.FindGameObjectWithTag ("GameController");

		//most recent time for level 1 and level 2
		string Level1data = gameController.GetComponent<GameController> ().GetLevel1Statistics ();
		string Level2data = gameController.GetComponent<GameController> ().GetLevel2Statistics ();
	
		if (Level1data != "00:00:00" || Level1data != level1BestTimes[0]) {
			level1BestTimes[2] = level1BestTimes[1];
			level1BestTimes[1] = level1BestTimes[0];
			level1BestTimes[0] = Level1data;
		}
		if (Level2data != "00:00:00" || Level1data != level1BestTimes[0]) {
			level2BestTimes[2] = level2BestTimes[1];
			level2BestTimes[1] = level2BestTimes[0];
			level2BestTimes[0] = Level2data;
		}
		setText ();
		saveDataToPlayerPrefs ();
	}
	public void setText ()
	{
		time1_1.GetComponent<Text> ().text = level1BestTimes[0];
		time1_2.GetComponent<Text> ().text = level1BestTimes[1];
		time1_3.GetComponent<Text> ().text = level1BestTimes[2];
		
		time2_1.GetComponent<Text> ().text = level2BestTimes[0];
		time2_2.GetComponent<Text> ().text = level2BestTimes[1];
		time2_3.GetComponent<Text> ().text = level2BestTimes[2];

	}
	public void saveDataToPlayerPrefs()
	{
		for (int i = 0; i < 3; i++) {
			string key1 = "Time1_" + i.ToString();
			string key2 = "Time2_" + i.ToString();
			PlayerPrefs.SetString(key1, level1BestTimes[i]); 
			PlayerPrefs.SetString(key2, level2BestTimes[i]); 
			Debug.Log("Updated Time1_" + i + ": " + level1BestTimes[i]);
			Debug.Log("Updated Time2_" + i + ": " + level2BestTimes[i]);

		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
