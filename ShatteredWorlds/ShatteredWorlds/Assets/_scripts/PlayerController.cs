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
		arduino.pinMode (13, PinMode.OUTPUT);
	}

	IEnumerator FootSensor()
	{
		while (true) {
			arduino.digitalWrite(13, Arduino.HIGH);
			yield return new WaitForSeconds(1);
			arduino.digitalWrite(13, Arduino.LOW);
			yield return new WaitForSeconds(1);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown ("space"))
			arduino.digitalWrite (13, Arduino.HIGH);
		if (Input.GetKeyDown ("c"))
			arduino.digitalWrite (13, Arduino.LOW);
	}
	
	void OnDataReceived(string message)
	{
		Debug.Log (message);
	}
}
