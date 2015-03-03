using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using Uniduino;
public class PlayerController : MonoBehaviour {

	public Arduino arduino;
	public GameObject gameController;

	void Start()
	{
		gameController = GameObject.Find( "GameController" );
		if (gameController)
		    arduino = gameController.GetComponent<GameController>().arduino;
	}

	void Update()
	{  
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal * 100.0f, 0.0f, moveVertical * 100.0f);
		if (moveHorizontal != 0 || moveVertical != 0)
			rigidbody.AddForce (movement);
		else
			rigidbody.Sleep (); 
	}
	
}
