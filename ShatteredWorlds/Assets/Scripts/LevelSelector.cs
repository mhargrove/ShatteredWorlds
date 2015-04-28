using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LevelSelector : MonoBehaviour {

	public GameObject gameController;
	
	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		string Level1data = gameController.GetComponent<GameController> ().GetLevel1Statistics ();
		string Level2data = gameController.GetComponent<GameController> ().GetLevel2Statistics ();
		GameObject time1 = GameObject.Find ("Time1");
		GameObject time2 = GameObject.Find ("Time2");
		time1.GetComponent<Text> ().text = Level1data;
		time2.GetComponent<Text> ().text = Level2data;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
