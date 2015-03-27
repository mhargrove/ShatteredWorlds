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
	public ArduinoController arduinoController;
	public GameObject UIcontroller;
	public GameObject gameController;

	public bool fadingToBlack = false;
	public bool initialSceneBlack= true;
	public float startBlackness = 0;
	public float endBlackness = 6;
	public Transform fadeToBlack;
	public GameObject blackScreen;
	// Use this for initialization

	void Start()
	{
		arduinoController = new ArduinoController();
		arduinoController.Setup ();
		gameController = GameObject.Find ("GameController");
		UIcontroller = GameObject.Find ("UI");
		if (arduinoController == null)
			Debug.Log ("Why is this null and still working");
	}
    void Awake()
    {
        motor = GetComponent<CharacterMotor>();
    }

    // Update is called once per frame
    void Update()
    {

		//arduino data
		int leftFoot = arduinoController.readLeftFootpad ();
		int rightFoot = arduinoController.readRightFootpad ();
		float moveHorizontal = arduinoController.getLeftAccelData ().y;
		float vertical = 0;
		float horizontal = 0;

		//Debug.Log ("LeftFoot = " + leftFoot + "RightFoot = " + rightFoot + "acceleromenter y = " + moveHorizontal);

		if (((leftFoot == 1 && rightFoot == 1) || (leftFoot == 0 && rightFoot == 0))) {
			vertical = 0;
		} else if (leftFoot == 1 && rightFoot == 0) {
			vertical = 1;
			UIcontroller.GetComponent<UIController> ().updateStepsTaken ();
		} else if (leftFoot == 0 && rightFoot == 1) {
			vertical = 1;	
			UIcontroller.GetComponent<UIController> ().updateStepsTaken ();
		}
		if (moveHorizontal > -5000.0f && moveHorizontal < 5000.0f) {
			horizontal = 0;
		} else if (moveHorizontal > 5000.0f)
			horizontal = 1;
		else if (moveHorizontal < -5000.0f)
			horizontal = -1;


        //Pass the arduino data to the direction vector
        Vector3 directionVector = new Vector3(horizontal, 0, vertical);
		//Vector3 directionVector = new Vector3 (0, 0, 0);
        if (directionVector != Vector3.zero) {

			DestroyClones ("DarkMist", 0.2f);
			fadingToBlack = false;
			if (initialSceneBlack) {
				//levels initially load black, once you move set initialSceneBlack to false and destroy the black screen
				Destroy(GameObject.FindGameObjectWithTag("BlackScreen"));
				initialSceneBlack = false;
			}
			
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
			startBlackness+= Time.deltaTime;
			if (!fadingToBlack){
				Instantiate (fadeToBlack, new Vector3 (this.transform.position.x,this.transform.position.y, this.transform.position.z + 4.0f) , Quaternion.identity);
				Instantiate (fadeToBlack, new Vector3 (this.transform.position.x + 4.0f,this.transform.position.y + 2.0f, this.transform.position.z + 4.0f) , Quaternion.identity);
				Instantiate (fadeToBlack, new Vector3 (this.transform.position.x - 4.0f,this.transform.position.y + 2.0f, this.transform.position.z + 4.0f) , Quaternion.identity);
				fadingToBlack = true;
				startBlackness = 0;
			}
			if (startBlackness >= endBlackness)
			{
				//TODO: Change the audio as you are fading into darkness
				
				//if the screen is black for 10 seconds, reload a random level, 
				//or we can change the players position in the map
				gameController.GetComponent<GameController>().loadRandomLevel();
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