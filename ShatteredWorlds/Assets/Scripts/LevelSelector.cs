using UnityEngine;
using System.Collections;

public class LevelSelector : MonoBehaviour {

	public GameObject gameController;
	
	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		string Level1data = gameController.GetComponent<GameController> ().GetLevel1Statistics ();
		string Level2data = gameController.GetComponent<GameController> ().GetLevel2Statistics ();
		GameObject time1 = GameObject.Find ("Time1");
		time1.
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
