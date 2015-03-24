using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Uniduino;
public class PlayerController : MonoBehaviour {

	public Arduino arduino;
	public GameObject gameController;
	public Transform fadeToBlack;
	public GameObject blackScreen;
	public ArduinoController arduinoController;	
	public GameObject UIcontroller;
	private Vector3 moveUp;
	public bool fadingToBlack = false;
	public bool initialSceneBlack= true;
	public bool moving = false;
	public float startBlackness = 0;
	public float endBlackness = 6;
	void Start()
	{
	   // arduinoController = new ArduinoController();
	   // arduinoController.Setup ("/dev/tty.usbmodem1451");
		gameController = GameObject.Find ("GameController");

		UIcontroller = GameObject.Find ("UI");
	}

	void Update()
	{  
		testMovement ();
		if (this.rigidbody.IsSleeping()) {
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
		if (!this.rigidbody.IsSleeping ()) {
			DestroyClones ("DarkMist", 0.2f);
			fadingToBlack = false;
		}

		if (initialSceneBlack && moving) {
			//levels initially load black, once you move set initialSceneBlack to false and destroy the black screen
			Destroy(GameObject.FindGameObjectWithTag("BlackScreen"));
			initialSceneBlack = false;
		}
		   
		//arduinoMovement();
	}
	void arduinoMovement()
	{
		bool moveH = true;
		bool moveV = true;
		float moveHorizontal = arduinoController.readAccelerometer ().y;
		if (moveHorizontal > -5000.0f && moveHorizontal < 5000.0f && moveV == false) {
			moveH = false;
			rigidbody.Sleep ();
		} else if (moveHorizontal > 5000.0f)
			rigidbody.AddForce (100.0f, 0.0f, 0.0f);
		else if (moveHorizontal < -5000.0f)
			rigidbody.AddForce (-100.0f, 0.0f, 0.0f);
		
		
		int leftFoot = arduinoController.readLeftFootpad ();
		int rightFoot = arduinoController.readRightFootpad ();
		
		//forward movement, foot sensors	 
		moveUp = new Vector3 (0.0f, 0.0f, 70.0f);
		if (((leftFoot == 1 && rightFoot == 1) || (leftFoot == 0 && rightFoot == 0)) && moveH == false) {
			moveV = false;
			rigidbody.Sleep ();
		} else if (leftFoot == 1 && rightFoot == 0) {
			rigidbody.AddForce (moveUp);
			UIcontroller.GetComponent<UIController> ().updateStepsTaken ();
		} else if (leftFoot == 0 && rightFoot == 1) {
			rigidbody.AddForce (moveUp);	
			UIcontroller.GetComponent<UIController> ().updateStepsTaken ();
		}
	}

	void testMovement()
	{
		if (Input.GetKey (KeyCode.UpArrow)) {
			rigidbody.AddForce (0.0f, 0.0f, 100.0f);
			moving = true;
			UIcontroller.GetComponent<UIController> ().updateStepsTaken ();
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			rigidbody.AddForce (0.0f, 0.0f, -50.0f);
			UIcontroller.GetComponent<UIController> ().updateStepsTaken ();
		}
		if ( Input.GetKey(KeyCode.RightArrow) )
			rigidbody.AddForce (50.0f, 0.0f, 0.0f);
		if ( Input.GetKey(KeyCode.LeftArrow) )
			rigidbody.AddForce (-50.0f, 0.0f, 0.0f);
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
