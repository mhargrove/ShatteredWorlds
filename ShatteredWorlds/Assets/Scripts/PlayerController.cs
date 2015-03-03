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
	}

	void Update()
	{  
		//testMovement ();
		arduinoMovement();
	}
	void arduinoMovement()
	{
		bool moveH = true;
		bool moveV = true;
		float moveHorizontal = arduinoController.readAccelerometer ().y;
		if (moveHorizontal > -1000.0f && moveHorizontal < 1000.0f && moveV == false) {
			moveH = false;
			rigidbody.Sleep ();
		} else if (moveHorizontal > 1000.0f)
			rigidbody.AddForce (100.0f, 0.0f, 0.0f);
		else if (moveHorizontal < -1000.0f)
			rigidbody.AddForce (-100.0f, 0.0f, 0.0f);
		
		
		int leftFoot = arduinoController.readLeftFootpad ();
		int rightFoot = arduinoController.readRightFootpad ();
		
		//forward movement, foot sensors	 
		moveUp = new Vector3 (0.0f, 0.0f, 70.0f);
		if (leftFoot == 1 && rightFoot == 1 && moveH == false) {
			moveV = false;
			rigidbody.Sleep ();
		} else if (leftFoot == 0 && rightFoot == 0 && moveH == false) {
			moveV = false;
			rigidbody.Sleep ();
		} else if (leftFoot == 1 && rightFoot == 0)
			rigidbody.AddForce (moveUp);
		else if (leftFoot == 0 && rightFoot == 1)
			rigidbody.AddForce (moveUp);	
	}

	void testMovement()
	{
		if (Input.GetKey (KeyCode.UpArrow))
			rigidbody.AddForce (0.0f, 0.0f, 100.0f);
		if ( Input.GetKey(KeyCode.DownArrow) )
			rigidbody.AddForce (0.0f, 0.0f, -50.0f);
		if ( Input.GetKey(KeyCode.RightArrow) )
			rigidbody.AddForce (50.0f, 0.0f, 0.0f);
		if ( Input.GetKey(KeyCode.LeftArrow) )
			rigidbody.AddForce (-50.0f, 0.0f, 0.0f);
	}
}
