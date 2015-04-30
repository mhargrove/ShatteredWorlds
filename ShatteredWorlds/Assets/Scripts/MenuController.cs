using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject arduinoController;
	private int leftBump;
	private int rightBump;

	// Use this for initialization
	void Start () {
		arduinoController = GameObject.Find ("ArduinoData");
	}
	
	// Update is called once per frame
	void Update () {
		leftBump = arduinoController.GetComponent<ArduinoController> ().getLeftBump (); 
		rightBump = arduinoController.GetComponent<ArduinoController> ().getRightBump ();

		if (leftBump == 1 && rightBump == 1) {
			LoadScene(2);
		}
	}

	public void LoadScene(int level){
		Debug.Log ("Play button pressed");
		Application.LoadLevel (level);
	}
}
