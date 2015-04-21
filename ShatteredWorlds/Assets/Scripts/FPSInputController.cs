using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


// Require a character controller to be attached to the same game object
[RequireComponent(typeof(CharacterMotor))]
[AddComponentMenu("Character/FPS Input Controller")]

public class FPSInputController : MonoBehaviour
{
    private CharacterMotor motor;
	public GameObject arduinoController;
	public GameObject UIcontroller;
	public GameObject gameController;
	public GameObject audioController;

	public bool fadingToBlack = false;
	public bool initialSceneBlack= true;
	public float startBlackness = 0;
	public float endBlackness = 6;
	public float timeTilBlackness = 10;
	public Transform fadeToBlack;
	public GameObject blackScreen;
	public GameObject fadeInOut;
	
	private bool canLeft = true;
	private bool canRight = true;
	// Use this for initialization

	void Start()
	{
		arduinoController = GameObject.Find ("ArduinoData");
		gameController = GameObject.Find ("GameController");
		UIcontroller = GameObject.Find ("UI");
		if (arduinoController == null)
			Debug.Log ("Why is this null and still working");
		fadeInOut = GameObject.FindGameObjectWithTag ("Fader");
		audioController = GameObject.Find ("audioController");
	}
    void Awake()
    {
        motor = GetComponent<CharacterMotor>();
    }

	private int leftFoot;
	private int rightFoot;
	private float moveHorizontal;
	private float vertical;
	private float horizontal;
	private Vector3 directionVector; 

    // Update is called once per frame
    void Update()
    {
		//arduino data
		leftFoot = arduinoController.GetComponent<ArduinoController> ().readLeftFootpad (); 
		rightFoot = arduinoController.GetComponent<ArduinoController> ().readRightFootpad (); 
		moveHorizontal = arduinoController.GetComponent<ArduinoController> ().getLeftAccelData ().y;
		vertical = 0;
		horizontal = 0;

		//Debug.Log ("LeftFoot = " + leftFoot + "RightFoot = " + rightFoot + "acceleromenter y = " + moveHorizontal);

		if (leftFoot == rightFoot) {
			vertical = 0;
		} else if (leftFoot == 1 && canLeft) {
			vertical = 10;
			canLeft = false;
			canRight = true;
			UIcontroller.GetComponent<UIController> ().updateStepsTaken ();
		} else if (rightFoot == 1 && canRight) {
			vertical = 10;	
			canRight = false;
			canLeft = true;
			UIcontroller.GetComponent<UIController> ().updateStepsTaken ();
		}


		/*if (moveHorizontal > -5000.0f && moveHorizontal < 5000.0f) {
			horizontal = 0;
		} else if (moveHorizontal > 5000.0f)
			horizontal = 1;
		else if (moveHorizontal < -5000.0f)
			horizontal = -1;*/


        //Pass the arduino data to the direction vector
        directionVector = new Vector3(horizontal, 0, vertical);
		//Vector3 directionVector = new Vector3 (0, 0, 0);
        if (directionVector != Vector3.zero) {
			timeTilBlackness = 10;
			audioController.GetComponent<AudioController> ().stopBlackness();
			
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			float directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;

			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min (1.0f, directionLength);

			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;

			// Multiply the normalized direction vector by the modified length
			directionVector = directionVector * directionLength;
		} 
		else 
		{
			if (!fadeInOut.GetComponent<SceneFadeInOut> ().sceneStarting) {
				timeTilBlackness -= Time.deltaTime;
				if (timeTilBlackness < 0) {
					audioController.GetComponent<AudioController> ().playBlackness();
					fadeInOut.GetComponent<SceneFadeInOut> ().EndScene ();
				}
			}
		}

	
	// Apply the direction to the CharacterMotor
        motor.inputMoveDirection = transform.rotation * directionVector;
        motor.inputJump = Input.GetButton("Jump");
    }

	public void DestroyClones(string tag, float time) 
	{
		var clones = GameObject.FindGameObjectsWithTag (tag);
		if (clones.Any())
		{
			foreach (var clone in clones)
				Destroy(clone, time);
		}
	}
}