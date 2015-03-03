using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using Uniduino;
public class PlayerController : MonoBehaviour {

	public Arduino arduino;
	public GameObject gameController;
	public ArduinoController arduinoController;

	private Vector3 moveUp;
	void Start()
	{
	    arduinoController = new ArduinoController();
		arduinoController.Setup ("/dev/tty.usbmodem1451");
	 
//		gameController = GameObject.Find( "GameController" );
//		if (gameController)
//		    arduino = gameController.GetComponent<GameController>().arduino;
	}

	void Update()
	{  
		bool moveH = true;
		float moveHorizontal = arduinoController.readAccelerometer().y;
		if (moveHorizontal > -1000.0f && moveHorizontal < 1000.0f) {
			moveH = false;
			rigidbody.Sleep ();
		} else if (moveHorizontal > 1000.0f)
			rigidbody.AddForce (100.0f, 0.0f, 0.0f);
		else if (moveHorizontal < -1000.0f)
			rigidbody.AddForce (-100.0f, 0.0f, 0.0f);
		

		int leftFoot = arduinoController.readLeftFootpad ();
		int rightFoot = arduinoController.readRightFootpad ();

		//forward movement, foot sensors	 
		moveUp = new Vector3 (0.0f, 0.0f, 100.0f);
		if (leftFoot == 1 && rightFoot == 1 && moveH == false) {
			rigidbody.Sleep ();
		} else if (leftFoot == 0 && rightFoot == 0 && moveH == false) {
			rigidbody.Sleep ();
		} else if (leftFoot == 1 && rightFoot == 0)
			rigidbody.AddForce (moveUp);
		else if (leftFoot == 0 && rightFoot == 1)
			rigidbody.AddForce (moveUp);

		
	}
	
}
