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
		gameController = GameObject.Find( "Game Controller" );
		if (gameController)
		    arduino = gameController.GetComponent<GameController>().arduino;
	}

	void Update()
	{

	}

}
