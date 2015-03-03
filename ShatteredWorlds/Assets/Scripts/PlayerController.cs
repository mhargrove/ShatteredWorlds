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

	void Start()
	{
		arduinoController = new ArduinoController();
		arduinoController.Setup ("/dev/tty.usbmodem621"); 
//		gameController = GameObject.Find( "GameController" );
//		if (gameController)
//		    arduino = gameController.GetComponent<GameController>().arduino;
	}

	void Update()
	{  
		float moveHorizontal = arduinoController.readAccelerometer().x*0.01f;
		if (moveHorizontal > -100.0f && moveHorizontal < 100.0f) {
			moveHorizontal = 0.0f; 
		}
		print (moveHorizontal); 
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal*0.1f, 0.0f, moveVertical * 10.0f);

		if (moveHorizontal != 0 || moveVertical != 0)
			rigidbody.AddForce (movement);
		else
			rigidbody.Sleep (); 
	}
	
}
