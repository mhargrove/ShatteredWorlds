using UnityEngine;
using System.Collections;
using System.IO.Ports; 

/*
 * Uses serial port for right now. Unsure about sending 16 bit readings using Uniduino without modifying Standard Firmata. 
 * TODO: Normalize accelerometer data
 * TODO: Reduce latency
 * TODO: Recognize gestures
 */

public class ArduinoController : MonoBehaviour {

	SerialPort inputStream; 
	int leftFootpad;
	int rightFootpad; 
	int arduinoID; 
	float x;
	float y; 
	float z; 
	float angleXZ; 
	float angleYZ; 
	float angleXY; 

	/*
	 * Sets up serial port; You may need to change this to match your arduino port  
	 */
	public void Setup(string s){
		try{
			inputStream = new SerialPort(s, 57600);  
			inputStream.Open (); 
		} catch{
			print ("Could not find Arduino."); 
		}
	}
	
	void Awake () 
	{
		DontDestroyOnLoad (gameObject);
	}
	
	/*
	 * Parses serial data and stores to appropriate global variables 
	 */
	void Update()
	{
		readArduinoData ();
	}

	public void readArduinoData()
	{
		try{
			//print (inputStream.ReadLine ());
			string[] str = inputStream.ReadLine ().Split(',');
			arduinoID = int.Parse (str[0]); 
			leftFootpad = int.Parse (str[1]); 
			rightFootpad = int.Parse (str[2]); 
			x = float.Parse (str[3]);
			y = float.Parse (str[4]);
			z = float.Parse(str[5]); 
			angleXZ = float.Parse(str[6]);
			angleYZ = float.Parse (str[7]);
			angleXY = float.Parse (str[8]); 
		//	print ("Raw XYZ Axis Readings: (" + x + " , " + y + " , " + z + " )"); 


		}catch(UnityException e){
			print ("Error communicating with Arduino."+e.Message); 
		}
	}



	public int getArduinoID(){
		return arduinoID; 
	}

	/*
	 * Returns accelerometer readings as a vector. Data will need to be normalized
	 */
	public Vector3 readAccelerometer(){
		Update (); 
		return new Vector3 (x, y, z); 
	}

	/*
	 * Returns if left foot pad is pressed (released)
	 */
	public int readLeftFootpad(){
		return leftFootpad; 
	}

	/*
	 * Returns if right foot pad is pressed (released)
	 */ 
	public int readRightFootpad(){
		return rightFootpad;
	}

}
