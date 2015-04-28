using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject arduinoController;
	private int leftFoot;
	private int rightFoot;

	// Use this for initialization
	void Start () {
		arduinoController = GameObject.Find ("ArduinoData");
	}
	
	// Update is called once per frame
	void Update () {
		leftFoot = arduinoController.GetComponent<ArduinoController> ().readLeftFootpad (); 
		rightFoot = arduinoController.GetComponent<ArduinoController> ().readRightFootpad ();

		if (leftFoot == 1 || rightFoot == 1) {
			LoadScene(2);
		}
	}

	public void LoadScene(int level){
		Debug.Log ("Play button pressed");
		Application.LoadLevel (level);
	}
}
