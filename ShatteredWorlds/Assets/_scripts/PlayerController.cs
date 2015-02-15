using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using Uniduino;
public class PlayerController : MonoBehaviour {

	public Arduino arduino;
	void Start()
	{
		arduino = Arduino.global;
		arduino.Setup (ConfigurePins);
	//	StartCoroutine (FootSensor());
	}
	void ConfigurePins()
	{
		arduino.pinMode (0, PinMode.ANALOG);
		arduino.reportAnalog(0, 1);
	}


	void Update()
	{
		int footPedal = arduino.analogRead (0);

		if (footPedal > 0)
            this.rigidbody.AddForce(new Vector3(2.0f,0.0f,0.0f));
	}
	
	void OnDataReceived(string message)
	{
		Debug.Log (message);
	}
}
