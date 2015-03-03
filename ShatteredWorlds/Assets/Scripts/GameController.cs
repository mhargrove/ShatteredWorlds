using UnityEngine;
using System.Collections;
using Uniduino;

public class GameController : MonoBehaviour {

	public Arduino arduino;
	void Awake () 
	{
		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		SetupArduino ();
	}

	void Update()
	{
		int footPedal = arduino.analogRead (0);


	}

	void SetupArduino ()
	{
		arduino = Arduino.global;
		arduino.Setup (ConfigurePins);
	}
	void ConfigurePins()
	{
		arduino.pinMode (0, PinMode.ANALOG);
	}

	void showPauseScreen()
	{

	}



}
