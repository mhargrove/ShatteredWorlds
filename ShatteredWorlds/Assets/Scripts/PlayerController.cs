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
		Vector3 movement = new Vector3 (moveHorizontal * 10.0f, 0.0f, moveVertical * 10.0f);
		rigidbody.AddForce (movement);
	}

}
