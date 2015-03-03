using UnityEngine;
using System.Collections;
using Uniduino;

public class GameController : MonoBehaviour {
	public ArduinoController arduinoController;
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
		arduinoController = new ArduinoController();
		arduinoController.Setup ("/dev/tty.usbmodem621");
	}
	void ConfigurePins()
	{
		arduino.pinMode (0, PinMode.ANALOG);
	}

	void showPauseScreen()
	{

	}



}
