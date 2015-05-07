using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject arduinoController;
	private int leftBump;
	private int rightBump;
	private int leftFoot;
	private int rightFoot;
	private float rightAccelY;
	private float leftAccelY;
	public GameObject InstructionText;
	public GameObject PlayText;
	public GameObject HowToText;

	public bool leftStepUp = false;
	public bool rightStepUp = false;
	public bool leftFootGood = false;
	public bool rightFootGood = false;
	public bool leftHandGood = false;
	public bool rightHandGood = false;
	public bool bumpGood = false;

	public int vertical = 0;
	public bool canLeft = true;
	public bool canRight = true;

	public int stepsTaken = 0;
	// Use this for initialization
	void Start () {
		arduinoController = GameObject.Find ("ArduinoData");
		InstructionText = GameObject.Find ("InstructionText");
		PlayText = GameObject.Find ("PlayText");
		HowToText = GameObject.Find ("HowToText");
	}
	
	// Update is called once per frame
	void Update () {
		PlayText.GetComponent<Text> ().text = "";
		leftBump = arduinoController.GetComponent<ArduinoController> ().getLeftBump (); 
		rightBump = arduinoController.GetComponent<ArduinoController> ().getRightBump ();
		rightFoot = arduinoController.GetComponent<ArduinoController> ().readRightFootpad (); 
		leftFoot = arduinoController.GetComponent<ArduinoController> ().readLeftFootpad (); 
		rightAccelY = arduinoController.GetComponent<ArduinoController> ().getRightAccelData ().y;
		leftAccelY = arduinoController.GetComponent<ArduinoController> ().getLeftAccelData ().y;

		if (stepsTaken < 10) {
			Debug.Log (stepsTaken);
			HowToText.GetComponent<Text> ().text = "Walk in place:  \n This is how you will move forward";
			InstructionText.GetComponent<Text> ().text = "Steps Taken: " + stepsTaken + "/10";
			if (leftFoot == rightFoot) {
			} else if (leftFoot == 1 && canLeft) {
				canLeft = false;
				canRight = true;
				InstructionText.GetComponent<Text> ().text = "Steps Taken: " + stepsTaken + "/10";
				stepsTaken++;
			} else if (rightFoot == 1 && canRight) {
				canRight = false;
				canLeft = true;
				InstructionText.GetComponent<Text> ().text = "Steps Taken: " + stepsTaken + "/10";
				stepsTaken++;
			}
		} else if (!leftHandGood) {
			HowToText.GetComponent<Text> ().text = "Now, move your left hand left: \n This is how you turn to the left!";
			InstructionText.GetComponent<Text> ().text = "Good Job!";
			if (leftAccelY < -23000.0f) {
				leftHandGood = true;
			}
		} else if (!rightHandGood) {
			HowToText.GetComponent<Text> ().text = "Now, move your right hand right: \n This is how you turn to the right!";
			if (rightAccelY > 23000.0f) {
				rightHandGood = true;
			}
		} else {
			HowToText.GetComponent<Text> ().text = "Finally..let's get started!";
			InstructionText.GetComponent<Text>().text = "";
			PlayText.GetComponent<Text>().text = "Propel Hands To Shoot and Lets GO!";
			if (leftBump == 1 && rightBump == 1)
				PlayButtonPressed();
		}
	}

	public void LoadScene(int level){
		Debug.Log ("Play button pressed");
		Application.LoadLevel (level);
	}
	public void PlayButtonPressed() {
		LoadScene (2);
	}

	public void Instructions(string text)
	{
		InstructionText.GetComponent<Text> ().text = text;

	}

}
